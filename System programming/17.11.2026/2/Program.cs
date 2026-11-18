using System;
using System.Threading;

using MutexCounter counter = new MutexCounter();
Thread[] threads = new Thread[5];

for (int i = 0; i < threads.Length; i++)
{
    threads[i] = new Thread(counter.IncrementOneMillionTimes);
    threads[i].Start();
}

foreach (Thread thread in threads)
{
    thread.Join();
}

Console.WriteLine($"Итоговое значение: {counter.Count}");

class MutexCounter : IDisposable
{
    private int count = 0;
    private readonly Mutex mutex = new Mutex();

    public int Count
    {
        get { return count; }
    }

    public void IncrementOneMillionTimes()
    {
        for (int i = 0; i < 1_000_000; i++)
        {
            mutex.WaitOne();
            try
            {
                count++;
            }
            finally
            {
                mutex.ReleaseMutex();
            }
        }
    }

    public void Dispose()
    {
        mutex.Dispose();
    }
}
