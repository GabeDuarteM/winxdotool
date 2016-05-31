// Created at: 31/05/2016 00:24

using System;
using System.Runtime.InteropServices;

namespace winxdotool
{
    public class Win32
    {
        public const uint MOUSEEVENTF_LEFTDOWN = 0x02;

        public const uint MOUSEEVENTF_LEFTUP = 0x04;

        [DllImport("user32.dll", SetLastError = true)]
        public static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, UIntPtr dwExtraInfo);

        //public const uint MOUSEEVENTF_RIGHTDOWN = 0x08;
        //public const uint MOUSEEVENTF_RIGHTUP = 0x10;

        [DllImport("user32.dll")]
        public static extern bool GetWindowRect(IntPtr hwnd, ref Rect rectangle);

        [DllImport("User32.Dll")]
        public static extern long SetCursorPos(int x, int y);

        [DllImport("User32.Dll")]
        public static extern bool ClientToScreen(IntPtr hWnd, ref POINT point);

        [StructLayout(LayoutKind.Sequential)]
        public struct POINT
        {
            public int x;

            public int y;
        }
    }
}
