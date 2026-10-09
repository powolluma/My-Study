using System;

class Program
{
    static void PrintWelcome()
    {
        Console.WriteLine("Приветствие!");
    }

    static void PrintLine()
    {
        Console.WriteLine(new string('-', 40));
    }

    static void PrintInfo(string name, int age)
    {
        Console.WriteLine($"Имя: {name}, возраст: {age}");
    }

    static void PrintDate()
    {
        Console.WriteLine(DateTime.Now.ToString("dd.MM.yyyy"));
    }

    static void Main()
    {
        PrintWelcome();
        PrintLine();
        PrintInfo("Алексей", 18);
        PrintDate();
    }
}
