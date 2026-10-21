#include <iostream>
using namespace std;

class Stack {
    int a[50];
    int t = -1;

public:
    bool isEmpty() { return t == -1; }
    bool isFull() { return t == 49; }

    void push(int x) {
        if (isFull()) {
            cout << "Стек переполнен\n";
            return;
        }
        a[++t] = x;
    }

    void pop() {
        if (isEmpty()) {
            cout << "Стек пуст\n";
            return;
        }
        cout << "Удалено: " << a[t--] << '\n';
    }

    void top() {
        if (isEmpty()) {
            cout << "Стек пуст\n";
            return;
        }
        cout << "Вершина: " << a[t] << '\n';
    }
};

int main() {
    Stack s;

    s.push(10);
    s.push(20);
    s.top();

    cout << "Пустой: " << s.isEmpty() << '\n';
    cout << "Полный: " << s.isFull() << '\n';

    int x = 1;
    while (!s.isFull())
        s.push(x++);

    cout << "Полный: " << s.isFull() << '\n';
    s.push(100);

    while (!s.isEmpty())
        s.pop();

    s.pop();

    return 0;
}