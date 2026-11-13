using System;
using System.Threading;

class Program
{
    static int ticketsLeft = 100;

    static void Main()
    {
        Thread[] cashiers = new Thread[5];

        for (int i = 0; i < 5; i++)
        {
            int cashierNumber = i + 1;

            cashiers[i] = new Thread(() =>
            {
                while (true)
                {
                    int current = Interlocked.CompareExchange(
                        ref ticketsLeft, 0, 0);

                    if (current <= 0)
                        break;

                    int remaining = current - 1;

                    int result = Interlocked.CompareExchange(
                        ref ticketsLeft, remaining, current);

                    if (result == current)
                    {
                        Console.WriteLine(
                            $"Кассир {cashierNumber} продал билет. Осталось: {remaining}");

                        Thread.Sleep(10);
                    }
                }
            });

            cashiers[i].Start();
        }

        foreach (Thread thread in cashiers)
        {
            thread.Join();
        }

        Console.WriteLine("Продажа билетов завершена");
        Console.WriteLine("Осталось билетов: " + ticketsLeft);
    }
}