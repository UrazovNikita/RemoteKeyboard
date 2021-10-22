using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using skp;

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
                                   wScan = (KeySender.ScanCodeShort)sendKeyParams.key,
                                   dwFlags = (KeySender.KEYEVENTF)sendKeyParams.flag|KeySender.KEYEVENTF.SCANCODE,
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
        public static void SendKey(SendKeyParams.ScanCodeShort key, SendKeyParams.KEYEVENTF flag)
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
                                   wScan = (KeySender.ScanCodeShort)key,
                                   dwFlags = (KeySender.KEYEVENTF)flag|KeySender.KEYEVENTF.SCANCODE,
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

