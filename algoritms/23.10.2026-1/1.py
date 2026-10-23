class BankAccount:
    def __init__(self, balance=0):
        if balance < 0:
            balance = 0

        self.__balance = balance

    def deposit(self, amount):
        if amount > 0:
            self.__balance += amount
        else:
            print("Сумма должна быть положительной")

    def withdraw(self, amount):
        if amount <= 0:
            print("Сумма должна быть положительной")
        elif amount > self.__balance:
            print("Недостаточно средств")
        else:
            self.__balance -= amount

    def getBalance(self):
        return self.__balance


account = BankAccount(1000)

account.deposit(500)
account.withdraw(200)

print("Баланс:", account.getBalance())

account.__balance = 10000

print("После попытки изменения:", account.getBalance())