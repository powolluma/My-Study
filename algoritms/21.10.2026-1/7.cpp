#include <iostream>
using namespace std;

struct Node {
    int value;
    Node* next;
};

class Queue {
    Node* head = nullptr;
    Node* tail = nullptr;

public:
    void enqueue(int x) {
        Node* p = new Node{x, nullptr};

        if (!tail) {
            head = tail = p;
        } else {
            tail->next = p;
            tail = p;
        }
    }

    bool dequeue(int& x) {
        if (!head)
            return false;

        Node* p = head;
        x = p->value;
        head = head->next;

        if (!head)
            tail = nullptr;

        delete p;
        return true;
    }
};

int main() {
    Queue q;
    int x;

    q.enqueue(1);
    q.enqueue(2);

    while (q.dequeue(x))
        cout << x << ' ';

    q.enqueue(3);
    q.dequeue(x);
    cout << x;

    return 0;
}