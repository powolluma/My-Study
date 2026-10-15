using System;

class Program
{
    static double SumSeries(int n)
    {
        if (n <= 0)
            return 0;

        return 1.0 / n + SumSeries(n - 1);
    }

    static void Main()
    {
        int[] tests = { 1, 2, 5, 10 };

        foreach (int n in tests)
        {
            Console.WriteLine($"n = {n}: сумма = {SumSeries(n)}");
        }
    }
}