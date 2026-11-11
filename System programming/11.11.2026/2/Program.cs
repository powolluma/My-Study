
using System;
using System.Threading;

class Program
{
    // Метод вызова таймера
    static void ShowTime(object? state)
    {
        Console.WriteLine($"Текущее время: {DateTime.Now}");
    }

    // Метод фонового потока
    static void BackgroundWork()
    {
        while (true)
        {
            Console.WriteLine("Фоновый поток работает");

            Thread.Sleep(300);
        }
    }

    static void Main(string[] args)
    {
        // Таймер
        Timer timer = new Timer(
            ShowTime,
            null,
            1000,
            2000
        );

        // Поток
        Thread backgroundThread = new Thread(BackgroundWork);

        // Поток становится фоновым
        backgroundThread.IsBackground = true;

        // Запуск потока
        backgroundThread.Start();

        Console.WriteLine("Нажмите Enter для выхода...");
        Console.ReadLine();

        // Остановка таймера
        timer.Dispose();
    }
}
