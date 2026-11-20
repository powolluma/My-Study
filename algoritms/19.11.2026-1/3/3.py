from abc import ABC, abstractmethod
from dataclasses import dataclass
import sqlite3



@dataclass
class CartItem:
    product_id: str
    quantity: int
    price: float



class Cart:
    def __init__(self, items):
        self.items = items

    def total(self):
        return sum(
            item.quantity * item.price
            for item in self.items
        )



class Inventory:
    def __init__(self, stock):
        self.stock = stock

    def check(self, cart):
        for item in cart.items:
            available = self.stock.get(item.product_id, 0)

            if available < item.quantity:
                return False

        return True



@dataclass
class Order:
    id: int
    items: list
    total: float



class OrderService:
    def __init__(self):
        self.next_id = 1

    def create(self, cart, total):
        order = Order(
            self.next_id,
            list(cart.items),
            total
        )

        self.next_id += 1
        return order



class Repository(ABC):

    @abstractmethod
    def save(self, order):
        pass



class FileRepository(Repository):

    def save(self, order):
        with open("orders.txt", "a", encoding="utf-8") as file:
            file.write(
                f"Заказ: {order.id}, "
                f"Сумма: {order.total}\n"
            )



class DatabaseRepository(Repository):

    def __init__(self):
        with sqlite3.connect("shop.db") as db:
            db.execute("""
                CREATE TABLE IF NOT EXISTS orders (
                    id INTEGER PRIMARY KEY,
                    total REAL
                )
            """)

    def save(self, order):
        with sqlite3.connect("shop.db") as db:
            db.execute(
                "INSERT INTO orders (id, total) VALUES (?, ?)",
                (order.id, order.total)
            )



class CheckoutService:

    def __init__(self, inventory, order_service, repository):
        self.inventory = inventory
        self.order_service = order_service
        self.repository = repository

    def checkout(self, cart):

        if not cart.items:
            raise ValueError("Корзина пустая")

        if not self.inventory.check(cart):
            raise ValueError("Недостаточно товаров на складе")

        total = cart.total()

        order = self.order_service.create(cart, total)

        self.repository.save(order)

        return order



items = [
    CartItem("1", 2, 100),
    CartItem("2", 1, 250)
]

cart = Cart(items)

inventory = Inventory({
    "1": 10,
    "2": 5
})

order_service = OrderService()

repository = FileRepository()

checkout_service = CheckoutService(
    inventory,
    order_service,
    repository
)

try:
    order = checkout_service.checkout(cart)
    print("Заказ оформлен!")
    print("Номер заказа:", order.id)
    print("Итоговая сумма:", order.total)

except ValueError as error:
    print("Ошибка:", error)
