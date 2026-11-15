using System;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.WriteLine("Введите числа через пробел:");

        int[] numbers = Console.ReadLine()!
            .Split(' ', StringSplitOptions.RemoveEmptyEntries)
            .Select(int.Parse)
            .ToArray();

        Console.Write("Введите число для поиска: ");
        int searchNumber = int.Parse(Console.ReadLine()!);

        object locker = new object();

        ManualResetEventSlim finish = new ManualResetEventSlim(false);

        // Поток сортировки массива
        Thread thread1 = new Thread(() =>
        {
            lock (locker)
            {
                Array.Sort(numbers);

                Console.WriteLine("Отсортированный массив:");

                Console.WriteLine(string.Join(", ", numbers));
            }

            finish.Set();
        });

        // Поток поиска числа
        Thread thread2 = new Thread(() =>
        {
            finish.Wait();

            lock (locker)
            {
                int index = Array.BinarySearch(numbers, searchNumber);

                if (index >= 0)
                {
                    Console.WriteLine("Число найдено");
                }
                else
                {
                    Console.WriteLine("Число не найдено");
                }
            }
        });

        thread2.Start();
        thread1.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Работа завершена");

        finish.Dispose();
    }
}