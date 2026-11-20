#include <iostream>
#include <limits>
using namespace std;

class Calculator {
public:
    double add(double a, double b) {
        return a + b;
    }

    double sub(double a, double b) {
        return a - b;
    }

    double mul(double a, double b) {
        return a * b;
    }

    double div(double a, double b) {
        if (b == 0) {
            throw std::invalid_argument("Деление на ноль");
        }

        return a / b;
    }
};

void runUI() {
    Calculator calc;
    int command;
    double a, b;

    while (true) {
        cout << "\nКалькулятор" << endl;
        cout << "1 - Сложение" << endl;
        cout << "2 - Вычитание" << endl;
        cout << "3 - Умножение" << endl;
        cout << "4 - Деление" << endl;
        cout << "0 - Выход" << endl;
        cout << "Выберите действие: ";

        if (!(cin >> command)) {
            if (cin.eof()) {
                break;
            }

            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
            cout << "Ошибка: введите число" << endl;
            continue;
        }

        if (command == 0) {
            cout << "Выход из программы" << endl;
            break;
        }

        if (command < 1 || command > 4) {
            cout << "Неверная команда" << endl;
            continue;
        }

        cout << "Введите первое число: ";
        if (!(cin >> a)) {
            if (cin.eof()) {
                break;
            }

            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
            cout << "Ошибка ввода числа" << endl;
            continue;
        }

        cout << "Введите второе число: ";
        if (!(cin >> b)) {
            if (cin.eof()) {
                break;
            }

            cin.clear();
            cin.ignore(numeric_limits<streamsize>::max(), '\n');
            cout << "Ошибка ввода числа" << endl;
            continue;
        }

        try {
            double result;

            switch (command) {
                case 1:
                    result = calc.add(a, b);
                    break;
                case 2:
                    result = calc.sub(a, b);
                    break;
                case 3:
                    result = calc.mul(a, b);
                    break;
                case 4:
                    result = calc.div(a, b);
                    break;
            }

            cout << "Результат: " << result << endl;
        }
        catch (const exception& e) {
            cout << "Ошибка: " << e.what() << endl;
        }
    }
}

int main() {
    runUI();
    return 0;
}
