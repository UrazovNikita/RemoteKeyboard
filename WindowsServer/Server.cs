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
        private static CancellationTokenSource _tokenSource;
        private static TcpListener _tcpServer;
        private static TcpClient _client;
        private static NetworkStream _stream;
        private static SendKeyParams[] _recievedStreamArray;
        public async static void StartWork()
        {
            _tokenSource = new CancellationTokenSource();
            CancellationToken cancelToken = _tokenSource.Token;
            IPAddress localAddr = GetLocalIp();
            _tcpServer = new TcpListener(localAddr, port);
            try
            {
                await Task.Run(() =>
                {
                    _tcpServer.Start();
                    while (true)
                    {
                        _client = _tcpServer.AcceptTcpClient();
                        _stream = _client.GetStream();
                        BinaryFormatter formatter = new BinaryFormatter();                        
                        {
                            while (_stream.CanRead)
                            {
                                _stream.ReadTimeout = 1;
                                _recievedStreamArray = (SendKeyParams[])formatter.Deserialize(_stream);
                                KeySenderMethods.SendKeysArray(_recievedStreamArray);
                            }
                        }
                    }
                });
            }
            catch
            {

            }           
        }    
       
        public static void StopWork(TcpListener tcpServer, TcpClient client, NetworkStream stream, SendKeyParams[] recievedStreamArray)
        {
            stream.Close();
            client.Close();
            tcpServer.Stop();
            for(int i=0; i<recievedStreamArray.Length; i++)
            {
                recievedStreamArray[i].flag = SendKeyParams.KEYEVENTF.KEYUP;
            }
            KeySenderMethods.SendKeysArray(recievedStreamArray);
        }
           
    }
}


    