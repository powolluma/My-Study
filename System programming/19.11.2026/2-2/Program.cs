using System;
using Date;

Console.Write("Введите год для проверки: ");
if (int.TryParse(Console.ReadLine(), out int year))
{
    bool isLeap = Year.IsYear(year);

    if (isLeap)
    {
        Console.WriteLine($"високосный");
    }
    else
    {
        Console.WriteLine($"не високосный");
    }
}
else
{
    Console.WriteLine("Ошибка");
}
