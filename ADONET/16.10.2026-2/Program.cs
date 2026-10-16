using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Xml.Linq;

public class Student
{
    public string Name { get; set; }
    public string Group { get; set; }
    public int Age { get; set; }
    public double GPA { get; set; }
    public int PassedCredits { get; set; }

    public override string ToString()
    {
        return $"{Name,-10} | {Group,-6} | {Age,2} | {GPA,4:F1} | {PassedCredits,2}";
    }
}

class Program
{
    private static List<Student> GetStudents()
    {
        return new List<Student>
        {
            new Student { Name = "Алексей", Group = "ИТ-41", Age = 20, GPA = 4.2, PassedCredits = 40 },
            new Student { Name = "Борис", Group = "ИТ-41", Age = 19, GPA = 3.1, PassedCredits = 35 },
            new Student { Name = "Виктория", Group = "ПИ-42", Age = 21, GPA = 4.8, PassedCredits = 45 },
            new Student { Name = "Григорий", Group = "ИБ-43", Age = 20, GPA = 2.9, PassedCredits = 20 },
            new Student { Name = "Дарья", Group = "ИТ-41", Age = 22, GPA = 4.5, PassedCredits = 42 },
            new Student { Name = "Елена", Group = "ПИ-42", Age = 20, GPA = 3.8, PassedCredits = 38 },
            new Student { Name = "Жанна", Group = "ИБ-43", Age = 23, GPA = 4.9, PassedCredits = 48 },
            new Student { Name = "Игорь", Group = "ИТ-41", Age = 19, GPA = 2.5, PassedCredits = 15 }
        };
    }

    static void Main(string[] args)
    {
        Task1_QuerySyntax();
        Console.WriteLine("\nНажмите Enter для перехода...");
        Console.ReadLine();

        Task2_MethodSyntax();
        Console.WriteLine("\nНажмите Enter для перехода...");
        Console.ReadLine();

        Task3_Aggregation();
        Console.WriteLine("\nНажмите Enter для перехода...");
        Console.ReadLine();

        Task4_Xml();
        Console.WriteLine("\nНажмите Enter для перехода...");
        Console.ReadLine();
    }

    // Задание 1
    static void Task1_QuerySyntax()
    {
        Console.WriteLine("Задание 1");
        Console.WriteLine("Имя | Группа | Возраст | Балл | Зачеты");

        var students = GetStudents();

        var query = from s in students
                    where s.Group == "ИТ-41" && s.GPA > 3.5
                    orderby s.Age
                    select s;

        foreach (var student in query)
        {
            Console.WriteLine(student.ToString());
        }
    }

    // Задание 2
    static void Task2_MethodSyntax()
    {
        Console.WriteLine("Задание 2");

        var students = GetStudents();

        var query = students
            .OrderByDescending(s => s.GPA)
            .Select(s => new
            {
                FullName = s.Name,
                Scholarship = s.GPA >= 4.7 ? "Ректорская" :
                              s.GPA >= 4.5 ? "Повышенная" :
                              s.GPA >= 4.0 ? "Стандартная" : "Нет"
            });

        foreach (var item in query)
        {
            if (item.Scholarship != "Нет")
            {
                Console.WriteLine($"{item.FullName}: {item.Scholarship}");
            }
        }
    }

    // Задание 3
    static void Task3_Aggregation()
    {
        Console.WriteLine("Задание 3");

        var students = GetStudents();

        // Группировка
        var groupsQuery = students
            .GroupBy(s => s.Group)
            .Select(g => new
            {
                GroupName = g.Key,
                AvgGPA = g.Average(s => s.GPA)
            })
            .OrderBy(g => g.GroupName);

        foreach (var g in groupsQuery)
        {
            Console.WriteLine($"Группа {g.GroupName}: Средний балл {g.AvgGPA}");
        }

        // Часть Б: Поиск
        var oldestWith45Credits = students
            .Where(s => s.PassedCredits == 45)
            .OrderByDescending(s => s.Age)
            .FirstOrDefault();

        if (oldestWith45Credits != null)
        {
            Console.WriteLine($"Студент с 45 зачетами: {oldestWith45Credits.Name} ({oldestWith45Credits.Age} год)");
        }
        else
        {
            Console.WriteLine("Студент не найден");
        }
    }

    // Задание 4
    static void Task4_Xml()
    {
        Console.WriteLine("Задание 4");

        string filePath = "uni.xml";

        // Создание 
        XDocument xmlDoc = new XDocument(
            new XElement("university",
                new XElement("course", new XAttribute("name", "Программирование"),
                    new XElement("student", new XAttribute("name", "Кирилл"), new XAttribute("gpa", "3.9")),
                    new XElement("student", new XAttribute("name", "Лариса"), new XAttribute("gpa", "4.7")),
                    new XElement("student", new XAttribute("name", "Михаил"), new XAttribute("gpa", "3.2")),
                    new XElement("student", new XAttribute("name", "Наталья"), new XAttribute("gpa", "4.1"))
                )
            )
        );

        xmlDoc.Save(filePath);
        Console.WriteLine("Документ создан\n");

        // Чтение 
        XDocument loadedDoc = XDocument.Load(filePath);

        var excellentStudents = from s in loadedDoc.Descendants("student")
                                where double.Parse((string)s.Attribute("gpa"), CultureInfo.InvariantCulture) > 4.0
                                select (string)s.Attribute("name");

        Console.WriteLine("Отличники из XML (GPA > 4.0):");
        foreach (var name in excellentStudents)
        {
            Console.WriteLine(name);
        }
    }
}
