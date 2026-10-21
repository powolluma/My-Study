#include <iostream>
using namespace std;

struct Node {
    int number;
    Node* next;
};

class Train {
    Node* head = nullptr;

public:
    void add(int x) {
        Node* p = new Node{x, nullptr};

        if (!head) {
            head = p;
            return;
        }

        Node* cur = head;
        while (cur->next)
            cur = cur->next;

        cur->next = p;
    }

    void removeLast() {
        if (!head) {
            cout << "Поезд пуст\n";
            return;
        }

        if (!head->next) {
            delete head;
            head = nullptr;
            return;
        }

        Node* p = head;
        while (p->next->next)
            p = p->next;

        delete p->next;
        p->next = nullptr;
    }

    int count() {
        int n = 0;
        for (Node* p = head; p; p = p->next)
            n++;
        return n;
    }

    void print() {
        cout << "ЛОКОМОТИВ";
        for (Node* p = head; p; p = p->next)
            cout << " - Вагон " << p->number;
        cout << " - конец\n";
    }
};

int main() {
    Train t;
    t.add(1);
    t.add(2);
    t.add(3);

    t.print();
    cout << "Вагонов: " << t.count() << '\n';

    t.removeLast();
    t.print();

    return 0;
}