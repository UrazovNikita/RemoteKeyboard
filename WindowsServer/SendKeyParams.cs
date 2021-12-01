namespace WindowsServer
{
    public class SendKeyParams
    {
        public InputSender.InputSender.ScanCodeShort key { get; set; }
        public InputSender.InputSender.KEYEVENTF flag { get; set; }
        public SendKeyParams(InputSender.InputSender.ScanCodeShort inputKey, InputSender.InputSender.KEYEVENTF inputFlag)
        {
            key = inputKey;
            flag = inputFlag;
        }
    }
}
