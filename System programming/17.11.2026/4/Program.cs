using System;
using System.Collections.Generic;
using System.Threading;

using ProducerConsumer app = new ProducerConsumer();

Thread consumer1 = new Thread(app.Consume);
Thread consumer2 = new Thread(app.Consume);
Thread producer = new Thread(app.Produce);

consumer1.Start();
consumer2.Start();
producer.Start();

producer.Join();
consumer1.Join();
consumer2.Join();

Console.WriteLine("Элементы обработаны");

class ProducerConsumer : IDisposable
{
    private const int ItemsCount = 20;
    private const int StopValue = -1;

    private readonly Queue<int> queue = new Queue<int>();
    private readonly object queueLock = new object();
    private readonly Semaphore empty = new Semaphore(5, 5);
    private readonly Semaphore full = new Semaphore(0, 5);

    public void Produce()
    {
        for (int i = 1; i <= ItemsCount; i++)
        {
            empty.WaitOne();

            lock (queueLock)
            {
                queue.Enqueue(i);
                Console.WriteLine($"Производитель добавил: {i}. В очереди: {queue.Count}");
            }

            full.Release();
            Thread.Sleep(100);
        }

        for (int i = 0; i < 2; i++)
        {
            empty.WaitOne();

            lock (queueLock)
            {
                queue.Enqueue(StopValue);
            }

            full.Release();
        }
    }

    public void Consume()
    {
        while (true)
        {
            full.WaitOne();
            int item;

            lock (queueLock)
            {
                item = queue.Dequeue();
            }

            empty.Release();

            if (item == StopValue)
            {
                Console.WriteLine($"Потребитель {Thread.CurrentThread.ManagedThreadId} завершил работу");
                break;
            }

            Console.WriteLine($"Потребитель {Thread.CurrentThread.ManagedThreadId} забрал: {item}");
            Thread.Sleep(250);
        }
    }

    public void Dispose()
    {
        empty.Dispose();
        full.Dispose();
    }
}
