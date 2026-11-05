using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        // 1. Получение информации о текущем процессе
        Process currentProcess = Process.GetCurrentProcess();
        Console.WriteLine($"Имя текущего процесса: {currentProcess.ProcessName}");
        Console.WriteLine($"ID текущего процесса: {currentProcess.Id}");
        Console.WriteLine($"Время запуска: {currentProcess.StartTime}");
        Console.WriteLine($"Использование виртуальной памяти: {currentProcess.VirtualMemorySize64 / 1024 / 1024} MB");
        Console.WriteLine(new string('-', 40));

        // 2. Получение списка всех запущенных процессов в системе
        Console.WriteLine("Топ 5 процессов по объему:");
        Process[] allProcesses = Process.GetProcesses();
        
        var top = allProcesses
                            .OrderByDescending(p => p.WorkingSet64) // Сортировка
                            .Take(5); // 5 шт

        // Вывод
        foreach(Process process in top)
        {
            Console.WriteLine(
                $"Имя: {process.ProcessName}, " + $"id: {process.Id}, " + $"Память: {process.WorkingSet64 / 1024 / 1024} мб"
            );
        }

        foreach(Process process in allProcesses)
        {
            process.Dispose();
        }
    }
}
