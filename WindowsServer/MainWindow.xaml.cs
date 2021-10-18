using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Net;
using System.Net.Sockets;
using System.Threading;
using System.IO;
using skp;

namespace WindowsServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
       


        public MainWindow()
        {
            //IPAddress ipAdress;
            //ipAdress = Server.GetLocalIp();
            //string ipAdressString = Server.GetLocalIp().ToString();
            //if (ipAdress == IPAddress.None)
            //{
            //    ipAdressString = "undefined";
            //}
            //HostInfo.Content = $"Host name: {Dns.GetHostName()}\nHost adress: {ipAdressString}";
      
            InitializeComponent();
        }




        private void ServerStart_Click(object sender, RoutedEventArgs e)
        {
            Task server = new Task(Server.StartWork);
            server.Start();

        }
    }
}

