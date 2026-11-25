
import pytest
from decimal import Decimal

from bank_account import (
    BankAccount,
    InvalidAmountError,
    InsufficientFundsError,
    InvalidTransferError
)


# 1. Проверка правильного пополнения
def test_deposit():
    account = BankAccount("Ivan", Decimal("100"))

    account.deposit(Decimal("50"))

    assert account.balance == Decimal("150")


# 2. Проверка отрицательного пополнения
def test_deposit_negative():
    account = BankAccount("Ivan", Decimal("100"))

    with pytest.raises(InvalidAmountError):
        account.deposit(Decimal("-50"))


# 3. Проверка пополнения нулём
def test_deposit_zero():
    account = BankAccount("Ivan", Decimal("100"))

    with pytest.raises(InvalidAmountError):
        account.deposit(Decimal("0"))


# 4. Проверка правильного снятия
def test_withdraw():
    account = BankAccount("Ivan", Decimal("100"))

    account.withdraw(Decimal("30"))

    assert account.balance == Decimal("70")


# 5. Проверка снятия больше баланса
def test_withdraw_more_than_balance():
    account = BankAccount("Ivan", Decimal("100"))

    with pytest.raises(InsufficientFundsError):
        account.withdraw(Decimal("150"))

    assert account.balance == Decimal("100")


# 6. Проверка перевода между счетами
def test_transfer():
    account1 = BankAccount("Ivan", Decimal("100"))
    account2 = BankAccount("Petr", Decimal("50"))

    account1.transfer(account2, Decimal("30"))

    assert account1.balance == Decimal("70")
    assert account2.balance == Decimal("80")


# 7. Проверка перевода самому себе
def test_transfer_to_self():
    account = BankAccount("Ivan", Decimal("100"))

    with pytest.raises(InvalidTransferError):
        account.transfer(account, Decimal("30"))

    assert account.balance == Decimal("100")


# 8. Проверка перевода на пустой счёт
def test_transfer_to_none():
    account = BankAccount("Ivan", Decimal("100"))

    with pytest.raises(InvalidTransferError):
        account.transfer(None, Decimal("30"))

    assert account.balance == Decimal("100")


# 9. Проверка расчёта процентов
def test_calculate_interest():
    account = BankAccount("Ivan", Decimal("1000"))

    result = account.calculate_interest(Decimal("0.05"))

    assert result == Decimal("50")


# 10. Проверка отрицательного снятия
def test_withdraw_negative():
    account = BankAccount("Ivan", Decimal("100"))

    with pytest.raises(InvalidAmountError):
        account.withdraw(Decimal("-20"))

    assert account.balance == Decimal("100")
