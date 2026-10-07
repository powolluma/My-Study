#include <iostream>
using namespace std;

int sumTo(int n) {
    int s = 0;
    for (int i = 1; i <= n; i++)
        s += i;
    return s;
}

int main() {
    int n;
    cin >> n;
    cout << sumTo(n) << endl;
    return 0;
}