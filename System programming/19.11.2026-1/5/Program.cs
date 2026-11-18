using System;
using System.Linq;
using System.Threading.Tasks;

namespace Task5
{
    class Program
    {
        static void Main(string[] args)
        {
            int[] numbers = { 12, 5, 8, 12, 3, 5, 20, 8, 15, 3 };
            int target = 15; 

            Console.WriteLine("Исходный массив: " + string.Join(", ", numbers));

            // Удаление дубликатов
            Task<int[]> removeDuplicatesTask = Task.Run(() =>
            {
                int[] unique = numbers.Distinct().ToArray();
                Console.WriteLine("Без дубликатов: " + string.Join(", ", unique));
                return unique;
            });

            // Сортировка массива
            Task<int[]> sortTask = removeDuplicatesTask.ContinueWith(prevTask =>
            {
                int[] sorted = prevTask.Result;
                Array.Sort(sorted);
                Console.WriteLine("Отсортированный: " + string.Join(", ", sorted));
                return sorted;
            });

            // Бинарный поиск
            Task<int> searchTask = sortTask.ContinueWith(prevTask =>
            {
                int[] sortedArray = prevTask.Result;
                int index = Array.BinarySearch(sortedArray, target);
                return index;
            });

            searchTask.Wait();

            // Вывод
            if (searchTask.Result >= 0)
            {
                Console.WriteLine($"Число {target} найдено на позиции (индекс): {searchTask.Result}");
            }
            else
            {
                Console.WriteLine($"Число {target} не найдено в массиве.");
            }
        }
    }
}
