from abc import ABC, abstractmethod


class IPaymentMethod(ABC):
    @abstractmethod
    def pay(self, amount: float) -> bool:
        pass

    @abstractmethod
    def name(self) -> str:
        pass


class CardPayment(IPaymentMethod):
    def pay(self, amount: float) -> bool:
        if amount <= 0:
            return False

        total = amount * 1.02
        print("Оплата картой:", total)
        return True

    def name(self) -> str:
        return "Банковская карта"


class CashPayment(IPaymentMethod):
    def pay(self, amount: float) -> bool:
        if amount <= 0:
            return False

        print("Оплата наличными:", amount)
        return True

    def name(self) -> str:
        return "Наличные"


class CryptoPayment(IPaymentMethod):
    def pay(self, amount: float) -> bool:
        if amount <= 0:
            return False

        total = amount * 1.01
        print("Оплата криптовалютой:", total)
        return True

    def name(self) -> str:
        return "Криптовалюта"


def checkout(method: IPaymentMethod, amount: float):
    print("Способ оплаты:", method.name())

    if method.pay(amount):
        print("Оплата прошла успешно")
    else:
        print("Оплата не прошла")


try:
    payment = IPaymentMethod()
except TypeError as e:
    print("Ошибка:", e)

card = CardPayment()
cash = CashPayment()
crypto = CryptoPayment()

checkout(card, 1000)
print()

checkout(cash, 500)
print()

checkout(crypto, 2000)
print()

checkout(card, -100)
