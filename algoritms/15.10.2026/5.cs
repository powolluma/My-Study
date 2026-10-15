using System;

class Program
{
    static int CountDigits(int n)
    {
        if (n > -10 && n < 10)
            return 1;

        return 1 + CountDigits(n / 10);
    }

    static void Main()
    {
        int[] tests = { 7, 25, 1234, 10000, -567 };

        foreach (int n in tests)
        {
            Console.WriteLine($"Число {n}: {CountDigits(n)}");
        }
    }
}