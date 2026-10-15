using System;

class Program
{
    static int FindMin(int[] array, int index)
    {
        if (index == array.Length - 1)
            return array[index];

        int min = FindMin(array, index + 1);

        return array[index] < min ? array[index] : min;
    }

    static void Main()
    {
        int[][] tests =
        {
            new int[] { 5, 2, 8, 1, 9 },
            new int[] { -4, 7, 0, -10, 3 },
            new int[] { 12, 6, 15, 4 }
        };

        foreach (int[] array in tests)
        {
            Console.WriteLine($"Массив: {string.Join(", ", array)}");
            Console.WriteLine($"Мин элемент: {FindMin(array, 0)}");
        }
    }
}