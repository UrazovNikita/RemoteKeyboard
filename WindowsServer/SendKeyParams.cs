using System;

namespace WindowsServer
{
    [Serializable]
    public class SendKeyParams
    {
        public SendKeyParams(KeySender.ScanCodeShort inputKey, KeySender.KEYEVENTF inputFlag)
        {
            key = inputKey;
            flag = inputFlag;
        }
        public KeySender.ScanCodeShort key { get; private set; }
        public KeySender.KEYEVENTF flag { get; private set; }
    }
}
