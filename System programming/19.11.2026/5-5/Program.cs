using System;
using FractionTools; // Подключаем пространство имен нашей DLL

// Создаём объекты класса Дробь (вторая дробь 2/4 автоматически сократится до 1/2)
Fraction f1 = new Fraction(1, 3);
Fraction f2 = new Fraction(2, 4);

Console.WriteLine($"Первая дробь: {f1}");
Console.WriteLine($"Вторая дробь: {f2}");
Console.WriteLine();

// Вызываем методы работы с дробями
Console.WriteLine($"Сложение: {f1} + {f2} = {f1.Add(f2)}");
Console.WriteLine($"Вычитание: {f1} - {f2} = {f1.Subtract(f2)}");
Console.WriteLine($"Умножение: {f1} * {f2} = {f1.Multiply(f2)}");
Console.WriteLine($"Деление: {f1} / {f2} = {f1.Divide(f2)}");
Console.WriteLine($"Десятичное значение {f1}: {f1.ToDouble():F2}");
