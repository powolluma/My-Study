# Главный модуль: запуск и меню
from game import play_game


def main():
    while True:
        print("=== Крестики-нолики ===")
        print("1. Играть")
        print("0. Выход")
        choice = input("Выбор: ").strip()
        if choice == "1":
            play_game()
        elif choice == "0":
            print("До свидания!")
            break
        else:
            print("Неверный пункт меню.")


if __name__ == "__main__":
    main()