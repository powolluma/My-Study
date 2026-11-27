using System;
using System.Collections.Generic;
using LibraryApp.Exceptions;
using LibraryApp.Models;

namespace LibraryApp
{
    class Program
    {
        static int ReadInt(string text)
        {
            while (true)
            {
                Console.Write(text);
                string line = Console.ReadLine();
                int value;
                if (int.TryParse(line, out value))
                {
                    return value;
                }
                Console.WriteLine("Нужно ввести целое число");
            }
        }

        static string ReadString(string text)
        {
            Console.Write(text);
            return Console.ReadLine();
        }

        static void AddPublication(Library lib)
        {
            Console.WriteLine("Тип издания: 1 - книга, 2 - журнал, 3 - газета");
            string kind = ReadString("Выбор: ");
            if (kind != "1" && kind != "2" && kind != "3")
            {
                Console.WriteLine("Неверный тип");
                return;
            }
            int inv = ReadInt("Инвентарный номер: ");
            string title = ReadString("Название: ");
            int year = ReadInt("Год выпуска: ");
            Publication pub;
            if (kind == "1")
            {
                string author = ReadString("Автор: ");
                int pages = ReadInt("Количество страниц: ");
                pub = new Book(inv, title, year, author, pages);
            }
            else if (kind == "2")
            {
                int number = ReadInt("Номер выпуска: ");
                string publisher = ReadString("Издательство: ");
                pub = new Magazine(inv, title, year, number, publisher);
            }
            else
            {
                string date = ReadString("Дата выпуска: ");
                string period = ReadString("Периодичность: ");
                pub = new Newspaper(inv, title, year, date, period);
            }
            lib.Add(pub);
            Console.WriteLine("Издание добавлено");
        }

        static void ShowCatalog(Library lib)
        {
            List<Publication> items = lib.GetAll();
            if (items.Count == 0)
            {
                Console.WriteLine("Каталог пуст");
                return;
            }
            foreach (Publication item in items)
            {
                Console.WriteLine(item.GetInfo());
            }
        }

        static void SearchPublication(Library lib)
        {
            string title = ReadString("Название для поиска: ");
            foreach (Publication item in lib.Search(title))
            {
                Console.WriteLine(item.GetInfo());
            }
        }

        static void ShowStats(Library lib)
        {
            int total, available, issued;
            lib.GetStats(out total, out available, out issued);
            Console.WriteLine($"Всего изданий: {total}");
            Console.WriteLine($"Доступно: {available}");
            Console.WriteLine($"Выдано: {issued}");
        }

        static void Main(string[] args)
        {
            Console.OutputEncoding = System.Text.Encoding.UTF8;
            Library lib = new Library();
            lib.Add(new Book(1, "Война и мир", 1869, "Л. Толстой", 1225));
            lib.Add(new Magazine(2, "Наука и жизнь", 2024, 5, "АНО Наука и жизнь"));
            lib.Add(new Newspaper(3, "Известия", 2025, "12.03.2025", "ежедневно"));

            bool running = true;
            while (running)
            {
                Console.WriteLine();
                Console.WriteLine("=== Библиотека ===");
                Console.WriteLine("1. Добавить издание");
                Console.WriteLine("2. Показать каталог");
                Console.WriteLine("3. Найти по названию");
                Console.WriteLine("4. Выдать издание");
                Console.WriteLine("5. Вернуть издание");
                Console.WriteLine("6. Удалить издание");
                Console.WriteLine("7. Статистика");
                Console.WriteLine("0. Выход");
                string choice = ReadString("Выберите действие: ");

                try
                {
                    switch (choice)
                    {
                        case "1":
                            AddPublication(lib);
                            break;
                        case "2":
                            ShowCatalog(lib);
                            break;
                        case "3":
                            SearchPublication(lib);
                            break;
                        case "4":
                            lib.Issue(ReadInt("Инвентарный номер: "));
                            Console.WriteLine("Издание выдано");
                            break;
                        case "5":
                            lib.GiveBack(ReadInt("Инвентарный номер: "));
                            Console.WriteLine("Издание возвращено");
                            break;
                        case "6":
                            lib.Remove(ReadInt("Инвентарный номер: "));
                            Console.WriteLine("Издание удалено");
                            break;
                        case "7":
                            ShowStats(lib);
                            break;
                        case "0":
                            Console.WriteLine("До свидания!");
                            running = false;
                            break;
                        default:
                            Console.WriteLine("Неверный пункт меню");
                            break;
                    }
                }
                catch (LibraryException e)
                {
                    Console.WriteLine("Ошибка: " + e.Message);
                }
            }
        }
    }
}
