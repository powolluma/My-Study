using System;
using System.Threading;

class Program
{
    delegate double HeavyMathDelegate(int n);

    static ManualResetEvent done = new ManualResetEvent(false);

    static void Main()
    {
        Console.Write("Введите число: ");
        int n = Convert.ToInt32(Console.ReadLine());

        HeavyMathDelegate calculate = CalculateHeavyMath;

        calculate.BeginInvoke(n, CalculationFinished, calculate);

        Console.WriteLine("Расчет запущен. Главный поток освобожден");

        done.WaitOne();
        done.Dispose();
    }

    static double CalculateHeavyMath(int n)
    {
        Thread.Sleep(3000);

        return Math.Sqrt(n);
    }

    static void CalculationFinished(IAsyncResult ar)
    {
        HeavyMathDelegate del = (HeavyMathDelegate)ar.AsyncState;

        try
        {
            double result = del.EndInvoke(ar);

            Console.WriteLine("Результат: " + result);
        }
        finally
        {
            done.Set();
        }
    }
}