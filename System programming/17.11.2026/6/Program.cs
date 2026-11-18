using System;
using System.Threading;

using AutoResetEvent event1 = new AutoResetEvent(true);
using AutoResetEvent event2 = new AutoResetEvent(false);

Thread oddThread = new Thread(() =>
{
    for (int number = 1; number <= 9; number += 2)
    {
        event1.WaitOne();
        Console.Write($"{number} ");
        event2.Set();
    }
});

Thread evenThread = new Thread(() =>
{
    for (int number = 2; number <= 10; number += 2)
    {
        event2.WaitOne();
        Console.Write($"{number} ");
        event1.Set();
    }
});

evenThread.Start();
oddThread.Start();

evenThread.Join();
oddThread.Join();

Console.WriteLine();
