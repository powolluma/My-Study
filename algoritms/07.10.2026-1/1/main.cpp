#include <iostream>
using namespace std;

int sumDigits(int n) {
    if (n < 10) return n;
    return n % 10 + sumDigits(n / 10);
}

int main() {
    int n;
    cout << "Введите число: ";
    cin >> n;
    cout << sumDigits(n) << endl;
    return 0;
}