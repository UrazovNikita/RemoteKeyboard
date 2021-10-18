using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Net.NetworkInformation;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;
using System.Runtime.Serialization;
using skp;

namespace WindowsServer
{
    public static class Server
    {
        public static IPAddress GetLocalIp()
        {
            IPAddress localIP;
            Socket socket = new Socket(AddressFamily.InterNetwork, SocketType.Dgram, ProtocolType.IP);

            try
            {
                socket.Connect("8.8.8.8", 65530);
                IPEndPoint endPoint = socket.LocalEndPoint as IPEndPoint;
                localIP = endPoint.Address;
            }
            catch
            {
                localIP = IPAddress.None;
            }
            return localIP;
        }        

        public static readonly int port = 16371;
        public async static void StartWork()
        {
            IPAddress localAddr =/* IPAddress.Loopback;*/ GetLocalIp();
            TcpListener tcpServer = new TcpListener(localAddr, port);
            tcpServer.Start();
            while (true)
            {
                TcpClient client = tcpServer.AcceptTcpClient();
                NetworkStream stream = client.GetStream();
                BinaryFormatter formatter = new BinaryFormatter();

                SendKeyParams[] gotArray;
                
                {
                    while (stream.CanRead)
                    {
                        //stream.ReadTimeout = 10;

                        gotArray = (SendKeyParams[])formatter.Deserialize(stream);
                        KeySenderMethods.SendKeysArray(gotArray);
                    }
                }
                //StopWork(stream, client, gotArray);
            }           
        }     
        public static void StopWork(NetworkStream stream, TcpClient client, SendKeyParams[] sendKeysUp)
        {
            stream.Close();
            client.Close();
        }
           
    }
}


    