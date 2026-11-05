using System;
using System.Diagnostics;
using System.Threading;

class Program
{
    static void Main()
    {
        // Настройка параметров дочернего запуска
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
          FileName = "notepad.exe",
          Arguments = "timeout /t 60",
        };

        try
        {
            using(Process childProcess = Process.Start(startInfo))
            {
                Process.Start("notepad.exe");
                Console.WriteLine($"Процесс запущен, ожидаем 3 секунды");


                if (childProcess.WaitForExit(3000))
                {
                    Console.WriteLine($"Процесс завершился");
                }

                else
                {
                    Console.WriteLine($"Процесс не завершился за 3 секунды. Завершение");

                    childProcess.Kill();
                    childProcess.WaitForExit();
                    Console.WriteLine($"Процесс зевершился");
                }
            };
        }

        catch(Exception ex)
        {
            Console.WriteLine($"Ошибка при работе с процессом: {ex.Message}");
        }
    }
}