using System;

class Program
{
    static bool IsPrime(int n)
    {
        if (n < 2)
            return false;

        return CheckDivisor(n, 2);
    }

    static bool Check(int n, int divisor)
    {
        if (divisor > n / divisor)
            return true;

        if (n % divisor == 0)
            return false;

        return CheckDivisor(n, divisor + 1);
    }

    static void Main()
    {
        int[] tests = { 1, 2, 3, 4, 17, 25, 29 };

        foreach (int n in tests)
        {
            Console.WriteLine($"{n}: {(IsPrime(n) ? "Простое" : "Составное")}");
        }
    }
}