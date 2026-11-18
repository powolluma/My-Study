using System;
using System.Threading.Tasks;

namespace Task1
{
    class Program
    {
        static void Main(string[] args)
        {
            Task task1 = new Task(() => ShowCurrentDateTime("Способ 1"));
            task1.Start();

            Task task2 = Task.Factory.StartNew(() => ShowCurrentDateTime("Способ 2"));

            Task task3 = Task.Run(() => ShowCurrentDateTime("Способ 3"));

            Task.WaitAll(task1, task2, task3);
            Console.WriteLine("Задачи завершены");
        }
        static void ShowCurrentDateTime(string methodInfo)
        {
            DateTime now = DateTime.Now;
            Console.WriteLine($"{methodInfo}: Текущая дата и время - {now:dd.MM.yyyy HH:mm:ss.fff}");
        }
    }
}
