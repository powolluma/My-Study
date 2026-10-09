using System;

class Program
{
    static void Main()
    {
        while (true)
        {
            Console.WriteLine("\nКАЛЬКУЛЯТОР");
            Console.WriteLine("1. Сложение");
            Console.WriteLine("2. Вычитание");
            Console.WriteLine("3. Умножение");
            Console.WriteLine("4. Деление");
            Console.WriteLine("5. Возведение в степень");
            Console.WriteLine("6. Квадратный корень");
            Console.WriteLine("7. Факториал");
            Console.WriteLine("0. Выход");
            Console.Write("Выберите операцию: ");
            string choice = Console.ReadLine();

            if (choice == "0") break;
            if (choice == "7")
            {
                Console.Write("Введите целое число: ");
                if (!int.TryParse(Console.ReadLine(), out int n) || n < 0 || n > 12)
                {
                    Console.WriteLine("Введите целое число от 0 до 12.");
                    continue;
                }
                Console.WriteLine($"Результат: {Factorial(n)}");
                continue;
            }

            if (choice == "6")
            {
                Console.Write("Введите число: ");
                if (!double.TryParse(Console.ReadLine(), out double x) || x < 0)
                {
                    Console.WriteLine("Введите неотрицательное число.");
                    continue;
                }
                Console.WriteLine($"Результат: {Math.Sqrt(x)}");
                continue;
            }

            if (choice is not ("1" or "2" or "3" or "4" or "5"))
            {
                Console.WriteLine("Нет такой операции.");
                continue;
            }

            Console.Write("Введите первое число: ");
            if (!double.TryParse(Console.ReadLine(), out double a))
            {
                Console.WriteLine("Некорректное число.");
                continue;
            }
            Console.Write("Введите второе число: ");
            if (!double.TryParse(Console.ReadLine(), out double b))
            {
                Console.WriteLine("Некорректное число.");
                continue;
            }

            switch (choice)
            {
                case "1": Console.WriteLine($"Результат: {a + b}"); break;
                case "2": Console.WriteLine($"Результат: {a - b}"); break;
                case "3": Console.WriteLine($"Результат: {a * b}"); break;
                case "4":
                    if (b == 0) Console.WriteLine("На ноль делить нельзя.");
                    else Console.WriteLine($"Результат: {a / b}");
                    break;
                case "5": Console.WriteLine($"Результат: {Math.Pow(a, b)}"); break;
            }
        }
    }

    static long Factorial(int n)
    {
        long result = 1;
        for (int i = 2; i <= n; i++) result *= i;
        return result;
    }
}
