#include "IOperation.h"
#include <iostream>
#include <vector>
#include <string>
#include <stdexcept>
#include <dlfcn.h>
using namespace std;

class Calculator {
private:
    vector<IOperation*> operations;

public:
    Calculator(const vector<IOperation*>& ops) {
        operations = ops;
    }

    double calculate(string name, double a, double b) {
        for (IOperation* op : operations) {
            if (op->name() == name) {
                return op->apply(a, b);
            }
        }

        throw runtime_error("Операция не найдена");
    }
};

using CreateOperation = IOperation* (*)();

int main() {
    vector<string> files = {
        "./add.so",
        "./sub.so",
        "./mul.so",
        "./div.so"
    };

    vector<void*> libraries;
    vector<IOperation*> operations;

    for (string file : files) {
        void* library = dlopen(file.c_str(), RTLD_NOW);

        if (!library) {
            cout << "Ошибка загрузки: " << file << endl;
            continue;
        }

        auto create = reinterpret_cast<CreateOperation>(
            dlsym(library, "createOperation")
        );

        if (!create) {
            dlclose(library);
            continue;
        }

        libraries.push_back(library);
        operations.push_back(create());
    }

    Calculator calc(operations);

    cout << calc.calculate("+", 10, 5) << endl;
    cout << calc.calculate("-", 10, 5) << endl;
    cout << calc.calculate("*", 10, 5) << endl;

    try {
        cout << calc.calculate("/", 10, 0) << endl;
    } catch (const exception& e) {
        cout << "Ошибка: " << e.what() << endl;
    }

    for (IOperation* op : operations) {
        delete op;
    }

    for (void* library : libraries) {
        dlclose(library);
    }

    return 0;
}