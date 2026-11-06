#pragma once
#include <string>

class Shape {
public:
    explicit Shape(std::string name);
    virtual ~Shape() = default;

    virtual double area() const = 0;
    std::string name() const;

private:
    std::string name_;
};