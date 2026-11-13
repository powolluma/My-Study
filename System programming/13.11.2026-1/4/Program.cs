using System;
using System.Threading;

class Program
{
    static int fuelLevel = 0;

    delegate void TrajectoryDelegate();

    static void Main()
    {
        // Поток мониторинга
        Thread monitor = new Thread(() =>
        {
            while (true)
            {
                Console.WriteLine("Телеметрия в норме");
                Thread.Sleep(1000);
            }
        });

        monitor.IsBackground = true;
        monitor.Start();

        TrajectoryDelegate trajectory = CalculateTrajectory;

        trajectory.BeginInvoke(TrajectoryFinished, trajectory);

        ThreadPool.QueueUserWorkItem(PumpFuel);

        Thread.Sleep(5000);

        int result = Interlocked.CompareExchange(
            ref fuelLevel, 0, 0);

        Console.WriteLine("Итоговый уровень топлива: " + result);

        Console.WriteLine("Подготовка к запуску завершена");
    }

    static void CalculateTrajectory()
    {
        Thread.Sleep(4000);
    }

    static void TrajectoryFinished(IAsyncResult ar)
    {
        TrajectoryDelegate del =
            (TrajectoryDelegate)ar.AsyncState;

        del.EndInvoke(ar);

        Console.WriteLine(
            "Траектория рассчитана");
    }

    static void PumpFuel(object state)
    {
        for (int i = 0; i < 10; i++)
        {
            Interlocked.Add(ref fuelLevel, 10);

            int current = Interlocked.CompareExchange(
                ref fuelLevel, 0, 0);

            Console.WriteLine("Уровень топлива: " + current);

            if (i < 9)
                Thread.Sleep(500);
        }

        Console.WriteLine("Работа завершена");
    }
}