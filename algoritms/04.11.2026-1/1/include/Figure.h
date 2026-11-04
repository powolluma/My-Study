#ifndef FIGURE_H
#define FIGURE_H

#include "Point.h"

class Figure {
public:
    virtual double area() const = 0;
    virtual ~Figure();
};

class Triangle : public Figure {
private:
    Point a;
    Point b;
    Point c;

public:
    Triangle(const Point& a, const Point& b, const Point& c);
    double area() const override;
};

#endif
