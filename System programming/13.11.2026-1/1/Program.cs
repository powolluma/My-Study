using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread monitor = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine("Текущее время: " + DateTime.Now);
                Thread.Sleep(1000);
            }
        });

        monitor.IsBackground = true;
        monitor.Start();

        for (int i = 1; i <= 3; i++)
        {
            ThreadPool.QueueUserWorkItem(TaskWork, i);
        }

        Thread.Sleep(5000);

        Console.WriteLine("Главный поток завершил работу");
    }

    static void TaskWork(object state)
    {
        int taskId = (int)state;

        Console.WriteLine(
            $"Задача №{taskId} выполняется в потоке {Thread.CurrentThread.ManagedThreadId}");

        Thread.Sleep(2000);

        Console.WriteLine($"Задача №{taskId} завершена");
    }
}