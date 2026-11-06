class Product:
    def __init__(self, name, price, quantity):
        self.name = name
        self.price = price
        self.quantity = quantity

    @property
    def name(self):
        return self._name

    @name.setter
    def name(self, value):
        if not isinstance(value, str) or not value.strip():
            raise ValueError("Название не должно быть пустым")
        if len(value) > 50:
            raise ValueError("Название не должно быть длиннее 50 символов")
        self._name = value

    @property
    def price(self):
        return self._price

    @price.setter
    def price(self, value):
        if isinstance(value, bool) or not isinstance(value, (int, float)) or value <= 0:
            raise ValueError("Цена должна быть больше 0")
        self._price = float(value)

    @property
    def quantity(self):
        return self._quantity

    @quantity.setter
    def quantity(self, value):
        if isinstance(value, bool) or not isinstance(value, int) or value < 0:
            raise ValueError("Количество должно быть целым и неотрицательным")
        self._quantity = value

    @property
    def total_cost(self):
        return self._price * self._quantity

p = Product("Ноутбук", 75000, 3)

print(p.total_cost)  

p.price = 80000
print(p.total_cost)  