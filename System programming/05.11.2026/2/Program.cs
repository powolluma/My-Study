using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "ping.exe",
            Arguments = "8.8.8.8 -n 4",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            CreateNoWindow = true
        };

        try
        {
            // Запуск дочернего процесса
            using (Process childProcess = Process.Start(startInfo))
            {
                if (childProcess == null)
                {
                    Console.WriteLine("Не удалось запустить процесс");
                    return;
                }

                Console.WriteLine("Дочерний процесс запущен");


                var outputTask = childProcess.StandardOutput.ReadToEnd();

                // Ожидание завершения 
                if (childProcess.WaitForExit(5000))
                {
                    Console.WriteLine("Дочерний процесс завершился");
                }
                else
                {
                    Console.WriteLine(
                        "Принудительное завершение"
                    );

                    childProcess.Kill();
                    childProcess.WaitForExit();

                    Console.WriteLine("Дочерний процесс был завершен");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Ошибка при работе с процессом: {ex.Message}");
        }
    }
}