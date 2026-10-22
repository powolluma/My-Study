#include <iostream>
using namespace std;

class Node
{
public:
    int Data;
    Node* Next;

    Node(int data)
    {
        Data = data;
        Next = nullptr;
    }
};

class SingleLinkedList
{
private:
    Node* _head;

public:
    SingleLinkedList()
    {
        _head = nullptr;
    }

    ~SingleLinkedList()
    {
        while (_head != nullptr)
        {
            Node* temp = _head;
            _head = _head->Next;
            delete temp;
        }
    }

    void AddFirst(int value)
    {
        Node* newNode = new Node(value);
        newNode->Next = _head;
        _head = newNode;
    }

    void Print()
    {
        Node* current = _head;

        while (current != nullptr)
        {
            cout << current->Data << " -> ";
            current = current->Next;
        }

        cout << "null" << endl;
    }

    int Find(int value)
    {
        Node* current = _head;
        int position = 1;

        while (current != nullptr)
        {
            if (current->Data == value)
                return position;

            current = current->Next;
            position++;
        }

        return -1;
    }

    int Sum()
    {
        int sum = 0;
        Node* current = _head;

        while (current != nullptr)
        {
            sum += current->Data;
            current = current->Next;
        }

        return sum;
    }

    void InsertAfter(int targetValue, int newValue)
    {
        Node* current = _head;

        while (current != nullptr && current->Data != targetValue)
        {
            current = current->Next;
        }

        if (current == nullptr)
        {
            cout << "Element not found" << endl;
            return;
        }

        Node* newNode = new Node(newValue);
        newNode->Next = current->Next;
        current->Next = newNode;
    }

    void DeleteAfter(int targetValue)
    {
        Node* current = _head;

        while (current != nullptr && current->Data != targetValue)
        {
            current = current->Next;
        }

        if (current == nullptr || current->Next == nullptr)
        {
            cout << "Cannot delete element" << endl;
            return;
        }

        Node* temp = current->Next;
        current->Next = temp->Next;
        delete temp;
    }

    void DeleteFirst()
    {
        if (_head == nullptr)
            return;

        Node* temp = _head;
        _head = _head->Next;
        delete temp;
    }

    void Reverse()
    {
        Node* prev = nullptr;
        Node* current = _head;

        while (current != nullptr)
        {
            Node* next = current->Next;
            current->Next = prev;
            prev = current;
            current = next;
        }

        _head = prev;
    }
};

int main()
{
    SingleLinkedList list;

    list.AddFirst(3);
    list.AddFirst(2);
    list.AddFirst(1);

    cout << "Список:" << endl;
    list.Print();

    cout << "Позиция числа 2: " << list.Find(2) << endl;
    cout << "Сумма: " << list.Sum() << endl;

    list.InsertAfter(2, 5);
    cout << "После вставки:" << endl;
    list.Print();

    list.DeleteAfter(2);
    cout << "После удаления:" << endl;
    list.Print();

    list.DeleteFirst();
    cout << "После удаления первого элемента:" << endl;
    list.Print();

    list.Reverse();
    cout << "После разворота:" << endl;
    list.Print();

    return 0;
}