using System;

class Program
{
    static int Factorial(int n)
    {
        if (n < 0) throw new ArgumentException("Число должно быть неотрицательным.");
        if (n <= 1) return 1;
        return n * Factorial(n - 1);
    }

    static int Fibonacci(int n)
    {
        if (n < 0) throw new ArgumentException("Номер должен быть неотрицательным.");
        if (n <= 1) return n;
        return Fibonacci(n - 1) + Fibonacci(n - 2);
    }

    static int SumDigits(int n)
    {
        n = Math.Abs(n);
        if (n < 10) return n;
        return n % 10 + SumDigits(n / 10);
    }

    static void Main()
    {
        Console.WriteLine($"Факториал 5: {Factorial(5)}");
        Console.WriteLine($"Число Фибоначчи № 7: {Fibonacci(7)}");
        Console.WriteLine($"Сумма цифр числа 12345: {SumDigits(12345)}");
    }
}
