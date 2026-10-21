#include <iostream>
#include <stack>
using namespace std;

int main() {
    int a[] = {1, 2, 3, 4, 5};
    int n = sizeof(a) / sizeof(a[0]);

    stack<int> s;

    for (int i = 0; i < n; i++)
        s.push(a[i]);

    for (int i = 0; i < n; i++) {
        a[i] = s.top();
        s.pop();
    }

    for (int i = 0; i < n; i++)
        cout << a[i] << ' ';

    return 0;
}