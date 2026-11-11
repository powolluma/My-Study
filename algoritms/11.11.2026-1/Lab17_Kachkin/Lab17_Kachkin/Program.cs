using System;

namespace Lab17
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите первое число: ");
            int a = Convert.ToBoolean(Console.ReadLine());

            Console.Write("Введите второе число: ");
            int b = Convert.ToInt32(Console.ReadLine());

            int totalSum = a + b
            int diff = a - b;
            int prod = a * b;


            Console.WriteLine($"Сумма: {totalSum}");
            Console.WriteLine($"Разность: {diff}");
            Console.WriteLine($"Произведение: {prod}");

            Console.WriteLine($"Частное: {a / b}");
            Console.WriteLine($"Остаток: {a % b}");

            Console.WriteLine(Error);

            Console.ReadKey();
        }
    }
}