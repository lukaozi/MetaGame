using System;
using System.Net;
using System.Net.Sockets;

public class NetUtil
{
    static public void NewSocketByIp(string ip, int port, out Socket s, out IPEndPoint ipEndPoint)
    {
        IPAddress ipAddress = null;
        s = null;
        ipEndPoint = null;
        if (IPAddress.TryParse(ip, out ipAddress))
        {
            ipEndPoint = new IPEndPoint(ipAddress, port);
            if(ipEndPoint.AddressFamily == AddressFamily.InterNetwork)
                s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            else if(ipEndPoint.AddressFamily == AddressFamily.InterNetworkV6)
                s = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
            return;
        }
        else
        {
            try
            {
                IPHostEntry ipHost = Dns.GetHostEntry(ip);
                IPAddress address = ipHost.AddressList[0];

                if (address.AddressFamily == AddressFamily.InterNetwork)
                    s = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
                else if (address.AddressFamily == AddressFamily.InterNetworkV6)
                    s = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                return;
            }
            catch (Exception e)
            {
                if (IPAddress.TryParse(ip, out ipAddress))
                {
                    ipEndPoint = new IPEndPoint(ipAddress, port);
                    s = new Socket(AddressFamily.InterNetworkV6, SocketType.Stream, ProtocolType.Tcp);
                    return;
                }
            }
        }

        s = null;
        ipEndPoint = null;
    }
    
}