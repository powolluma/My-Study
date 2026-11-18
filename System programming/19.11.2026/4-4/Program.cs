using System;
using Mathh;

int number = 7;

long fact = MathHelp.Factorial(number);
bool prime = MathHelp.IsPrime(number);
bool even = MathHelp.Chet(number);
bool odd = MathHelp.NeChet(number);

Console.WriteLine($"Число: {number}");
Console.WriteLine($"Факториал: {fact}");
Console.WriteLine($"Простое: {(prime ? "Да" : "Нет")}");
Console.WriteLine($"Четное: {(even ? "Да" : "Нет")}");
Console.WriteLine($"Нечетное: {(odd ? "Да" : "Нет")}");
