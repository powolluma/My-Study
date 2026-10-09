using System;

class Program
{
    static void Swap(ref int a, ref int b)
    {
        int temp = a;
        a = b;
        b = temp;
    }

    static void Divide(int a, int b, out int quotient, out int remainder)
    {
        if (b == 0)
            throw new DivideByZeroException("Делить на ноль нельзя.");
        quotient = a / b;
        remainder = a % b;
    }

    static void Main()
    {
        int a = 12;
        int b = 5;
        Swap(ref a, ref b);
        Console.WriteLine($"После обмена: a = {a}, b = {b}");

        Divide(a, b, out int quotient, out int remainder);
        Console.WriteLine($"Частное: {quotient}, остаток: {remainder}");
    }
}
