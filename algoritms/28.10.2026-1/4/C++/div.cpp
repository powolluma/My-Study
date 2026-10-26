#include "IOperation.h"
#include <stdexcept>

class Div : public IOperation {
public:
    double apply(double a, double b) const override {
        if (b == 0) {
            throw std::runtime_error("Деление на ноль!");
        }

        return a / b;
    }

    std::string name() const override {
        return "/";
    }
};

extern "C" IOperation* createOperation() {
    return new Div();
}