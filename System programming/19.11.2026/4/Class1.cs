using System;

namespace Mathh
{
    public class MathHelp
    {
        public static long Factorial(int n)
        {
            if (n < 0)
                Console.WriteLine("Число не может быть отрицательным");

            long result = 1;
            for (int i = 2; i <= n; i++)
            {
                result *= i;
            }
            return result;
        }

        public static bool IsPrime(int n)
        {
            if (n <= 1) return false;
            if (n == 2) return true;
            if (n % 2 == 0) return false;

            for (int i = 3; i <= Math.Sqrt(n); i += 2)
            {
                if (n % i == 0) return false;
            }
            return true;
        }

        public static bool Chet(int n)
        {
            return n % 2 == 0;
        }

        public static bool NeChet(int n)
        {
            return n % 2 != 0;
        }
    }
}
