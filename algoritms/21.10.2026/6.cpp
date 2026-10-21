#include <iostream>
#include <queue>
using namespace std;

class Stack {
    queue<int> q1, q2;

public:
    void push(int x) {
        q2.push(x);

        while (!q1.empty()) {
            q2.push(q1.front());
            q1.pop();
        }

        swap(q1, q2);
    }

    bool pop(int& x) {
        if (q1.empty()) {
            cout << "Стек пуст\n";
            return false;
        }

        x = q1.front();
        q1.pop();
        return true;
    }

    bool top(int& x) {
        if (q1.empty())
            return false;

        x = q1.front();
        return true;
    }
};

int main() {
    Stack s;
    int x;

    s.push(1);
    s.push(2);
    s.push(3);

    if (s.top(x))
        cout << "Вершина: " << x << '\n';

    while (s.pop(x))
        cout << x << ' ';

    return 0;
}