using System;
using System.Threading;

const string mutexName = "MY_MUTEX";

try
{
    using Mutex existingMutex = Mutex.OpenExisting(mutexName);
    Console.WriteLine("Приложение уже запущено");
    return;
}
catch (WaitHandleCannotBeOpenedException)
{
    
}

using (Mutex mutex = new Mutex(true, mutexName))
{
    try
    {
        Console.WriteLine("Приложение работает");
        Console.ReadKey();
    }
    finally
    {
        mutex.ReleaseMutex();
    }
}
