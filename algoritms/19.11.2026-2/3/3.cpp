#include <iostream>
#include <vector>
#include <memory>
#include <string>
#include <cmath>
using namespace std;

class IShape {
public:
    virtual double area() const = 0;
    virtual double perimeter() const = 0;
    virtual string name() const = 0;

    virtual ~IShape() = default;
};

class Circle : public IShape {
private:
    double radius;

public:
    Circle(double r) {
        radius = r;
    }

    double area() const override {
        return 3.14 * radius * radius;
    }

    double perimeter() const override {
        return 2 * 3.14 * radius;
    }

    std::string name() const override {
        return "Круг";
    }
};

class Rectangle : public IShape {
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

    double perimeter() const override {
        return 2 * (width + height);
    }

    string name() const override {
        return "Прямоугольник";
    }
};

class Triangle : public IShape {
private:
    double a;
    double b;
    double c;

public:
    Triangle(double a, double b, double c) {
        this->a = a;
        this->b = b;
        this->c = c;
    }

    double area() const override {
        double p = (a + b + c) / 2;
        return sqrt(p * (p - a) * (p - b) * (p - c));
    }

    double perimeter() const override {
        return a + b + c;
    }

    string name() const override {
        return "Треугольник";
    }
};

void printInfo(const IShape& s) {
    cout << "Фигура: " << s.name() << endl;
    cout << "Площадь: " << s.area() << endl;
    cout << "Периметр: " << s.perimeter() << endl;
    cout << endl;
}

int main() {
    vector<unique_ptr<IShape>> shapes;

    shapes.push_back(make_unique<Circle>(5));
    shapes.push_back(make_unique<Rectangle>(4, 6));
    shapes.push_back(make_unique<Triangle>(3, 4, 5));

    for (const auto& shape : shapes) {
        printInfo(*shape);
    }

    return 0;
}
