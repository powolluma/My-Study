import contact_io as io
import contact_ops as ops
import contact_file as files

FILENAME = "contacts.json"


def main():
    book = files.load_contacts(FILENAME)
    while True:
        choice = io.show_menu()
        if choice == "1":
            name, phone, email = io.input_contact()
            print("Добавлено." if ops.add_contact(book, name, phone, email) else "Имя не может быть пустым.")
        elif choice == "2":
            print("Удалено." if ops.remove_contact(book, io.input_name()) else "Контакт не найден.")
        elif choice == "3":
            io.print_contacts(ops.find_contacts(book, io.input_name()))
        elif choice == "4":
            io.print_contacts(book)
        elif choice == "5":
            files.save_contacts(book, FILENAME)
            print("Сохранено.")
        elif choice == "6":
            book = files.load_contacts(FILENAME)
            print("Загружено.")
        elif choice == "0":
            files.save_contacts(book, FILENAME)
            print("Данные сохранены. До свидания!")
            break
        else:
            print("Неверный пункт меню.")


if __name__ == "__main__":
    main()