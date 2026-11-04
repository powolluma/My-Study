#include "Figure.h"
#include <cmath>
using namespace std;

Figure::~Figure() = default;

Triangle::Triangle(const Point& a, const Point& b, const Point& c)
    : a(a), b(b), c(c) {}

double Triangle::area() const {
    double result = a.getX() * (b.getY() - c.getY())
                  + b.getX() * (c.getY() - a.getY())
                  + c.getX() * (a.getY() - b.getY());
    return abs(result) / 2.0;
}
