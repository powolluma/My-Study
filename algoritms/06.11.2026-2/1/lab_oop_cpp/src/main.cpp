#include <iostream>
#include <memory>
#include <vector>
#include "Shape.h"
using namespace std;

class Circle : public Shape {
public:
    explicit Circle(double r) : Shape("Circle"), r_(r) {}

    double area() const override {
        return 3.14159 * r_ * r_;
    }

private:
    double r_;
};

class Square : public Shape {
public:
    explicit Square(double a) : Shape("Square"), a_(a) {}

    double area() const override {
        return a_ * a_;
    }

private:
    double a_;
};

int main() {
    vector<std::unique_ptr<Shape>> shapes;

    shapes.push_back(make_unique<Circle>(2.0));
    shapes.push_back(make_unique<Square>(3.0));

    for (const auto& s : shapes) {
        cout << s->name() << ": "
                  << s->area() << "\n";
    }

    return 0;
}