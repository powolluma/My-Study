using System;

class Program
{
    static void Main()
    {
        // Новый домен
        AppDomain newDomain = AppDomain.CreateDomain("NewDomain");
        // Вывод
        Console.WriteLine($"Новый домен: {newDomain.FriendlyName}");

        // Выгрузка
        AppDomain.Unload(newDomain);
        Console.WriteLine($"Домен выгружен");
    }
}