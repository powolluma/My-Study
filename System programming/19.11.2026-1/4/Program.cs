using System;
using System.Linq;
using System.Threading.Tasks;

namespace Task4
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = GenerateRandomArray(20, 1, 100);

            Console.WriteLine("Исходный массив:");
            Console.WriteLine(string.Join(", ", numbers));

            int min = 0;
            int max = 0;
            double average = 0;
            int sum = 0;

            Task[] tasks = new Task[4];

            // Поиск минимума
            tasks[0] = Task.Run(() =>
            {
                min = numbers.Min();
            });

            // Поиск максимума
            tasks[1] = Task.Run(() =>
            {
                max = numbers.Max();
            });

            // Среднее
            tasks[2] = Task.Run(() =>
            {
                average = numbers.Average();
            });

            // Сумма
            tasks[3] = Task.Run(() =>
            {
                sum = numbers.Sum();
            });
            Task.WaitAll(tasks);

            // Вывод
            Console.WriteLine($"Минимум: {min}");
            Console.WriteLine($"Максимум: {max}");
            Console.WriteLine($"Среднее: {average:F2}");
            Console.WriteLine($"Сумма: {sum}");
        }

        // Генерация случайных
        static int[] GenerateRandomArray(int size, int minVal, int maxVal)
        {
            Random random = new Random();
            int[] array = new int[size];

            for (int i = 0; i < size; i++)
            {
                array[i] = random.Next(minVal, maxVal + 1);
            }
            return array;
        }
    }
}
