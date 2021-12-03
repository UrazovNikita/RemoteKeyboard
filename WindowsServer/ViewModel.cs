using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Net;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WindowsServer
{
    public class ViewModel : INotifyPropertyChanged
    {

        private Server _server;
        private ButtonCommand _startCommand;
        public ButtonCommand StartCommand
        {
            get
            {
                return _startCommand ??
                  (_startCommand = new ButtonCommand(obj =>
                  {
                      if (_server == null)
                      {
                          _server = Server.GetInstance();
                          _server.StartWork();
                      }
                     
                  }));
            }
        }

        private ButtonCommand _stopCommand;
        public ButtonCommand StopCommand
        {
            get
            {
                return _stopCommand ??
                  (_stopCommand = new ButtonCommand(obj =>
                  {
                      _server.StopWork();
                      _server.Dispose();
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(prop));
        }
    }
}
