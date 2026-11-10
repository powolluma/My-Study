using System.Data.Common;
using System.Diagnostics;
using System.Globalization;

class Program
{
    static void Main()
    {
        // Дочерний процесс
        ProcessStartInfo startInfo = new ProcessStartInfo
        {
          FileName = "cmd.exe",
          UseShellExecute = false,
          RedirectStandardOutput = true,
          CreateNoWindow = true
        };

        try
        {
            using (Process childProcess = Process.Start(startInfo))
            {
                if(childProcess == null)
                {
                    Console.WriteLine("Не удалось запустить процесс");
                    return;
                }
                
                Console.WriteLine("Процесс запущен");
                Console.WriteLine("Выберите действие:");
                Console.WriteLine("1. Ожидание завершения и вывод кода завершения");
                Console.WriteLine("2. Завершить работу процесса");
                Console.Write("Ответ: ");
                int variant = Convert.ToInt32(Console.ReadLine());

                if(variant == 1)
                {
                    Console.WriteLine("Ожидание завершения процесса");
                    childProcess.WaitForExit(3000);
                    Console.WriteLine("Процесс завешился");
                    Console.WriteLine("Код завершения: " + childProcess.ExitCode);
                }
                else if(variant == 2)
                {
                    childProcess.Kill();
                    childProcess.WaitForExit();
                    Console.WriteLine("Процесс завершен");
                }
                else
                {
                    Console.WriteLine("Неккоректный ввод");
                }

            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);            
        }
    }
}