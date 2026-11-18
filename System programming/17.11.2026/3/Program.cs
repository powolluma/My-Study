using System;
using System.Threading;

using Semaphore semaphore = new Semaphore(3, 3);
using CountdownEvent finished = new CountdownEvent(6);
int activeWorkers = 0;

for (int i = 1; i <= 6; i++)
{
    int taskNumber = i;

    ThreadPool.QueueUserWorkItem(_ =>
    {
        try
        {
            while (!semaphore.WaitOne(500))
            {
                Console.WriteLine($"Задача {taskNumber}: время ожидания истекло");
            }

            try
            {
                int active = Interlocked.Increment(ref activeWorkers);
                Console.WriteLine(
                    $"Задача {taskNumber}: поток {Thread.CurrentThread.ManagedThreadId} начал работу. Сейчас работают: {active}");

                Thread.Sleep(2000);

                Console.WriteLine($"Задача {taskNumber}: работа завершена");
            }
            finally
            {
                Interlocked.Decrement(ref activeWorkers);
                semaphore.Release();
            }
        }
        finally
        {
            finished.Signal();
        }
    });
}

finished.Wait();
