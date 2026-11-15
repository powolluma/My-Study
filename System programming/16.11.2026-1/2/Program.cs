using System;
using System.IO;
using System.Linq;
using System.Threading;

class Program
{
    static void Main()
    {
        Console.Write("Введите путь к файлу: ");
        string path = Console.ReadLine()!;

        if (!File.Exists(path))
        {
            Console.WriteLine("Файл не найден");
            return;
        }

        object locker = new object();

        ManualResetEventSlim finish = new ManualResetEventSlim(false);

        int sentences = 0;
        bool success = false;

        // Поток чтения файла
        Thread thread1 = new Thread(() =>
        {
            try
            {
                lock (locker)
                {
                    string text = File.ReadAllText(path);

                    sentences = text.Count(c =>
                        c == '.' || c == '!' || c == '?');
                }

                Console.WriteLine("Количество предложений: " + sentences);

                success = true;
            }
            catch (Exception ex)
            {
                Console.WriteLine("Ошибка: " + ex.Message);
            }
            finally
            {
                finish.Set();
            }
        });

        // Поток изменения файла
        Thread thread2 = new Thread(() =>
        {
            finish.Wait();

            if (!success)
            {
                Console.WriteLine("Изменение файла не выполнено");
                return;
            }

            lock (locker)
            {
                string text = File.ReadAllText(path);

                text = text.Replace('!', '#');

                File.WriteAllText(path, text);
            }

            Console.WriteLine("Все символы заменены на ");
        });

        thread1.Start();
        thread2.Start();

        thread1.Join();
        thread2.Join();

        Console.WriteLine("Работа завершена");
        finish.Dispose();
    }
}