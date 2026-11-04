#ifndef MATRIX_H
#define MATRIX_H

#include <memory>
#include <cstddef>
using namespace std;

class Matrix {
private:
    size_t rows;
    size_t columns;
    unique_ptr<double[]> data;

public:
    Matrix(size_t rows, size_t columns);
    Matrix(Matrix&& other) noexcept;

    Matrix(const Matrix&) = delete;
    Matrix& operator=(const Matrix&) = delete;

    double& at(size_t row, size_t column);
    const double& at(size_t row, size_t column) const;
    size_t getRows() const;
    size_t getColumns() const;
};

#endif
