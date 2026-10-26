class PiggyBank:
    def __init__(self):
        self.money = 0

    def put(self, amount):
        self.money += amount

    def take(self, amount):
        if amount <= self.money:
            self.money -= amount
        else:
            print("Недостаточно денег")

    def show(self):
        print("В копилке:", self.money)


bank = PiggyBank()
bank.put(100)
bank.put(50)
bank.take(200)
bank.show()