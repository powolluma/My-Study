#include <iostream>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");

	// 1
	/*
	int n, count = 1, a = 0;
    cout << "Введите число: ";
    cin >> n;
    
    while (count <= n) {
        a += count;
        count++;
    }
    
    cout << "Сумма всех чисел от 1 до " << n << " = " << a << endl;
    return 0;
	*/

    // 2
    /*
    int n, fact = 1;
    cout << "Введите число: ";
    cin >> n;
    
    for (int i = 1; i <= n; i++) {
        fact *= i;
    }
    
    cout << "Факториал числа " << n << " = " << fact << endl;
    return 0;
    */

    // 2
    /*
    int n;
    cout << "Введите кол-во звездочек: ";
    cin >> n;
    
    // Квадрат
    for (int i = 0; i < n; i++) {
        for (int j = 0; j < n; j++) {
            cout << "* ";
        }
        cout << endl;
    }
    cout << endl << endl;
    
    // Прямоугольный треугольник
    for (int i = 1; i <= n; i++) {
        for (int j = 0; j < i; j++) {
            cout << "* ";
        }
        cout << endl;
    }
    cout << endl << endl;
    
    // Перевёрнутый треугольник
    for (int i = n; i >= 1; i--) {
        for (int j = 0; j < i; j++) {
            cout << "* ";
        }
        cout << endl;
    }
    cout << endl << endl;
    
    // Ромб
    for (int i = 1; i <= n; i++) {
        for (int j = 0; j < n - i; j++) cout << " ";
        for (int j = 0; j < 2 * i - 1; j++) cout << "*";
        cout << endl;
    }
    for (int i = n; i >= 1; i--) {
        for (int j = 0; j < n - i; j++) cout << " ";
        for (int j = 0; j < 2 * i - 1; j++) cout << "*";
        cout << endl;
    }
    
    return 0;
    */

    // 3
    /*
    int count, n;
    double number = 0;
    
    cout << "Введите кол-во чисел: ";
    cin >> count;
    
    for (int i = 1; i <= count; i++) {
        cout << "Введите число: ";
        cin >> n;
        number += n;
    }
    
    cout << "Среднее " << count << " чисел = " << number / count << endl;
    return 0;
    */

    // 4
    /*
    int n, count = 0;
    cout << "Введите целое число: ";
    cin >> n;
    
    while (n > 0) {
        count++;
        n /= 10;
    }
    
    cout << "Кол-во цифр = " << count << endl;
    return 0;
    */

    // 5
    /*
    int n, a = 0, b = 1;
    cout << "Сколько чисел вывести: ";
    cin >> n;
    
    for (int i = 0; i < n; i++) {
        cout << a << endl;
        int temp = a;
        a = b;
        b = temp + b;
    }
    
    return 0;
    */

    // 6
    /*
    int n, a = 0, b = 1;
    cout << "Сколько чисел вывести: ";
    cin >> n;
    
    for (int i = 0; i < n; i++) {
        cout << a << endl;
        int temp = a;
        a = b;
        b = temp + b;
    }
    
    return 0;
    */
}