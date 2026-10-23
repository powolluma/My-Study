#include <iostream>
#include <string>
#include <vector>
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
    Circle(double r) : radius(r) {}

    double area() const override {
        return 3.14 * radius * radius;
    }

    string getName() const override {
        return "Круг";
    }
};

class Rectangle : public Shape {
private:
    double width, height;

public:
    Rectangle(double w, double h)
        : width(w), height(h) {}

    double area() const override {
        return width * height;
    }

    string getName() const override {
        return "Прямоугольник";
    }
};

class Triangle : public Shape {
private:
    double width, height;

public:
    Triangle(double w, double h)
        : width(w), height(h) {}

    double area() const override {
        return width * height / 2;
    }

    string getName() const override {
        return "Треугольник";
    }
};

double total_area(const vector<Shape*>& shapes) {
    double total = 0;

    for (Shape* shape : shapes) {
        total += shape->area();
    }

    return total;
}

int main() {
    Circle circle(5);
    Rectangle rectangle(4, 6);
    Triangle triangle(4, 3);

    vector<Shape*> shapes = {
        &circle, &rectangle, &triangle
    };

    for (Shape* shape : shapes) {
        shape->describe();
    }

    cout << "Общая площадь: "
         << total_area(shapes) << endl;

    return 0;
}