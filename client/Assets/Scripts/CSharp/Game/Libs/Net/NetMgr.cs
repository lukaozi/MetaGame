using System.Collections.Generic;

public class NetMgr : Singleton<NetMgr>
{
    public List<SocketEx> m_connectingSockets = new List<SocketEx>();
    public List<SocketEx> m_sockets = new List<SocketEx>();
    
    public static Dictionary<int, int[]> s_opcodes = new Dictionary<int, int[]>();
    public static Dictionary<int, int[]> s_opcodesForRecvs = new Dictionary<int, int[]>();
    
    public static HashSet<int> s_compIds = new HashSet<int>();


    public void OnSocketConnecting(SocketEx socket)
    {
        m_sockets.Remove(socket);
        m_connectingSockets.Remove(socket);
        m_connectingSockets.Add(socket);
    }
    
    public void OnSocketConnected(SocketEx socket)
    {
        m_sockets.Remove(socket);
        m_connectingSockets.Remove(socket);
        m_sockets.Add(socket);
    }

    public void OnSocketClose(SocketEx socket)
    {
        m_sockets.Remove(socket);
        m_connectingSockets.Remove(socket);
    }
    
}