
from decimal import Decimal, InvalidOperation
from typing import Optional


# Основное исключение банковского приложения
class BankError(Exception):
    pass


# Ошибка неправильной суммы
class InvalidAmountError(BankError):
    pass


# Ошибка недостатка средств
class InsufficientFundsError(BankError):
    pass


# Ошибка перевода
class InvalidTransferError(BankError):
    pass


class BankAccount:

    def __init__(self, owner: str, initial_balance=Decimal("0")):
        self.owner = owner

        balance = self._to_decimal(initial_balance)

        if balance < 0:
            raise InvalidAmountError(
                "Начальный баланс не может быть отрицательным"
            )

        self._balance = balance

    @property
    def balance(self):
        return self._balance

    # Преобразование значения в Decimal
    def _to_decimal(self, value):
        try:
            amount = Decimal(str(value))
        except (InvalidOperation, ValueError, TypeError):
            raise InvalidAmountError("Некорректная сумма")

        if not amount.is_finite():
            raise InvalidAmountError("Сумма должна быть конечным числом")

        return amount

    # Проверка положительной суммы
    def _check_amount(self, amount):
        amount = self._to_decimal(amount)

        if amount <= 0:
            raise InvalidAmountError(
                "Сумма должна быть больше нуля"
            )

        return amount

    # Пополнение счёта
    def deposit(self, amount):
        amount = self._check_amount(amount)

        self._balance += amount

    # Снятие денег
    def withdraw(self, amount):
        amount = self._check_amount(amount)

        if amount > self._balance:
            raise InsufficientFundsError(
                "Недостаточно денег на счёте"
            )

        self._balance -= amount

    # Расчёт процентов
    # rate задаётся в долях: 0.05 = 5%
    def calculate_interest(self, rate):
        rate = self._to_decimal(rate)

        if rate < 0:
            raise InvalidAmountError(
                "Процентная ставка не может быть отрицательной"
            )

        return self._balance * rate

    # Перевод денег на другой счёт
    def transfer(self, target: Optional["BankAccount"], amount):

        # Проверка получателя
        if target is None:
            raise InvalidTransferError(
                "Получатель не указан"
            )

        if not isinstance(target, BankAccount):
            raise InvalidTransferError(
                "Получатель должен быть банковским счётом"
            )

        # Запрет перевода самому себе
        if target is self:
            raise InvalidTransferError(
                "Нельзя переводить деньги самому себе"
            )

        amount = self._check_amount(amount)

        # Проверка наличия средств
        if amount > self._balance:
            raise InsufficientFundsError(
                "Недостаточно денег для перевода"
            )

        # Выполнение перевода
        self._balance -= amount
        target._balance += amount
