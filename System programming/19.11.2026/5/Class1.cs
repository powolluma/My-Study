using System;

namespace FractionTools
{
    public class Fraction
    {
        public int Numerator { get; private set; }   
        public int Denominator { get; private set; } 

        public Fraction(int numerator, int denominator)
        {
            if (denominator == 0)
                Console.WriteLine("Знаменатель не может быть равен нулю");

            if (denominator < 0)
            {
                numerator = -numerator;
                denominator = -denominator;
            }

            Numerator = numerator;
            Denominator = denominator;
            Simplify(); 
        }

        // Метод сокращения дроби
        public void Simplify()
        {
            int gcd = GetGCD(Math.Abs(Numerator), Math.Abs(Denominator));
            Numerator /= gcd;
            Denominator /= gcd;
        }

        // Сложение 
        public Fraction Add(Fraction other)
        {
            int num = Numerator * other.Denominator + other.Numerator * Denominator;
            int den = Denominator * other.Denominator;
            return new Fraction(num, den);
        }

        // Вычитание
        public Fraction Subtract(Fraction other)
        {
            int num = Numerator * other.Denominator - other.Numerator * Denominator;
            int den = Denominator * other.Denominator;
            return new Fraction(num, den);
        }

        // Умножение
        public Fraction Multiply(Fraction other)
        {
            return new Fraction(Numerator * other.Numerator, Denominator * other.Denominator);
        }

        // Деление
        public Fraction Divide(Fraction other)
        {
            if (other.Numerator == 0)
                throw new DivideByZeroException("Нельзя делить на нулевую дробь.");

            return new Fraction(Numerator * other.Denominator, Denominator * other.Numerator);
        }

        // Перевод 
        public double ToDouble()
        {
            return (double)Numerator / Denominator;
        }

        // Вывод дроби в виде строки
        public override string ToString()
        {
            return Denominator == 1 ? $"{Numerator}" : $"{Numerator}/{Denominator}";
        }

        // Поиск НОД
        private int GetGCD(int a, int b)
        {
            while (b != 0)
            {
                int temp = b;
                b = a % b;
                a = temp;
            }
            return a;
        }
    }
}
