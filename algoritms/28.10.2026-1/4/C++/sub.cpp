#include "IOperation.h"

class Sub : public IOperation {
public:
    double apply(double a, double b) const override {
        return a - b;
    }

    std::string name() const override {
        return "-";
    }
};

extern "C" IOperation* createOperation() {
    return new Sub();
}