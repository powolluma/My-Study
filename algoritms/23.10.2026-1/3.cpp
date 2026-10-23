#include <iostream>
#include <string>
using namespace std;

class Shape {
public:
    virtual double area() const = 0;
    virtual string getName() const = 0;

    void describe() const {
        cout << getName() << ", площадь: "
             << area() << endl;
    }

    virtual ~Shape() {}
};

class Circle : public Shape {
private:
    double radius;

public:
    Circle(double r) {
        radius = r;
    }

    double area() const override {
        return 3.14 * radius * radius;
    }

    string getName() const override {
        return "Круг";
    }
};

class Rectangle : public Shape {
private:
    double width;
    double height;

public:
    Rectangle(double w, double h) {
        width = w;
        height = h;
    }

    double area() const override {
        return width * height;
    }

    string getName() const override {
        return "Прямоугольник";
    }
};

int main() {
    Circle circle(5);
    Rectangle rectangle(4, 6);

    circle.describe();
    rectangle.describe();

    return 0;
}