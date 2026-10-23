from datetime import date


class Product:
    def __init__(self, name, price):
        if price < 0:
            raise ValueError("Цена не может быть отрицательной")

        self.name = name
        self.__price = price

    def price(self):
        return self.__price


class Book(Product):
    def __init__(self, name, price, author):
        super().__init__(name, price)
        self.author = author


class Electronics(Product):
    def __init__(self, name, price, warranty_months):
        super().__init__(name, price)
        self.warranty_months = warranty_months


class Food(Product):
    def __init__(self, name, price, expiry_date):
        super().__init__(name, price)
        self.expiry_date = expiry_date

    def price(self):
        if date.today() >= self.expiry_date:
            return super().price() * 0.5

        return super().price()


class Cart:
    def __init__(self):
        self.products = []

    def add(self, product):
        self.products.append(product)

    def total(self):
        total = 0

        for product in self.products:
            total += product.price()

        return total

    def receipt(self):
        print("ЧЕК")

        for product in self.products:
            print(f"{product.name} - {product.price():.2f} руб.")

        print(f"Итого: {self.total():.2f} руб.")


book = Book("Книга", 500, "Иван Иванов")
computer = Electronics("Компьютер", 50000, 24)
milk = Food("Молоко", 100, date(2025, 1, 1))

cart = Cart()

cart.add(book)
cart.add(computer)
cart.add(milk)

cart.receipt()