#include <iostream>
#include <string>
using namespace std;

struct Node {
    string name;
    Node* prev;
    Node* next;
};

class Metro {
    Node* head = nullptr;
    Node* tail = nullptr;

    int indexOf(string name) {
        int i = 0;

        for (Node* p = head; p; p = p->next, i++)
            if (p->name == name)
                return i;

        return -1;
    }

public:
    void add(string name) {
        Node* p = new Node{name, tail, nullptr};

        if (tail)
            tail->next = p;
        else
            head = p;

        tail = p;
    }

    void remove(string name) {
        Node* p = head;

        while (p && p->name != name)
            p = p->next;

        if (!p) return;

        if (p->prev)
            p->prev->next = p->next;
        else
            head = p->next;

        if (p->next)
            p->next->prev = p->prev;
        else
            tail = p->prev;

        delete p;
    }

    int distanceBetween(string a, string b) {
        int i = indexOf(a);
        int j = indexOf(b);

        if (i == -1 || j == -1)
            return -1;

        return i > j ? i - j : j - i;
    }
};

int main() {
    Metro m;

    m.add("A");
    m.add("B");
    m.add("C");
    m.add("D");

    cout << m.distanceBetween("A", "D") << '\n';

    m.remove("B");

    cout << m.distanceBetween("A", "D");

    return 0;
}