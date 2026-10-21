#include <iostream>
#include <stack>
using namespace std;

void sortStack(stack<int>& s) {
    stack<int> temp;

    while (!s.empty()) {
        int x = s.top();
        s.pop();

        while (!temp.empty() && temp.top() < x) {
            s.push(temp.top());
            temp.pop();
        }

        temp.push(x);
    }

    while (!temp.empty()) {
        s.push(temp.top());
        temp.pop();
    }
}

int main() {
    stack<int> s;

    s.push(2);
    s.push(1);
    s.push(3);

    sortStack(s);

    while (!s.empty()) {
        cout << s.top() << ' ';
        s.pop();
    }

    return 0;
}