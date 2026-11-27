from library import Library
from exceptions import LibraryError
from models.book import Book
from models.magazine import Magazine
from models.newspaper import Newspaper


def read_int(text):
    while True:
        try:
            return int(input(text))
        except ValueError:
            print("Нужно ввести целое число")


def add_publication(lib):
    print("Тип издания: 1 - книга, 2 - журнал, 3 - газета")
    kind = input("Выбор: ")
    if kind not in ("1", "2", "3"):
        print("Неверный тип")
        return
    inv = read_int("Инвентарный номер: ")
    title = input("Название: ")
    year = read_int("Год выпуска: ")
    if kind == "1":
        author = input("Автор: ")
        pages = read_int("Количество страниц: ")
        pub = Book(inv, title, year, author, pages)
    elif kind == "2":
        number = read_int("Номер выпуска: ")
        publisher = input("Издательство: ")
        pub = Magazine(inv, title, year, number, publisher)
    else:
        date = input("Дата выпуска: ")
        period = input("Периодичность: ")
        pub = Newspaper(inv, title, year, date, period)
    lib.add(pub)
    print("Издание добавлено")


def show_catalog(lib):
    items = lib.get_all()
    if not items:
        print("Каталог пуст")
        return
    for item in items:
        print(item.get_info())


def search_publication(lib):
    title = input("Название для поиска: ")
    for item in lib.search(title):
        print(item.get_info())


def issue_publication(lib):
    lib.issue(read_int("Инвентарный номер: "))
    print("Издание выдано")


def return_publication(lib):
    lib.give_back(read_int("Инвентарный номер: "))
    print("Издание возвращено")


def remove_publication(lib):
    lib.remove(read_int("Инвентарный номер: "))
    print("Издание удалено")


def show_stats(lib):
    total, available, issued = lib.stats()
    print(f"Всего изданий: {total}")
    print(f"Доступно: {available}")
    print(f"Выдано: {issued}")


def main():
    lib = Library()
    lib.add(Book(1, "Война и мир", 1869, "Л. Толстой", 1225))
    lib.add(Magazine(2, "Наука и жизнь", 2024, 5, "АНО Наука и жизнь"))
    lib.add(Newspaper(3, "Известия", 2025, "12.03.2025", "ежедневно"))

    actions = {
        "1": add_publication,
        "2": show_catalog,
        "3": search_publication,
        "4": issue_publication,
        "5": return_publication,
        "6": remove_publication,
        "7": show_stats,
    }

    while True:
        print()
        print("=== Библиотека ===")
        print("1. Добавить издание")
        print("2. Показать каталог")
        print("3. Найти по названию")
        print("4. Выдать издание")
        print("5. Вернуть издание")
        print("6. Удалить издание")
        print("7. Статистика")
        print("0. Выход")
        choice = input("Выберите действие: ")
        if choice == "0":
            print("До свидания!")
            break
        action = actions.get(choice)
        if action is None:
            print("Неверный пункт меню")
            continue
        try:
            action(lib)
        except LibraryError as e:
            print("Ошибка:", e)


if __name__ == "__main__":
    main()
