
using System;
using System.Diagnostics;
using System.IO;

class Program
{
    static void Main()
    {
        // Путь к дочерней программе
        string path = @"C:\Users\powolluma\Desktop\10.11.2026\3\bin\Debug\net10.0\3.dll";

        // Ввод аргументов
        Console.Write("Введите первое число: ");
        int a = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите второе число: ");
        int b = Convert.ToInt32(Console.ReadLine());

        Console.Write("Введите операцию (+, -, *, /): ");
        string operation = Console.ReadLine();

        // Дочерний процесс
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
            FileName = "dotnet",
            Arguments = $"\"{path}\" {a} {b} \"{operation}\"",
            UseShellExecute = false,
            RedirectStandardOutput = true,
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

                childProcess.WaitForExit();

                // Вывод
                Console.WriteLine("\nРезультат:");
                Console.WriteLine(result);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);
        }
    }
}
