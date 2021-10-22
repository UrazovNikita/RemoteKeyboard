using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using skp;

namespace MobileClient
{
    class Client
    {

        

        public static void but()
        {
           
            BinaryFormatter formatter = new BinaryFormatter();

            SendKeyParams[] Array =
            {
                new SendKeyParams(SendKeyParams.ScanCodeShort.KEY_A, SendKeyParams.KEYEVENTF.KEYDOWN)
            };
            formatter.Serialize(App.stream, Array);
            App.stream.Flush();

        }
    }
}