using System;
using System.Threading;

using ManualResetEvent startEvent = new ManualResetEvent(false);
Thread[] threads = new Thread[10];

for (int i = 0; i < threads.Length; i++)
{
    int number = i + 1;

    threads[i] = new Thread(() =>
    {
        Console.WriteLine($"Поток {number} ожидает сигнал");
        startEvent.WaitOne();
        Console.WriteLine($"Поток {number} стартовал");
    });

    threads[i].Start();
}

Thread.Sleep(2000);
Console.WriteLine("Основной поток подает сигнал Set()");
startEvent.Set();

foreach (Thread thread in threads)
{
    thread.Join();
}

startEvent.Reset();
Console.WriteLine("Событие сброшено. Запускаю еще один поток");

Thread newThread = new Thread(() =>
{
    Console.WriteLine("Новый поток ожидает сигнал после Reset");
    startEvent.WaitOne();
    Console.WriteLine("Новый поток продолжил работу после Set");
});

newThread.Start();
Thread.Sleep(1000);
Console.WriteLine("Основной поток снова вызывает Set()");
startEvent.Set();
newThread.Join();
