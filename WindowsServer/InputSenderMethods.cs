using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace WindowsServer
{
    public class InputSenderMethods 
    {
        /// <summary>
        /// Sends key event for single key on focussed app
        /// </summary>
        /// <param name="sendKeyParams"></param>
        public void SendKey(SendKeyParams sendKeyParams)
        {
            InputSender.InputSender.LPINPUT[] inputs = new InputSender.InputSender.LPINPUT[]
              {
                       new InputSender.InputSender.LPINPUT
                       {
                           type = InputSender.InputSender.InputType.INPUT_KEYBOARD,
                           U = new InputSender.InputSender.InputUnion
                           {
                               ki = new InputSender.InputSender.KEYBDINPUT
                               {
                                   wVk = 0,
                                   wScan = sendKeyParams.key,
                                   dwFlags =sendKeyParams.flag|InputSender.InputSender.KEYEVENTF.SCANCODE,
                                   dwExtraInfo = InputSender.InputSender.GetMessageExtraInfo(),
                               }
                           }
                       }
              };

            InputSender.InputSender.SendInput((uint)inputs.Length, inputs, InputSender.InputSender.LPINPUT.Size);
        }

        /// <summary>
        /// Sends key event for single key on focussed app
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        public void SendKey(InputSender.InputSender.ScanCodeShort key, InputSender.InputSender.KEYEVENTF flag)
        {
            InputSender.InputSender.LPINPUT[] inputs = new InputSender.InputSender.LPINPUT[]
            {
                       new InputSender.InputSender.LPINPUT
                       {
                           type = InputSender.InputSender.InputType.INPUT_KEYBOARD,
                           U = new InputSender.InputSender.InputUnion
                           {
                               ki = new InputSender.InputSender.KEYBDINPUT
                               {
                                   wVk = 0,
                                   wScan = key,
                                   dwFlags = flag|InputSender.InputSender.KEYEVENTF.SCANCODE,
                                   dwExtraInfo = InputSender.InputSender.GetMessageExtraInfo(),
                               }
                           }
                       }
            };
            InputSender.InputSender.SendInput((uint)inputs.Length, inputs, InputSender.InputSender.LPINPUT.Size);
        }

        public void SendRecievedArray(SendKeyParams[] received)
        {
            foreach (SendKeyParams i in received)
            {
                SendKey(i);
            }
        }
    }
}


