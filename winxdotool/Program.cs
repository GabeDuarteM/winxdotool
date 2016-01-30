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
                Win32.POINT p = new Win32.POINT();
                p.x = Convert.ToInt16(x);
                p.y = Convert.ToInt16(y);

                Win32.ClientToScreen(hWnd, ref p);
                Win32.SetCursorPos(p.x, p.y);

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
