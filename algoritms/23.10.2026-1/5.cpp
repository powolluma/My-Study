#include <iostream>
#include <string>
#include <vector>
#include <ctime>
#include <iomanip>
using namespace std;

string currentDate() {
    time_t now = time(nullptr);
    tm* date = localtime(&now);

    char buffer[11];
    strftime(buffer, sizeof(buffer), "%Y-%m-%d", date);

    return string(buffer);
}

class Product {
private:
    double price_;

public:
    string name;

    Product(string n, double p) {
        if (p < 0)
            throw invalid_argument("Цена не может быть отрицательной");

        name = n;
        price_ = p;
    }

    virtual double price() const {
        return price_;
    }

    virtual ~Product() {}
};

class Book : public Product {
public:
    string author;

    Book(string n, double p, string a)
        : Product(n, p), author(a) {}
};

class Electronics : public Product {
public:
    int warrantyMonths;

    Electronics(string n, double p, int months)
        : Product(n, p), warrantyMonths(months) {}
};

class Food : public Product {
private:
    string expiryDate;

public:
    Food(string n, double p, string date)
        : Product(n, p), expiryDate(date) {}

    double price() const override {
        if (currentDate() >= expiryDate)
            return Product::price() * 0.5;

        return Product::price();
    }
};

class Cart {
private:
    vector<Product*> products;

public:
    void add(Product* product) {
        products.push_back(product);
    }

    double total() const {
        double sum = 0;

        for (Product* product : products) {
            sum += product->price();
        }

        return sum;
    }

    void receipt() const {
        cout << "ЧЕК" << endl;

        for (Product* product : products) {
            cout << product->name << " - "
                 << fixed << setprecision(2)
                 << product->price() << " руб." << endl;
        }

        cout << "Итого: " << total() << " руб." << endl;
    }
};

int main() {
    Book book("Книга", 500, "Иван Иванов");
    Electronics computer("Компьютер", 50000, 24);
    Food milk("Молоко", 100, "2025-01-01");

    Cart cart;

    cart.add(&book);
    cart.add(&computer);
    cart.add(&milk);

    cart.receipt();

    return 0;
}