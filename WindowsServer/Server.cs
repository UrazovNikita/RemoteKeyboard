using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
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

        private class ExceptionClientUnconnected : Exception
        {
            public ExceptionClientUnconnected(string message)
                : base(message)
            { }
        }

        public static readonly int port = 16371;
        private static TcpListener _tcpServer;
        private static TcpClient _client;
        private static NetworkStream _stream;
        private static SendKeyParams[] _recievedStreamArray;
        private static CancellationTokenSource _tokenSource;
        public static bool IsWorked { get; private set; } = false;
        public static async void StartWork()
        {
            IsWorked = true;
            _tokenSource = new CancellationTokenSource();
            CancellationToken cancelToken = _tokenSource.Token;

            IPAddress localAddr = GetLocalIp();
            _tcpServer = new TcpListener(localAddr, port);
            await Task.Run(() =>
            {
                try
                {
                    _tcpServer.Start();
                    _client = _tcpServer.AcceptTcpClient();
                    _stream = _client.GetStream();

                    BinaryFormatter formatter = new BinaryFormatter();

                    while (_stream.CanRead)
                    {

                        if (_stream.DataAvailable)
                        {
                            if (_client.Connected)
                            {
                                _recievedStreamArray = (SendKeyParams[])formatter.Deserialize(_stream);
                                KeySenderMethods.SendKeysArray(_recievedStreamArray);
                                cancelToken.ThrowIfCancellationRequested();
                            }
                            else
                            {
                                throw new ExceptionClientUnconnected("not connected");
                            }
                        }
                        else
                        {
                            continue;
                        }

                    }
                }
                catch (ExceptionClientUnconnected ex)
                {
                    StopWork();
                }
            });

        }

        public static void StopWork()
        {
            IsWorked = false;
            try
            {
                if (_stream != null)
                {
                    _stream.Close();
                }
                if (_client != null)
                {
                    _client.Close();
                }
                if (_tcpServer != null)
                {
                    _tcpServer.Stop();
                }
                if (_recievedStreamArray != null)
                {
                    for (int i = 0; i < _recievedStreamArray.Length; i++)
                    {
                        _recievedStreamArray[i].flag = SendKeyParams.KEYEVENTF.KEYUP;
                    }
                    KeySenderMethods.SendKeysArray(_recievedStreamArray);
                }
            }
            finally
            {
                if (_tokenSource != null)
                {
                    _tokenSource.Cancel();
                }
            }
        }

    }
}