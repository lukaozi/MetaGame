using System;
using System.Net.Sockets;
using System.Threading;
using UnityEngine;

public class SocketRecvThread
{
    public SocketEx m_socketEx;
    public Socket m_socket;
    public Thread m_thread;
    public SocketEx.onRecvThreadHandle m_recvThreadHandle;
    public Lockqueue m_msgQueue;
    public bool m_isClose = false;
    public bool m_isFixedCompress;
    private Action<object, int> m_onMainThreadRecv;
    private int Sleep_Time = 15;
    private ByteArray buffer;
    private int curPackLen = -1;//当前要读的包的长度， -1表示还没读包长
    private int curId = -1;
    
    public SocketRecvThread(SocketEx socketEx, Socket socket, SocketEx.onRecvThreadHandle onRecvThreadHandle, Lockqueue msgQueue, bool isFixedCompress)
    {
        m_socketEx = socketEx;
        m_isFixedCompress = isFixedCompress;
        m_socket = socket;
        m_msgQueue = msgQueue;
        buffer = new ByteArray(1024 * 4, false, m_isFixedCompress, false);
        m_thread = new Thread(new ThreadStart(run));
        m_thread.Priority = System.Threading.ThreadPriority.Highest;
        m_thread.IsBackground = true;
        m_thread.Start();
        m_onMainThreadRecv = this.OnMainThreadRecv;
    }

    private long lastTime;

    public void run()
    {
        while (!m_isClose)
        {
            try
            {
                if (!m_socket.Connected)
                {
                    TellMainThreadError("m_socket.connected = false");
                    break;
                }

                int available = m_socket.Available;
                if (available == 0)
                {
                    Thread.Sleep(Sleep_Time);
                    continue;
                }

                if (available > ByteArray.Max_Buffer_Size)
                    available = ByteArray.Max_Buffer_Size;
                buffer.EnsureCapacity(available, ByteArray.enEnsureMoveToFirst.forceEnsureMove);
                SocketError error = SocketError.Success;
                int length = m_socket.Receive(buffer.Buffer, buffer.WritePos, available, SocketFlags.None, out error);
                buffer.WritePos += length;
                if (error != SocketError.Success)
                {
                    TellMainThreadError("m_socket.Receive error:" + error);
                    return;
                }

                if (length > available)
                {
                    TellMainThreadError("m_socket.Receive 长度 大于 available");
                    return;
                }

                if (length == 0)
                {
                    TellMainThreadError("socket shutdown gracefully:" + error);
                    return;
                }

                ReadDatas();
                Thread.Sleep(Sleep_Time);
            }
            catch (Exception e)
            {
                if (curId != -1 && curPackLen != -1)
                {
                    TellMainThreadError("recv error.msgId=" + curId + ".\n" + e.Message + "\n" + e.StackTrace);
                }
                else
                {
                    TellMainThreadError("recv error." + e.Message + "\n" + e.StackTrace);
                }
                throw;
            }
        }
    }

    private void ReadDatas()
    {
        while (true)
        {
            if (curPackLen == -1)
            {
                if (buffer.Remain < 4)
                {
                    return;
                }

                curPackLen = buffer.ReadInt(true);
                if(curPackLen > ByteArray.Max_Buffer_Size)
                    throw new Exception("curPacklen toobig:" + curPackLen);
            }

            //包格式：包长 + 0 + 协议号（变长）+ 数据，
            if (buffer.Remain < curPackLen)
                return;

            int beginPos = buffer.ReadPos;
            int endPos = (beginPos + curPackLen);
            buffer.ReadInt();
            curId = buffer.ReadInt();
            
            try
            {
                object oneData = null;
                if (m_recvThreadHandle != null)
                    oneData = m_recvThreadHandle(curId, buffer, endPos - buffer.ReadPos);
                if (oneData == null)
                    oneData = buffer.ReadBytes(endPos - buffer.ReadPos);
                TellMainThreadRecv(curId, oneData);
            }
            catch(System.Exception e)
            {
                Debug.LogError("recv error2.msgId=" + curId + ".\n" + e.Message + "\n" + e.StackTrace);
            }

            if (buffer.ReadPos < endPos)
                buffer.ReadPos = endPos;
            else if(buffer.ReadPos > endPos)
            {
                 throw new Exception("序列化异常 buffer.ReadPos > endPos");//后端删了字段
            }

            curPackLen = -1;
            curId = -1;
        }
    }

    public void TellMainThreadError(string error)
    {
        m_msgQueue.Needcall(OnMainThreadError, error);//主线程调用
    }

    public void OnMainThreadError(object param, int intParam)
    {
        if (m_socketEx.State != SocketEx.EnState.Connected || m_socketEx.Socket != this.m_socket ||
            m_msgQueue != m_socketEx.MsgQueue)
            return;
        m_socketEx.Close(SocketEx.EnCloseType.NetError, "SocketRecvThread error:" + param);
    }

    public void TellMainThreadRecv(int id, object oneData)
    {
        m_msgQueue.Needcall(m_onMainThreadRecv, oneData, id);
    }

    public void OnMainThreadRecv(object oneData, int id)
    {
        if (m_socketEx.State != SocketEx.EnState.Connected || m_socketEx.Socket != this.m_socket ||
            m_msgQueue != m_socketEx.MsgQueue)
            return;
        m_socketEx.OnRecv(id, oneData);
    }
    
}