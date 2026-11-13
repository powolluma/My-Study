using System;
using System.Threading;

class Program
{
    static void Main()
    {
        Thread thread = new Thread(() =>
        {
            for (int i = 0; i <= 50; i++)
            {
                Console.WriteLine(i);
            }
        });

        thread.Start();
        thread.Join();
    }
}