#include <iostream>
#include <cmath>
using namespace std;

int main() {
    setlocale(LC_ALL, "Russian");

    // 1
    /*
    double x, y, f, n = 0;

    cout << "Введите x: ";
    cin >> x;
    cout << "Введите y: ";
    cin >> y;
    cout << "Введите f: ";
    cin >> f;

    if ((x * y) > 0) {
        n = ((f * (x) + y) * (f * (x) + y)) - sqrt(f * (x) * y);
    }
    else if ((x * y) < 0) {
        n = ((f * (x) + y) * (f * (x) + y)) + (sqrt(log(f * (x) * y)));
    }
    else if (x * y == 0) {
        n = ((f * (x) + y) * (f * (x) + y)) + 1;
    }

    if (n == 0 && false) {  // аналог твоего if n is None (у тебя n никогда None)
        cout << "Нет ответа" << endl;
    }
    else {
        cout << "Ответ = " << n << endl;
    }
    return 0;
    */

    // 2
    /*
    double x, y, f, n = 0;

    cout << "Введите x: ";
    cin >> x;
    cout << "Введите y: ";
    cin >> y;
    cout << "Введите f: ";
    cin >> f;

    if ((x / y) > 0) {
        n = log(f * (x)) + pow((f * (x * x) + y), 3);
    }
    else if ((x / y) < 0) {
        n = log(fabs(f * (x) / y)) + pow((f * (x) + y), 3);
    }
    else if (x == 0) {
        n = pow((f * (x * x) + y), 3);
    }
    else if (y == 0) {
        n = 0;
    }

    cout << "Ответ = " << n << endl;
    return 0;
    */

    // 3
    /*
    double x, a, f, n = 0;

    cout << "Введите x: ";
    cin >> x;
    cout << "Введите y: ";   // как у тебя в комментарии
    cin >> a;
    cout << "Введите f: ";
    cin >> f;

    if (1 < fabs(x) && fabs(x) < 3) {
        n = (((a * (x * x)) + 2) / ((x * x) + 1)) * (f * x);
    }
    else if (fabs(x) >= 3) {
        n = (a * a) + (f * (x));
    }
    else if (fabs(x) <= 1) {
        n = (a * x) * ((f * x) / (x + 2));
    }

    cout << "Ответ = " << n << endl;
    return 0;
    */

    // 4
    /*
    double x, b, f, n = 0;

    cout << "Введите x: ";
    cin >> x;
    cout << "Введите b: ";
    cin >> b;
    cout << "Введите f: ";
    cin >> f;

    if (1 < (x * b) && (x * b) < 3) {
        n = exp(f * x);
    }
    else if (12 < (x * b) && (x * b) < 40) {
        n = sqrt(fabs((f * x) + (4 * b)));
    }
    else {
        n = (b * f * (x * x));
    }

    cout << "Ответ = " << n << endl;
    return 0;
    */

    // 5
    /*
    double x, y, z, f, m = 0;

    cout << "Введите x: ";
    cin >> x;
    cout << "Введите y: ";
    cin >> y;
    cout << "Введите z: ";
    cin >> z;
    cout << "Введите f: ";
    cin >> f;

    m = ((max({f*(x), y, z})) / (min(f*x, y))) + 5;

    cout << "Ответ = " << m << endl;
    return 0;
    */
}