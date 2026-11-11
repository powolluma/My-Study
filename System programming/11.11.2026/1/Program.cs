
using System;
using System.Threading;

class Program
{
    // Метод выполняющий каждый поток
    static void PrintSteps(object? parameter)
    {
        // Имя потока
        string name = parameter?.ToString() ?? "Без имени";

        // Вывод
        for (int i = 1; i <= 5; i++)
        {
            Console.WriteLine($"{name} Шаг {i}");

            // Пауза
            Thread.Sleep(500);
        }
    }

    static void Main(string[] args)
    {
        // 1 поток
        Thread threadA = new Thread(
            new ParameterizedThreadStart(PrintSteps)
        );

        // 2 поток
        Thread threadB = new Thread(
            new ParameterizedThreadStart(PrintSteps)
        );

        // Запуск потоков и передача имен
        threadA.Start("Поток А");
        threadB.Start("Поток Б");

        // Ожидание завершения потоков
        threadA.Join();
        threadB.Join();

        // Конец
        Console.WriteLine("Потоки завершили работу");
    }
}
