#ifndef POINT_H
#define POINT_H

class Point {
private:
    double x;
    double y;

public:
    Point(double x, double y);
    double distanceTo(const Point& other) const;
    double getX() const;
    double getY() const;
};

#endif
