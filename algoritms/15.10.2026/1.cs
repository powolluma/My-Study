using System;

class Program
{
    static long DoubleFactorial(int n)
    {
        if (n < 0)
            throw new ArgumentOutOfRangeException("Факториал не может быть ниже 0");

        if (n <= 1)
            return 1;

        return n * DoubleFactorial(n - 2);
    }

    static void Main()
    {
        int[] tests = { 0, 1, 5, 8, 10 };

        foreach (int n in tests)
        {
            Console.WriteLine($"{n}!! = {DoubleFactorial(n)}");
        }
    }
}