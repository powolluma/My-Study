#include <iostream>
using namespace std;

struct Node {
    int value;
    Node* next;
};

bool removeNth(Node*& head, int n) {
    if (n <= 0) return false;

    Node dummy{0, head};
    Node* fast = &dummy;
    Node* slow = &dummy;

    for (int i = 0; i < n; i++) {
        if (!fast->next) return false;
        fast = fast->next;
    }

    while (fast->next) {
        fast = fast->next;
        slow = slow->next;
    }

    Node* p = slow->next;
    slow->next = p->next;
    delete p;

    head = dummy.next;
    return true;
}

int main() {
    Node* head = new Node{1,
        new Node{2,
        new Node{3,
        new Node{4,
        new Node{5, nullptr}}}}};

    removeNth(head, 2);

    for (Node* p = head; p; p = p->next)
        cout << p->value << ' ';

    return 0;
}