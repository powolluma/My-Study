using System;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        int[] numbers = new int[1000];

        ManualResetEventSlim finish = new ManualResetEventSlim(false);

        int max = 0;
        int min = 0;
        double average = 0;

        // Генерация чисел
        Thread generator = new Thread(() =>
        {
            Random random = new Random();

            for (int i = 0; i < 1000; i++)
            {
                numbers[i] = random.Next(0, 5001);
            }

            Console.WriteLine("Числа сгенерированы");

            finish.Set();
        });

        // Поиск максимума
        Thread thread1 = new Thread(() =>
        {
            finish.Wait();

            max = numbers.Max();

            Console.WriteLine("Максимум: " + max);
        });

        // Поиск минимума
        Thread thread2 = new Thread(() =>
        {
            finish.Wait();

            min = numbers.Min();

            Console.WriteLine("Минимум: " + min);
        });

        // Среднее арифметического
        Thread thread3 = new Thread(() =>
        {
            finish.Wait();

            average = numbers.Average();

            Console.WriteLine("Среднее: " + average);
        });

        thread1.Start();
        thread2.Start();
        thread3.Start();

        generator.Start();

        generator.Join();
        thread1.Join();
        thread2.Join();
        thread3.Join();

        Console.WriteLine("Работа программы завершена");

        finish.Dispose();
    }
}