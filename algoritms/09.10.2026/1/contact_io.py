# Модуль ввода-вывода
from contact_model import contact_to_str


def show_menu():
    print("\n=== Записная книжка ===")
    print("1. Добавить контакт")
    print("2. Удалить контакт")
    print("3. Найти контакт")
    print("4. Показать все контакты")
    print("5. Сохранить в файл")
    print("6. Загрузить из файла")
    print("0. Выход")
    return input("Выбор: ").strip()


def input_contact():
    name = input("Имя: ")
    phone = input("Телефон: ")
    email = input("Email: ")
    return name, phone, email


def input_name():
    return input("Введите имя: ")


def print_contacts(contacts):
    if not contacts:
        print("Список пуст.")
        return
    for i, c in enumerate(contacts, 1):
        print(f"{i}. {contact_to_str(c)}")