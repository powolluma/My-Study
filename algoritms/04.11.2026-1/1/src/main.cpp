#include <iostream>
#include "Figure.h"
using namespace std;

int main() {
    Triangle triangle(Point(0, 0), Point(4, 0), Point(0, 3));

    Figure* figure = &triangle;
    cout << "Area: " << figure->area() << endl;

    return 0;
}
