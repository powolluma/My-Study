#include <iostream>
#include <string>
using namespace std;

class Animal {
public:
    string name;

    Animal(string n) {
        name = n;
    }

    virtual void speak() {
        cout << "Какой то звук" << endl;
    }
};

class Dog : public Animal {
public:
    Dog(string n) : Animal(n) {}

    void speak() override {
        cout << "Гав" << endl;
    }
};

class Cat : public Animal {
public:
    Cat(string n) : Animal(n) {}

    void speak() override {
        cout << "Мяу" << endl;
    }
};

int main() {
    Dog dog("Тузик");
    Cat cat("Муся");

    cout << dog.name << ": ";
    dog.speak();

    cout << cat.name << ": ";
    cat.speak();

    return 0;
}