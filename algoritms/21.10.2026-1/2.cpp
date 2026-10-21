#include <iostream>
using namespace std;

int main() {
    int x = 42;
    int* p = &x;
    int* q = &x;

    *p = 100;
    cout << *q << '\n';

    q = nullptr;
    cout << *p << '\n';

    return 0;
}