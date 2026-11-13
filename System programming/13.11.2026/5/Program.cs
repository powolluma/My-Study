using System.IO;

using System;
using System.Threading;

class Program
{
    static void Main()
    {
        int[] numbers = new int[10000];
        Random random = new Random();

        for (int i = 0; i < numbers.Length; i++)
        {
            numbers[i] = random.Next(0, 1001);
        }

        int min = 0;
        int max = 0;
        double average = 0;

        Thread minThread = new Thread(() =>
        {
            min = numbers[0];

            foreach (int number in numbers)
            {
                if (number < min)
                    min = number;
            }
        });

        Thread maxThread = new Thread(() =>
        {
            max = numbers[0];

            foreach (int number in numbers)
            {
                if (number > max)
                    max = number;
            }
        });

        Thread averageThread = new Thread(() =>
        {
            long sum = 0;

            foreach (int number in numbers)
            {
                sum += number;
            }

            average = (double)sum / numbers.Length;
        });

        minThread.Start();
        maxThread.Start();
        averageThread.Start();

        minThread.Join();
        maxThread.Join();
        averageThread.Join();

        Console.WriteLine("Минимум: " + min);
        Console.WriteLine("Максимум: " + max);
        Console.WriteLine("Среднее: " + average);



        Thread fileThread = new Thread(() =>
    {
    using (StreamWriter writer = new StreamWriter("result.txt"))
    {
        writer.WriteLine("Набор чисел:");

        foreach (int number in numbers)
        {
            writer.Write(number + " ");
        }

        writer.WriteLine();
        writer.WriteLine("Минимум: " + min);
        writer.WriteLine("Максимум: " + max);
        writer.WriteLine("Среднее: " + average);
            }
        });

        fileThread.Start();
        fileThread.Join();

        Console.WriteLine("Результаты записаны в result.txt");
    }
}
