using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Введите начало диапазона: ");
        int start = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите конец диапазона: ");
        int end = Convert.ToInt32(Console.ReadLine());

        if (start > end)
        {
            Console.WriteLine("Начало не может быть больше конца");
            return;
        }

        Thread thread = new Thread(() =>
        {
            for (int i = start; i <= end; i++)
            {
                Console.WriteLine(i);
            }
        });

        thread.Start();
        thread.Join();
    }
}