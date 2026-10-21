#include <iostream>
using namespace std;

struct Node {
    int value;
    Node* next;
};

Node* rotate(Node* head, int k) {
    if (!head) return head;

    int n = 1;
    Node* p = head;

    while (p->next != head) {
        p = p->next;
        n++;
    }

    k %= n;

    while (k--)
        head = head->next;

    return head;
}

void print(Node* head) {
    Node* p = head;

    do {
        cout << p->value;
        p = p->next;
        if (p != head) cout << " -> ";
    } while (p != head);

    cout << " - (" << head->value << ")\n";
}

int main() {
    Node* a = new Node{1, nullptr};
    Node* b = new Node{2, nullptr};
    Node* c = new Node{3, nullptr};
    Node* d = new Node{4, nullptr};
    Node* e = new Node{5, nullptr};

    a->next = b;
    b->next = c;
    c->next = d;
    d->next = e;
    e->next = a;

    Node* head = rotate(a, 2);
    print(head);

    return 0;
}
