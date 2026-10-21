#include <iostream>
#include <string>
using namespace std;

struct Node {
    string name;
    Node* left;
    Node* right;
};

void insert(Node*& root, string name) {
    if (!root) {
        root = new Node{name, nullptr, nullptr};
        return;
    }

    if (name < root->name)
        insert(root->left, name);
    else if (name > root->name)
        insert(root->right, name);
}

void inorder(Node* root) {
    if (!root) return;

    inorder(root->left);
    cout << root->name << ' ';
    inorder(root->right);
}

int height(Node* root) {
    if (!root) return 0;

    int left = height(root->left);
    int right = height(root->right);

    return 1 + (left > right ? left : right);
}

string minName(Node* root) {
    while (root->left)
        root = root->left;

    return root->name;
}

string maxName(Node* root) {
    while (root->right)
        root = root->right;

    return root->name;
}

int main() {
    Node* root = nullptr;

    insert(root, "Иван");
    insert(root, "Анна");
    insert(root, "Петр");
    insert(root, "Мария");
    insert(root, "Олег");

    cout << "Имена: ";
    inorder(root);

    cout << "\nВысота: " << height(root);
    cout << "\nМинимум: " << minName(root);
    cout << "\nМаксимум: " << maxName(root);

    return 0;
}