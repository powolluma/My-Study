using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Введите начало диапазона: ");
        int start = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите конец диапазона: ");
        int end = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите количество потоков: ");
        int count = Convert.ToInt32(Console.ReadLine());

        if (start > end || count <= 0)
        {
            Console.WriteLine("Неверные данные");
            return;
        }

        int total = end - start + 1;
        count = Math.Min(count, total);

        Thread[] threads = new Thread[count];

        for (int i = 0; i < count; i++)
        {
            int from = start + i * total / count;
            int to = start + (i + 1) * total / count - 1;

            threads[i] = new Thread(() =>
            {
                for (int j = from; j <= to; j++)
                {
                    Console.WriteLine(
                        $"Поток {Thread.CurrentThread.ManagedThreadId}: {j}");
                }
            });

            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }
    }
}