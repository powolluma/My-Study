#pragma once
#include <string>

class IOperation {
public:
    virtual double apply(double a, double b) const = 0;
    virtual std::string name() const = 0;

    virtual ~IOperation() = default;
};