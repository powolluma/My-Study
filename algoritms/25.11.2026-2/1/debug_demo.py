
class DefectiveBankAccount:
    def __init__(self, balance):
        self.balance = balance

    def withdraw(self, amount):
        print("Баланс до снятия:", self.balance)
        print("Сумма снятия:", amount)

        # Точка останова
        breakpoint()

        # Ошибка: нет проверки достаточности средств
        self.balance -= amount

        print("Баланс после снятия:", self.balance)


account = DefectiveBankAccount(100)

# Пытаемся снять больше денег, чем есть
account.withdraw(150)
