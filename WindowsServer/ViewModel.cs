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
        Server server;
        private ButtonCommand startCommand;
        public ButtonCommand StartCommand
        {
            get
            {
                return startCommand ??
                  (startCommand = new ButtonCommand(obj =>
                  {
                      server=Server.GetInstance();
                      server.StartWork();
                  }));
            }           
        }

        private ButtonCommand stopCommand;
        public ButtonCommand StopCommand
        {
            get
            {
                return stopCommand ??
                  (stopCommand = new ButtonCommand(obj =>
                  {
                      server.StopWork();
                      server.Dispose();                      
                  }));
            }            
        }

        //public Phone SelectedPhone
        //{
        //    get { return selectedPhone; }
        //    set
        //    {
        //        selectedPhone = value;
        //        OnPropertyChanged("SelectedPhone");
        //    }
        //}       

        public event PropertyChangedEventHandler PropertyChanged;
        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }
    }
}
