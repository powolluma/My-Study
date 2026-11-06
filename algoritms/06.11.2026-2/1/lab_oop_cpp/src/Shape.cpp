#include "Shape.h"
#include <utility>
using namespace std;

Shape::Shape(string name)
    : name_(move(name)) {}

string Shape::name() const {
    return name_;
}