using System.Data.Common;
using System.Diagnostics;

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
                }
                
                Console.WriteLine("Процесс запущен");

                // Ожидание завершения
                if (childProcess.WaitForExit(3000))
                {
                    Console.WriteLine("Процесс завершился");
                }
                else
                {
                    Console.WriteLine("Принудительное завершение");

                    childProcess.Kill();
                    childProcess.WaitForExit();
                    Console.WriteLine("Процесс завершен");
                    Console.WriteLine("Код завершения: " + childProcess.ExitCode);
                }
            }
        }
        catch(Exception ex)
        {
            Console.WriteLine("Ошибка: " + ex.Message);            
        }
    }
}