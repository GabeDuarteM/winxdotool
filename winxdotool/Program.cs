// Created at: 31/05/2016 00:24

using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace winxdotool
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            if (args.Length < 2)
            {
                ShowUsage();
            }

            string windowName = args[0];

            IntPtr hWnd = IntPtr.Zero;

            foreach (Process pList in Process.GetProcesses())
            {
                if (pList.MainWindowTitle != windowName)
                {
                    continue;
                }
                hWnd = pList.MainWindowHandle;
                break;
            }

            Rect hWndRect = new Rect();

            Win32.GetWindowRect(hWnd, ref hWndRect);

            if (hWnd.ToInt32() == 0)
            {
                Console.WriteLine("Warning! Cannot find Window!");
                return;
            }

            string action = args[1].ToLower();
            switch (action)
            {
                case "mousemove":
                    if (args.Length != 4)
                    {
                        Console.WriteLine("Mousemove needs 4 parameters!");
                        Environment.Exit(-1);
                    }

                    //string x = args[2];
                    //string y = args[3];

                    //var screenWidth = 1920;
                    //var screenHeight = 1080;

                    //// Mickey X coordinate
                    //var mic_x = (int) Math.Round(Convert.ToInt32(x) * 65536.0 / screenWidth);
                    //// Mickey Y coordinate
                    //var mic_y = (int) Math.Round(Convert.ToInt32(y) * 65536.0 / screenHeight);

                    // 0x0001 | 0x8000: Move + Absolute position
                    //Win32.mouse_event(0x0001 | 0x8000, Convert.ToUInt32(mic_x), Convert.ToUInt16(mic_y), 0, UIntPtr.Zero);

                    var p = new Win32.POINT
                    {
                        x = Convert.ToInt16(hWndRect.Left),
                        y = Convert.ToInt16(hWndRect.Bottom)
                    };

                    Win32.ClientToScreen(hWnd, ref p);
                    Win32.SetCursorPos(p.x / (Screen.AllScreens.Length * 2), p.y / 2); // Set the position based on the window found via title. // Screen.AllScreens.Length to get the correct position if there are multiple screens

                    break;
                case "click":
                    Win32.mouse_event(Win32.MOUSEEVENTF_LEFTDOWN, 0, 0, 0, UIntPtr.Zero);
                    Win32.mouse_event(Win32.MOUSEEVENTF_LEFTUP, 0, 0, 0, UIntPtr.Zero);
                    break;
                default:
                    Console.WriteLine("unknown action :" + action);
                    break;
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
