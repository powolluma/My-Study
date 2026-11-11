
using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main(string[] args)
    {
        // Генератор случайных чисел
        Random rnd = new Random();

        // От 2 до 5 сек
        int delay = rnd.Next(2000, 5001);

        // Ожидание
        Console.WriteLine("Ожидайте сигнала");
        Thread.Sleep(delay);

        // Сигнал красным
        Console.ForegroundColor = ConsoleColor.Red;
        Console.WriteLine("Жмите");
        Console.ResetColor();

        // Секундомер
        Stopwatch sw = Stopwatch.StartNew();

        // Ожидание нажатия
        Console.ReadKey(true);

        // Остановка 
        sw.Stop();

        // Вывод
        Console.WriteLine(
            $"Ваше время реакции: {sw.ElapsedMilliseconds}"
        );
    }
}
