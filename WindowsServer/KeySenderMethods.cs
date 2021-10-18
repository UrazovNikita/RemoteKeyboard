using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WindowsServer
{
    partial class KeySenderMethods
    {
        /// <summary>
        /// Sends key event for single key on focussed app
        /// </summary>
        /// <param name="sendKeyParams"></param>
        public static void SendKey(SendKeyParams sendKeyParams)
        {
            KeySender.LPINPUT[] inputs = new KeySender.LPINPUT[]
            {
                       new KeySender.LPINPUT
                       {
                           type = KeySender.InputType.INPUT_KEYBOARD,
                           U = new KeySender.InputUnion
                           {
                               ki = new KeySender.KEYBDINPUT
                               {
                                   wVk = 0,
                                   wScan = sendKeyParams.key,
                                   dwFlags = sendKeyParams.flag|KeySender.KEYEVENTF.SCANCODE,
                                   dwExtraInfo = KeySender.GetMessageExtraInfo(),
                               }
                           }
                       }
            };
            KeySender.SendInput((uint)inputs.Length, inputs, KeySender.LPINPUT.Size);
        }

        /// <summary>
        /// Sends key event for single key on focussed app
        /// </summary>
        /// <param name="key"></param>
        /// <param name="flag"></param>
        public static void SendKey(KeySender.ScanCodeShort key, KeySender.KEYEVENTF flag)
        {
            KeySender.LPINPUT[] inputs = new KeySender.LPINPUT[]
            {
                       new KeySender.LPINPUT
                       {
                           type = KeySender.InputType.INPUT_KEYBOARD,
                           U = new KeySender.InputUnion
                           {
                               ki = new KeySender.KEYBDINPUT
                               {
                                   wVk = 0,
                                   wScan = key,
                                   dwFlags = flag|KeySender.KEYEVENTF.SCANCODE,
                                   dwExtraInfo = KeySender.GetMessageExtraInfo(),
                               }
                           }
                       }
            };
            KeySender.SendInput((uint)inputs.Length, inputs, KeySender.LPINPUT.Size);
        }
        public static void SendKeysArray(SendKeyParams[] received)
        {
            foreach (SendKeyParams i in received)
            {
                SendKey(i);
            }
        }
    }
}

