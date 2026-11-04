#include "Point.h"
#include <cmath>
using namespace std;

Point::Point(double x, double y) : x(x), y(y) {}

double Point::distanceTo(const Point& other) const {
    double dx = x - other.x;
    double dy = y - other.y;
    return sqrt(dx * dx + dy * dy);
}

double Point::getX() const { return x; }
double Point::getY() const { return y; }
