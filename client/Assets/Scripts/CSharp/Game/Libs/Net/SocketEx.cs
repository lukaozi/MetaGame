using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Sockets;
using UnityEngine;

public class SocketEx
{
    public delegate object onRecvThreadHandle(int id, ByteArray buffer, int len);

    //对外事件
    public enum EnEvent
    {
        Connect, //连接上或连接失败的时候
        Close,
        Recv,
        Accept //被连接
    }

    public class Result
    {
        public bool ret;
        public string errorMsg;
        public object param;
    }

    public class Res
    {
        public int id; //协议号
        public object param;
    }

    public enum EnCloseType
    {
        Normal,
        NetError,
        AppQuit,
    }

    public enum EnState
    {
        Close,
        Connecting,
        Connected,
    }

    private Socket m_socket;
    private EventNotifier m_notifier = new EventNotifier();

    private EnState m_state;
    private Lockqueue m_msgQueue = new Lockqueue();
    private object m_connectParam;
    private bool m_isFixedCompress = true; //变长int 和变长long


    private SocketRecvThread m_recvThresd;
    private onRecvThreadHandle m_recvThreadHandle;
    private EnCloseType m_lastCloseType = EnCloseType.Normal;


    private HashSet<SocketEx> m_sub = new HashSet<SocketEx>();

    public Socket Socket
    {
        get { return m_socket; }
    }

    public EventNotifier Notifier => m_notifier;
    public EnState State => m_state;

    public bool IsFixedCompress
    {
        get => m_isFixedCompress;
        set => m_isFixedCompress = value;
    }

    public Lockqueue MsgQueue => m_msgQueue;
    public EnCloseType LastCloseType => m_lastCloseType;
    public Action LoopFinishHandle { get; set; }
    public object SocketHandler { get; set; } //上层自己实现处理类，主要是处理协议序列化相关

    public void Accept(string ip, int port)
    {
        Close();

        m_state = EnState.Connected;
        NetMgr.instance.OnSocketConnected(this);
        TimeMgr.instance.FrameLoop(this, OnLoop);

        IPEndPoint ipEndPoint = null;
        NetUtil.NewSocketByIp(ip, port, out m_socket, out ipEndPoint);
        if (m_socket == null || ipEndPoint == null)
        {
            Close(EnCloseType.Normal, "parse id fail");
            return;
        }

        m_socket.Bind(ipEndPoint);
        m_socket.Listen(port);
        BeginAccept();
    }

    public void Connect(string ip, int port, object param = null, float timeout = 8)
    {
        Close();
        NetMgr.instance.OnSocketConnecting(this);
        m_state = EnState.Connecting;
        m_connectParam = param;
        IPEndPoint ipEndPoint = null;
        NetUtil.NewSocketByIp(ip, port, out m_socket, out ipEndPoint);

        if (m_socket == null || ipEndPoint == null)
        {
            Close(EnCloseType.Normal, "parse id fail2:" + ip);
            return;
        }

        m_socket.NoDelay = true; //游戏业务肯定还是nodelay好点
        BeginConnect(ipEndPoint);
        TimeMgr.instance.Once(this, (object param2, TimeMgr.Timer timer) =>
        {
            if (m_state != EnState.Connecting || param2 != this.m_socket)
            {
                return;
            }

            Close(EnCloseType.Normal, "beginconnect timeot");
        }, timeout, m_socket);
        TimeMgr.instance.FrameLoop(this, OnLoop);
    }

    public void Close(EnCloseType type = EnCloseType.Normal, string errorMsg = "", bool notify = true)
    {
        if (this.m_state == EnState.Close)
            return;
        if (TimeMgr.IsExist)
            TimeMgr.instance.Remove(this, OnLoop);
        if (type == EnCloseType.NetError && !string.IsNullOrEmpty(errorMsg))
            Debug.LogError("socketex.close closeType:" + type + "  errMsg:" + errorMsg);

        var oldState = m_state;
        var oldConnParam = m_connectParam;
        var oldSocket = m_socket;
        var oldRecvThread = m_recvThresd;

        if (notify)
            NetMgr.instance.OnSocketClose(this);
        m_state = EnState.Close;
        m_socket = null;
        m_msgQueue = new Lockqueue();
        m_connectParam = null;
        m_recvThresd = null;
        m_lastCloseType = type;
        try
        {
            if (oldSocket.Connected)
                oldSocket.Shutdown(SocketShutdown.Both);
            oldSocket.Close(); //这里不完美，不过前端通常这样就够了。如果是后端，应该先shutdown, 然后read eof, 然后close, 如此可以保证最后一条协议对方收到
        }
        catch (Exception e)
        {
            Debug.LogError(e.Message + "\n" + e.StackTrace);
        }

        if (oldRecvThread != null)
            oldRecvThread.m_isClose = true;

        if (m_sub.Count != 0)
        {
            var subList = new List<SocketEx>(m_sub);
            m_sub = new HashSet<SocketEx>();
            foreach (var socketEx in subList)
            {
                socketEx.Close();
            }
        }

        //广播出去
        if (notify)
        {
            if (oldState == EnState.Connecting)
                m_notifier.Invoke(EnEvent.Connect,
                    new Result() {ret = false, errorMsg = errorMsg, param = oldConnParam});
            else if (oldState == EnState.Connected)
            {
                m_notifier.Invoke(EnEvent.Close, new Result() {ret = true, errorMsg = errorMsg, param = oldConnParam});
            }
            else
            {
                throw new Exception("Close unknow State:" + oldState);
            }
        }
    }

    public void SetRecvThreadHandle(onRecvThreadHandle fun)
    {
        m_recvThreadHandle = fun;
    }

    public void Send(int id, byte[] bs, int startIndex = 0, int len = -1)
    {
        //needtodo
    }

    private void OnSend(IAsyncResult result)
    {
        //needtodo
    }

    private void SafeClose(string error, Socket temSocket = null)
    {
        m_msgQueue.Needcall((object param, int intParam) =>
        {
            if (this.State != SocketEx.EnState.Connected || (temSocket != null && this.m_socket != param))
            {
                return;
            }
            this.Close(SocketEx.EnCloseType.NetError, error);
        }, temSocket);
    }

    private void BeginAccept()
    {
        m_socket.BeginAccept(new AsyncCallback((IAsyncResult ar) =>
        {
            Socket tem = (Socket) ar.AsyncState;
            byte[] Buffer;
            int bytesTransferred;
            Socket handler = null;
            try
            {
                handler = tem.EndAccept(out Buffer, out bytesTransferred, ar);
            }
            catch (Exception e)
            {
                SafeCloseConnect("EndAccept exception:" + e.Message + " \n" + e.StackTrace, tem);
                return;
            }

            if (this.m_state != EnState.Connected || tem != this.m_socket) return;

            m_msgQueue.Needcall(((object param, int intParam) =>
            {
                //主线程调用
                Socket curSocket = (Socket) param;
                if (this.m_state != EnState.Connected) return;
                if (!curSocket.Connected)
                    return;
                var socketEx = new SocketEx();
                socketEx.m_socket = curSocket;
                m_notifier.Invoke(EnEvent.Accept, new Result() {ret = true, param = socketEx});
                m_sub.Add(socketEx);
                if (m_sub.Count > 65536)
                {
                    Debug.LogError("endaccept m_sub overflow");
                }

                socketEx.m_notifier.On(EnEvent.Close, () => { m_sub.Remove(socketEx); });

                TimeMgr.instance.FrameLoop(socketEx, socketEx.OnLoop);
                socketEx.m_state = EnState.Connecting;
                socketEx.OnConnect();
            }), handler);

            BeginAccept();
        }), m_socket);
    }

    private void BeginConnect(IPEndPoint ipEndPoint)
    {
        m_socket.BeginConnect(ipEndPoint, new AsyncCallback((IAsyncResult ar) =>
        {
            Socket tem = (Socket) ar.AsyncState;
            try
            {
                tem.EndConnect(ar);
            }
            catch (Exception e)
            {
                SafeCloseConnect("EndConnect exception:" + e.Message + " \n" + e.StackTrace, tem);
                return;
            }

            m_msgQueue.Needcall((object param, int intParam) =>//主线程调用
            {
                Socket curSocket = (Socket) param;
                if (this.m_state != EnState.Connecting || curSocket != this.m_socket) return;
                if (!curSocket.Connected)
                {
                    SafeCloseConnect("EndConnect Connected==false", curSocket);
                    return;
                }

                this.OnConnect();
            }, ar.AsyncState);
        }), m_socket);
    }

    private void SafeCloseConnect(string error, Socket temSocket = null)
    {
        m_msgQueue.Needcall((object param, int intParam) =>
        {
            if (this.State != SocketEx.EnState.Connecting || (temSocket != null && this.m_socket != param))
                return;
            this.Close(SocketEx.EnCloseType.NetError, error);
        }, temSocket);
    }

    private void OnLoop(object param, TimeMgr.Timer timer)
    {
        m_msgQueue.Call();
        LoopFinishHandle?.Invoke();
    }

    private void OnConnect()
    {
        if (m_state != EnState.Connecting || !m_socket.Connected)
        {
            this.Close(SocketEx.EnCloseType.NetError, "onconnect logic error");
            return;
        }

        NetMgr.instance.OnSocketConnected(this);
        m_state = EnState.Connected;

        m_recvThresd =
            new SocketRecvThread(this, this.m_socket, this.m_recvThreadHandle, m_msgQueue, m_isFixedCompress);

        var p = m_connectParam;
        m_connectParam = null;
        m_notifier.Invoke(EnEvent.Connect, new Result() {ret = true, param = p});
    }

    private Res singletonRes = new Res();

    public void OnRecv(int id, object data)
    {
        if (m_state != EnState.Connected)
        {
            return;
        }

        singletonRes.id = id;
        singletonRes.param = data;

        m_notifier.Invoke(EnEvent.Recv, singletonRes);
    }
}