using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace winxdotool
{
    class Program
    {
        static void Main(string[] args)
        {
            if(args.Length<2)
            {
                ShowUsage();
            }

            string windowName = args[0];

            IntPtr hWnd = (IntPtr)Win32.FindWindow(null, windowName);
            if(hWnd.ToInt32()==0)
            {
                Console.WriteLine("Warning! Cannot find Window!");
            }

            string action = args[1].ToLower();
            if(action == "mousemove")
            {
                if(args.Length != 4)
                {
                    Console.WriteLine("Mousemove needs 4 parameters!");
                    Environment.Exit(-1);
                }

                string x = args[2];
                string y = args[3];


                int screenWidth = 1920;
                int screenHeight = 1080;

                // Mickey X coordinate
                int mic_x = (int)System.Math.Round(Convert.ToInt32(x) * 65536.0 / screenWidth);
                // Mickey Y coordinate
                int mic_y = (int)System.Math.Round(Convert.ToInt32(y) * 65536.0 / screenHeight);

                // 0x0001 | 0x8000: Move + Absolute position
                Win32.mouse_event(0x0001 | 0x8000, Convert.ToUInt32(mic_x), Convert.ToUInt16(mic_y), 0, UIntPtr.Zero);

            }
            else if (action == "click")
            {

                Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                Win32.mouse_event(Win32.MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
            } else
            {
                Console.WriteLine("unknown action :" + action);
            }

        }

        private static void ShowUsage()
        {
            Console.WriteLine("Winxdotool usage:");
            Console.WriteLine("winxdotool \"WindowName\" mousemove 100 100");
            Console.WriteLine("winxdotool \"WindowName\" click 100 100");
            Environment.Exit(0);
        }
    }
}
