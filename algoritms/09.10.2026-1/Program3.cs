using System;

class Program
{
    static double CalculateArea(double radius) => Math.PI * radius * radius;

    static double CalculateArea(double width, double height) => width * height;

    static double CalculateArea(double a, double b, double c)
    {
        double p = (a + b + c) / 2;
        double value = p * (p - a) * (p - b) * (p - c);
        if (a <= 0 || b <= 0 || c <= 0 || value <= 0)
            throw new ArgumentException("Такие стороны не образуют треугольник.");
        return Math.Sqrt(value);
    }

    static void Main()
    {
        Console.WriteLine($"Площадь круга: {CalculateArea(3):F2}");
        Console.WriteLine($"Площадь прямоугольника: {CalculateArea(4, 5):F2}");
        Console.WriteLine($"Площадь треугольника: {CalculateArea(3, 4, 5):F2}");
    }
}
