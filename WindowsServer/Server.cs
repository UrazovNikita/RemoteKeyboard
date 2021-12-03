using System;
using System.Threading;
using System.Threading.Tasks;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsServer
{
    public class Server : IDisposable
    {
        private static Server _instance;
        private Server() {  }
        public static Server GetInstance()
        {
            if (_instance == null)
            {
                _instance = new Server();
            }
            return _instance;
        }
        public void Dispose()
        {
            if (_instance != null)
            {
                _instance.Dispose();
            }
        }
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
        public int GetPort()
        {
            return _port;
        }

        private class ExceptionClientDisconnected : Exception
        {
            public ExceptionClientDisconnected(string message)
                : base(message)
            { }
        }

        private int _port = 16371;
        private TcpListener _tcpServer;
        private TcpClient _client;
        private NetworkStream _stream;
        private SendKeyParams[] _recievedStreamArray;
        private CancellationTokenSource _tokenSource;
        private InputSenderMethods _input = new InputSenderMethods();

        //public static bool AskConnection()
        //{
        //    _stream.Write()
        //}

        public bool IsWork { get; private set; } = false;
        public async void StartWork()
        {
            IsWork = true;

            _tokenSource = new CancellationTokenSource();
            CancellationToken cancelToken = _tokenSource.Token;

            IPAddress localAddr = GetLocalIp();
            _tcpServer = new TcpListener(localAddr, _port);
            await Task.Run(() =>
            {
                try
                {
                    _tcpServer.Start();
                    _client = _tcpServer.AcceptTcpClient();
                    _stream = _client.GetStream();                    

                    while (_stream.CanRead)
                    {
                        if (_stream.DataAvailable)
                        {
                            if (_client.Connected)
                            {
                                BinaryFormatter formatter = new BinaryFormatter();
                                _recievedStreamArray = (SendKeyParams[])formatter.Deserialize(_stream);
                                _input.SendRecievedArray(_recievedStreamArray);
                                cancelToken.ThrowIfCancellationRequested();
                            }
                            else
                            {
                                throw new ExceptionClientDisconnected("not connected");
                            }
                        }
                        else
                        {
                            continue;
                        }
                    }
                }
                catch (ExceptionClientDisconnected ex)
                {
                    StopWork();
                }
            });
        }

        public void StopWork()
        {
            IsWork = false;

            if (_tcpServer != null)
            {
                if (_client != null)
                {
                    if (_stream != null)
                    {
                        _stream.Close();
                    }
                    _client.Close();
                }
                _tcpServer.Stop();
            }

            if (_recievedStreamArray != null)
            {
                for (int i = 0; i < _recievedStreamArray.Length; i++)
                {
                    _recievedStreamArray[i].flag = InputSender.InputSender.KEYEVENTF.KEYUP;
                }
                _input.SendRecievedArray(_recievedStreamArray);
            }

            if (_tokenSource != null)
            {
                _tokenSource.Cancel();
            }
        }
    }

}

