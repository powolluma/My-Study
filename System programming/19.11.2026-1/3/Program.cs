using System;
using System.Threading.Tasks;

namespace Task3
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите начало диапазона: ");
            int start = int.Parse(Console.ReadLine());

            Console.Write("Введите конец диапазона: ");
            int end = int.Parse(Console.ReadLine());

            Task<int> primeTask = Task.Run(() => PrintAndCountPrimes(start, end));
            primeTask.Wait();

            int primeCount = primeTask.Result;
            Console.WriteLine($"Простых чисел: {primeCount}");
        }

        // Вывод кол-ва простых чисел и просто чисел
        static int PrintAndCountPrimes(int start, int end)
        {
            int count = 0;

            for (int i = start; i <= end; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write($"{i} ");
                    count++;
                }
            }
            return count;
        }

        // Проверка на простое число
        static bool IsPrime(int number)
        {
            if (number < 2)
            {
                return false;
            }

            for (int i = 2; i * i <= number; i++)
            {
                if (number % i == 0)
                {
                    return false;
                }
            }
            return true;
        }
    }
}
