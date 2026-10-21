#include <cmath>
#include <iostream>
#include <vector>
using namespace std;

class Shape {
public:
    virtual double area() const = 0;
    virtual ~Shape() = default;
};

class Rectangle : public Shape {
public:
    Rectangle(double width, double height) : width_(width), height_(height) {}

    double area() const override {
        return width_ * height_;
    }

private:
    double width_;
    double height_;
};

class Circle : public Shape {
public:
    explicit Circle(double radius) : radius_(radius) {}

    double area() const override {
        return acos(-1.0) * radius_ * radius_;
    }

private:
    double radius_;
};

int main() {
    Rectangle rectangle(4, 5);
    Circle circle(3);
    vector<Shape*> shapes{&rectangle, &circle};

    for (const Shape* shape : shapes) {
        cout << "Area: " << shape->area() << '\n';
    }
    return 0;
}
