def read_int(prompt):
    while True:
        try:
            return int(input(prompt))
        except ValueError:
            print("Ошибка: введите целое число")

def read_positive_int(prompt):
    while True:
        v = read_int(prompt)
        if v > 0:
            return v
        print("Ошибка: число должно быть больше нуля")

def read_text(prompt):
    while True:
        s = input(prompt).strip()
        if s:
            return s
        print("Ошибка: строка не должна быть пустой")

def print_books(books):
    if not books:
        print("Ничего не найдено")
        return
    for b in books:
        print(b["author"], "|", b["title"], "|", b["year"], "|", b["pages"])

def add_book(books):
    author = read_text("Автор: ")
    title = read_text("Название: ")
    year = read_positive_int("Год: ")
    pages = read_positive_int("Страниц: ")
    books.append({"author": author, "title": title, "year": year, "pages": pages})

def remove_book(books):
    title = read_text("Название для удаления: ")
    for b in books:
        if b["title"].lower() == title.lower():
            books.remove(b)
            print("Удалено")
            return
    print("Книга не найдена")

def find_by_author(books):
    author = read_text("Автор: ")
    found = []
    for b in books:
        if b["author"].lower() == author.lower():
            found.append(b)
    print_books(found)

def books_after_year(books):
    year = read_int("Год: ")
    found = []
    for b in books:
        if b["year"] > year:
            found.append(b)
    print_books(found)

def sort_by_year(books):
    books.sort(key=lambda b: b["year"])
    print_books(books)

def statistics(books):
    if not books:
        print("Библиотека пуста")
        return
    total = 0
    for b in books:
        total += b["pages"]
    print("Всего книг:", len(books))
    print("Средний объём:", total / len(books))

def print_menu():
    print("\n1. Добавить книгу")
    print("2. Удалить книгу")
    print("3. Найти по автору")
    print("4. Книги после года")
    print("5. Сортировать по году")
    print("6. Статистика")
    print("7. Показать все")
    print("0. Выход")

def main():
    books = []
    while True:
        print_menu()
        choice = input("Выбор: ").strip()
        if choice == "1":
            add_book(books)
        elif choice == "2":
            remove_book(books)
        elif choice == "3":
            find_by_author(books)
        elif choice == "4":
            books_after_year(books)
        elif choice == "5":
            sort_by_year(books)
        elif choice == "6":
            statistics(books)
        elif choice == "7":
            print_books(books)
        elif choice == "0":
            break
        else:
            print("Неверный пункт меню")

main()