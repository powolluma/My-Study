using System;

class Program
{
    static int Sum(int a, int b) => a + b;
    static int Difference(int a, int b) => a - b;
    static int Product(int a, int b) => a * b;

    static double Quotient(int a, int b)
    {
        if (b == 0)
            throw new DivideByZeroException("Делить на ноль нельзя.");
        return (double)a / b;
    }

    static int Max(int a, int b) => Math.Max(a, b);
    static int Min(int a, int b) => Math.Min(a, b);

    static void Main()
    {
        Console.Write("Введите два целых числа: ");
        string[] parts = Console.ReadLine().Split(' ', StringSplitOptions.RemoveEmptyEntries);
        int a = int.Parse(parts[0]);
        int b = int.Parse(parts[1]);

        Console.WriteLine($"Сумма: {Sum(a, b)}");
        Console.WriteLine($"Разность: {Difference(a, b)}");
        Console.WriteLine($"Произведение: {Product(a, b)}");
        Console.WriteLine($"Частное: {(b == 0 ? "деление на ноль" : Quotient(a, b).ToString())}");
        Console.WriteLine($"Максимум: {Max(a, b)}");
        Console.WriteLine($"Минимум: {Min(a, b)}");
    }
}
