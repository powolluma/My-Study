#include <iostream>
#include <vector>
using namespace std;

struct Node {
    bool isNumber;
    int value;
    vector<Node*> children;
};

int sum(Node* node) {
    if (node->isNumber)
        return node->value;

    int result = 0;

    for (Node* p : node->children)
        result += sum(p);

    return result;
}

int main() {
    Node* inner = new Node{false, 0, {
        new Node{true, 3, {}},
        new Node{true, 4, {}}
    }};

    Node* middle = new Node{false, 0, {
        new Node{true, 2, {}},
        inner
    }};

    Node* root = new Node{false, 0, {
        new Node{true, 1, {}},
        middle,
        new Node{true, 5, {}}
    }};

    cout << sum(root);

    return 0;
}