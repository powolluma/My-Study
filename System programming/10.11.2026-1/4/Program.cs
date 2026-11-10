using System;
using System.Runtime.InteropServices;
using System.Threading;

class Program
{
    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern IntPtr FindWindow(string lpClassName, string lpWindowName);

    [DllImport("user32.dll", CharSet = CharSet.Unicode)]
    static extern IntPtr SendMessage(IntPtr hWnd, uint Msg, IntPtr wParam, string lParam);

    static void Main()
    {
        const uint WM_SETTEXT = 0x000C;

        // Поиск нотпада
        IntPtr hWnd = FindWindow("Notepad", null);

        if (hWnd == IntPtr.Zero)
        {
            Console.WriteLine("Блокнот не найден");
            return;
        }

        Console.WriteLine("Блокнот найден. Обновление времени");

        // Обновление заголовка
        while (true)
        {
            string time = "Текущее время: " + DateTime.Now.ToString("HH:mm:ss");

            SendMessage(hWnd, WM_SETTEXT, IntPtr.Zero, time);

            Thread.Sleep(1000);
        }
    }
}