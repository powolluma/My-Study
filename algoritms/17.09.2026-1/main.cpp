#include <iostream>
#include <cmath>
using namespace std;

int main() {
    // 1 
    /*
    double x0 = -1.5, xk = 3.5, dx = 0.5;
    double a = -1.25, b = -1.5, c = 0.75;

    for (double x = x0; x <= xk + dx / 2; x += dx) {
        if (x <= 0)
            cout << "x = " << x << ", y = ?" << endl;
        else {
            double y = (0.01 * b * c) / x + cos(sqrt(pow(a, 3) * x));
            cout << "x = " << x << ", y = " << y << endl;
        }
    }
    return 0;
    */

    // 2
    /*
    double x0 = 5.3, xk = 10.3, dx = 0.25;
    double a = 1.35, b = -6.25;

    for (double x = x0; x <= xk + dx / 2; x += dx) {
        if (x <= 0)
            cout << "x = " << x << " y = ?" << endl;
        else {
            double y = a * pow(x, 3) + pow(cos(pow(x, 3) - b), 2);
            cout << "x = " << x << ", y = " << y << endl;
        }
    }
    return 0;
    */

    // 3
    /*
    double x0 = -0.75, xk = -2.05, dx = -0.2;

    for (double x = x0; x >= xk + dx / 2; x += dx) {
        if (x <= 0)
            cout << "x = " << x << " y = ?" << endl;
        else {
            double y = (9 * pow(x, 4)) + sin(57.2 + x);
            cout << "x = " << x << ", y = " << y << endl;
        }
    }
    return 0;
    */

    // 4
    /*
    double x0 = -0.73, xk = -1.73, dx = -0.1;
    double b = -2;

    for (double x = x0; x >= xk + dx / 2; x += dx) {
        if (x <= 0)
            cout << "x = " << x << " y = ?" << endl;
        else {
            double y = sqrt(log(x - b)) / pow(pow(b, 3) - pow(x, 3), 1.5) + log(x - b);
            cout << "x = " << x << ", y = " << y << endl;
        }
    }
    return 0;
    */

    // 5
    /*
    double x0 = 1.23, xk = -2.4, dx = -0.3;
    double b = 12.6;

    for (double x = x0; x >= xk + dx / 2; x += dx) {
        if (x <= 0)
            cout << "x = " << x << " y = ?" << endl;
        else {
            double y = 15.28 * log(pow(x, -1.5)) + cos(log(x) + b);
            cout << "x = " << x << ", y = " << y << endl;
        }
    }
    return 0;
    */

    
}
