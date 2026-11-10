
using System;
using System.Diagnostics;

class Program
{
    static void Main()
    {
        
        Console.Write("Введите путь к файлу: ");
        string path = Console.ReadLine();

        
        Console.Write("Введите слово для поиска: ");
        string word = Console.ReadLine();

        // Путь 
        string childPath = @"C:\Users\powolluma\Desktop\10.11.2026\ChildProcess\bin\Debug\net10.0\4.dll";

        // Дочерний процесс
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"\"{childPath}\" \"{path}\" \"{word}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
            RedirectStandardError = true,
            CreateNoWindow = true
        };

        try
        {
            using (Process childProcess = Process.Start(startInfo))
            {
                if (childProcess == null)
                {
                    Console.WriteLine("Не удалось запустить процесс");
                    return;
                }

                string result = childProcess.StandardOutput.ReadToEnd();
                string error = childProcess.StandardError.ReadToEnd();

                childProcess.WaitForExit();

                Console.WriteLine("\nРезультат:");
                Console.WriteLine(result);

                if (!string.IsNullOrEmpty(error))
                {
                    Console.WriteLine("Ошибка: " + error);
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
