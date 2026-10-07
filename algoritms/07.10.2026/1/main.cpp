#include <iostream>
#include <vector>
using namespace std;

int sumPositive(const vector<int>& a) {
    int s = 0;
    for (int x : a)
        if (x > 0) s += x;
    return s;
}

int countNegative(const vector<int>& a) {
    int c = 0;
    for (int x : a)
        if (x < 0) c++;
    return c;
}

int maxElement(const vector<int>& a) {
    int m = a[0];
    for (int x : a)
        if (x > m) m = x;
    return m;
}

double average(const vector<int>& a) {
    double s = 0;
    for (int x : a) s += x;
    return s / a.size();
}

int main() {
    int n;
    cout << "Размер массива: ";
    cin >> n;
    vector<int> a(n);
    for (int i = 0; i < n; i++) cin >> a[i];
    cout << "Сумма положительных: " << sumPositive(a) << endl;
    cout << "Количество отрицательных: " << countNegative(a) << endl;
    cout << "Максимум: " << maxElement(a) << endl;
    cout << "Среднее: " << average(a) << endl;
    return 0;
}