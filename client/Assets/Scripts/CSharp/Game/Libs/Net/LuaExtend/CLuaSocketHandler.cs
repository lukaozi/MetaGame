using System;
using System.Collections.Generic;
using System.Diagnostics;
using LuaInterface;
using Debug = UnityEngine.Debug;

public class CLuaSocketHandlerBridge
{
    public delegate void onRecv(byte[] buffer);

    public SocketEx socketEx;
    private LuaTable luaHandler;
    private Dictionary<int, onRecv> m_recvHandlers = new Dictionary<int, onRecv>();

    private LuaFunction m_onConnect;
    private LuaFunction m_onClose;
    private LuaFunction m_onRecv;
    private LuaFunction m_onAccept;
    private LuaFunction m_onNew;

    private LuaTable m_luaBuffer;
    private LuaTable m_ints;
    private LuaTable m_nums;
    private LuaTable m_strs;

    private List<CLuaBuffer> m_msgs = new List<CLuaBuffer>(); //协议分片

    public string State
    {
        get { return socketEx.State.ToString(); }
    }

    //优化选项，开启后不用压栈方式读写lua数据，而是用指针直接读写
    public bool UseOptimize { get; set; } = false;

    public CLuaSocketHandlerBridge(SocketEx s, LuaTable luaHandler)
    {
        this.socketEx = s;
        this.socketEx.LoopFinishHandle = OnLoop;
        s.SocketHandler = this;
        this.luaHandler = luaHandler;

        m_onConnect = this.luaHandler.GetLuaFunction("OnConnect");
        m_onClose = this.luaHandler.GetLuaFunction("OnClose");
        m_onRecv = this.luaHandler.GetLuaFunction("OnRecv");
        m_onAccept = this.luaHandler.GetLuaFunction("OnAccept");
        m_onNew = this.luaHandler.GetLuaFunction("New");
        
        if(m_onConnect == null || m_onClose == null || m_onRecv == null)
            throw new Exception("CluaSocketHandler lua层必须实现函数");

        m_luaBuffer = this.luaHandler.GetTable<LuaTable>("m_buffer");
        m_ints = this.m_luaBuffer.GetTable<LuaTable>("m_ints");
        m_nums = this.m_luaBuffer.GetTable<LuaTable>("m_nums");
        m_strs = this.m_luaBuffer.GetTable<LuaTable>("m_strs");

        socketEx.Notifier.On(SocketEx.EnEvent.Connect, OnConnect);
        socketEx.Notifier.On(SocketEx.EnEvent.Close, OnClose);
        socketEx.Notifier.On(SocketEx.EnEvent.Recv, OnRecv);
        socketEx.Notifier.On(SocketEx.EnEvent.Accept, OnAccept);
        socketEx.SetRecvThreadHandle(OnRecvThreadHandle);

    }

    public void Accept(string ip, int port)
    {
        socketEx.Accept(ip, port);
    }

    public void Connect(string ip, int port, string param = null, float timeout = 8)
    {
        socketEx.Connect(ip, port, param, timeout);
    }
    
    public void Send(int id, byte[] bs ,int startIndex = 0, int len = -1)
    {
        socketEx.Send(id, bs, startIndex, len);
    }

    private void OnConnect(object param)
    {
        //needtodo
    }
    
    private void OnClose(object param)
    {
        //needtodo
    }
    
    private void OnRecv(object param)
    {
        //needtodo
    }
    
    private void OnAccept(object param)
    {
        //needtodo
    }
    
    Stopwatch sw = new Stopwatch();

    void OnLoop()
    {
        var count = m_msgs.Count;
        if (count == 0)
            return;

        var size = 0;
        long cost = 0;

        var i = 0;
        for (; i < count; i++)
        {
            sw.Restart();
            var cluaBuffer = m_msgs[i];
            size += cluaBuffer.len;

            cluaBuffer.WriteToLua(this.UseOptimize, m_ints, m_nums, m_strs);

            var id = cluaBuffer.id;
            cluaBuffer.Reset();
            _recvPool.Enqueue(cluaBuffer);
            try
            {
                if (m_onRecv.IsAlive && m_onRecv.GetLuaState() != null)
                {
                    m_onRecv.Call<LuaTable, int>(luaHandler, id);
                }
            }
            catch(Exception e)
            {
                Debug.LogException(e);
            }
            
            sw.Stop();
            cost += sw.ElapsedMilliseconds;

            if (cost > 20 && i >= -1)
            {
                i = i + 1;
                break;
            }
        }
        
        m_msgs.RemoveRange(0, i);
    }
    
    private CLuaBuffer _sendCLuaBuffer = new CLuaBuffer();
    private ByteArray _sendBuffer = new ByteArray(1024, false, true, false);
    private Lockqueue _recvPool = new Lockqueue();
    private ByteArray _zlibBuffer = new ByteArray(1024, false, true, false);


    public void SendEx(int id, int intsNeedLen, int numsNeedLen, int strsNeedLen)
    {
        try
        {
            if (false == NetMgr.s_opcodes.ContainsKey(id))
            {
                Debug.LogError("lua请求协议找不到， id：" + id);
                return;
            }

            //needtodo
        }
        catch (Exception e)
        {
            Debug.LogError("send error.msgId=" + id);
        }
    }
    
    object OnRecvThreadHandle(int id, ByteArray buffer, int len)
    {
        if(len > 1024 * 16)
            Debug.LogError("协议超过16k");
        if (NetMgr.s_opcodesForRecvs.ContainsKey(id))
        {
            //needtodo
            return null;
        }
        else
        {
            return null;
        }
    }
    
}