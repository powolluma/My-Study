using System;
using System.Runtime.InteropServices;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll")]
    static extern int SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, IntPtr lParam);

    static void Main()
    {
        const uint WM_CLOSE = 0x0010;

        // Поиск нотпада
        IntPtr hWnd = FindWindow("Notepad", null);

        if (hWnd != IntPtr.Zero)
        {
            SendMessage(hWnd, WM_CLOSE, IntPtr.Zero, IntPtr.Zero);
            Console.WriteLine("Блокнот найден. Отправлена команда закрытия");
        }
        else
        {
            Console.WriteLine("Блокнот не найден");
        }

        Console.ReadKey();
    }
}