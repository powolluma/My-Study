#include "Matrix.h"
#include <stdexcept>
#include <utility>
using namespace std;

Matrix::Matrix(size_t rows, size_t columns)
    : rows(rows), columns(columns), data(new double[rows * columns]()) {}

Matrix::Matrix(Matrix&& other) noexcept
    : rows(other.rows), columns(other.columns), data(move(other.data)) {
    other.rows = 0;
    other.columns = 0;
}

double& Matrix::at(size_t row, size_t column) {
    if (row >= rows || column >= columns) {
        throw out_of_range("Matrix index is out of range");
    }
    return data[row * columns + column];
}

const double& Matrix::at(size_t row, size_t column) const {
    if (row >= rows || column >= columns) {
        throw out_of_range("Matrix index is out of range");
    }
    return data[row * columns + column];
}

size_t Matrix::getRows() const { return rows; }
size_t Matrix::getColumns() const { return columns; }
