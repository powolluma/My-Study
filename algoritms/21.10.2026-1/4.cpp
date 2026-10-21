#include <iostream>
using namespace std;

struct Node {
    int value;
    Node* next;
};

Node* reverse(Node* head) {
    Node* prev = nullptr;

    while (head) {
        Node* next = head->next;
        head->next = prev;
        prev = head;
        head = next;
    }

    return prev;
}

bool isPalindrome(Node* head) {
    Node* slow = head;
    Node* fast = head;

    while (fast && fast->next) {
        slow = slow->next;
        fast = fast->next->next;
    }

    if (fast)
        slow = slow->next;

    Node* second = reverse(slow);
    Node* p = head;
    Node* q = second;
    bool result = true;

    while (q) {
        if (p->value != q->value) {
            result = false;
            break;
        }

        p = p->next;
        q = q->next;
    }

    reverse(second);
    return result;
}

int main() {
    Node* head = new Node{1,
        new Node{2,
        new Node{3,
        new Node{2,
        new Node{1, nullptr}}}}};

    cout << (isPalindrome(head) ? "Да" : "Нет");

    return 0;
}