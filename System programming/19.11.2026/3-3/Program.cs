using System;
using Number;

int x = 7, y = -7, z = 77;

int maxVal = NumberHelp.Max(x, y, z);
int minVal = NumberHelp.Min(x, y, z);
int sumVal = NumberHelp.Sum(x, y, z);

Console.WriteLine($"Числа: {x}, {y}, {z}");
Console.WriteLine($"Максимум: {maxVal}");
Console.WriteLine($"Минимум: {minVal}");
Console.WriteLine($"Сумма: {sumVal}");
