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
using System.Windows.Forms;
using System.Drawing;

namespace WindowsServer
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        //IPAddress ipAdress;
        //ipAdress = Server.GetLocalIp();
        //string ipAdressString = Server.GetLocalIp().ToString();
        //if (ipAdress == IPAddress.None)
        //{
        //    ipAdressString = "undefined";
        //}
        //HostInfo.Content = $"Host name: {Dns.GetHostName()}\nHost adress: {ipAdressString}";

        private NotifyIcon notifyIcon = new NotifyIcon();

        System.Windows.Controls.ContextMenu menu;
        public MainWindow()
        {
            InitializeComponent();
            notifyIcon.Icon = new Icon("key1.ico");
            menu = getMenu();
        }
        private System.Windows.Controls.ContextMenu getMenu()
        {
            var menu = new System.Windows.Controls.ContextMenu();
            menu.Items.Add(new System.Windows.Controls.MenuItem { Header = "first" });
            menu.Items.Add(new System.Windows.Controls.MenuItem { Header = "second" });
            var mnuexit = new System.Windows.Controls.MenuItem { Header = "Exit" };
            mnuexit.Click += (sndr, args) => System.Windows.Application.Current.Shutdown();
            menu.Items.Add(mnuexit);
            return menu;

        }


        private void _buttonServerStart_Click(object sender, RoutedEventArgs e)
        {
            Server.StartWork();
        }

        private void _buttonCancle_Click(object sender, RoutedEventArgs e)
        {
            Server.StopWork();

        }

        private void _buttonToTray_Click(object sender, RoutedEventArgs e)
        {

            notifyIcon.Visible = true;
            notifyIcon.DoubleClick += (sndr, args) =>
            {
                this.Show();
                this.WindowState = WindowState.Normal;
            };
            notifyIcon.Click += (sndr, args) =>
            {
                if ((args as System.Windows.Forms.MouseEventArgs).Button == MouseButtons.Right)
                    menu.IsOpen = true;
            };

            this.Hide();
        }
    }
}


