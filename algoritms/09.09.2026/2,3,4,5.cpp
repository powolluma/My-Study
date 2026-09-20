#include <iostream>
using namespace std;

int main() {
	setlocale(LC_ALL, "ru");

	// 2
	/*
	int a, b;
    
    cout << "Длина прямоугольника: ";
    cin >> a;
    
    cout << "Ширина прямоугольника: ";
    cin >> b;
    
    int square = a * b;
    int perimetr = (a * 2) + (b * 2);
    
    cout << "Площадь: " << square << endl;
    cout << "Периметр: " << perimetr << endl;
	*/

    // 3
    /*
    int a, b;
    
    cout << "a: ";
    cin >> a;
    
    cout << "b: ";
    cin >> b;

    a = a + b;
    b = a - b;
    a = a - b;

    cout << "a = " << a << ", b = " << b << endl;
    */

    // 4
    /*
    int start_second;
    cout << "Введите кол-во секунд: ";
    cin >> start_second;

    int hours = start_second / 3600;    
    int minute = (start_second % 3600) / 60;
    int end_second = start_second % 60;

    cout << hours << " часов, " << minute << " минут, " << end_second << " секунд" << endl;
    */

    // 5
    /*
    double x, y;
    
    cout << "x: ";
    cin >> x;
    
    cout << "y: ";
    cin >> y;

    double z = (pow(x, 2) + pow(y, 2)) / (2 * x * y) + (x - y) / (x + y);

    cout << "z = " << z << endl;
    */

	return 0;
}