using System;
using System.Threading.Tasks;

namespace Task2
{
    class Program
    {
        static void Main(string[] args)
        {
            Task primeTask = Task.Run(() => PrintPrimeNumbers(0, 1000));
            primeTask.Wait();
            Console.WriteLine("Задача завершена");
        }

        // Вывод простых чисел
        static void PrintPrimeNumbers(int start, int end)
        {
            for (int i = start; i <= end; i++)
            {
                if (IsPrime(i))
                {
                    Console.Write($"{i} ");
                }
            }
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
