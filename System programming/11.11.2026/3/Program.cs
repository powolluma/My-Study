
using System;
using System.Threading;

class Program
{
    // Общая переменная
    static int counter = 0;

    // Общий объект для синхронизации
    private static readonly object locker = new object();

    // Метод увеличения счетчика
    static void IncrementCounter()
    {
        for (int i = 0; i < 100_000; i++)
        {
            lock (locker)
            {
                counter++;
            }
        }
    }

    static void Main(string[] args)
    {
        // Массив из 5 потоков
        Thread[] threads = new Thread[5];

        // Запуск потоков
        for (int i = 0; i < 5; i++)
        {
            threads[i] = new Thread(IncrementCounter);
            threads[i].Start();
        }

        foreach (Thread thread in threads)
        {
            thread.Join();
        }

        // Вывод
        Console.WriteLine($"Итоговое значение: {counter}");
    }
}
