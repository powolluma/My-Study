class BankAccount:
    def __init__(self, owner, balance=0):
        self.owner = owner
        self.balance = balance

    def deposit(self, amount):
        self.balance += amount

    def withdraw(self, amount):
        if 0 <= amount <= self.balance:
            self.balance -= amount
            return True
        return False

    def __str__(self):
        return f"Owner: {self.owner}, balance: {self.balance}"


if __name__ == "__main__":
    account = BankAccount("Иван", 100)
    account.deposit(50)
    account.withdraw(30)
    account.withdraw(1000)
    print(account)
