#include <iostream>
#include <cmath>
using namespace std;

class Point {
private:
    double x, y;

public:
    Point(double x, double y) {
        this->x = x;
        this->y = y;
    }

    void move(double dx, double dy) {
        x += dx;
        y += dy;
    }

    double distanceTo(const Point& other) {
        return sqrt(
            pow(other.x - x, 2) +
            pow(other.y - y, 2)
        );
    }

    void show() {
        cout << "Координаты: " << x << " " << y << endl;
    }
};

int main() {
    Point p1(1, 2);
    Point p2(4, 6);

    p1.move(1, 1);

    p1.show();
    p2.show();

    cout << "Расстояние: " << p1.distanceTo(p2) << endl;

    return 0;
}