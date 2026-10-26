import random


class Pet:
    def __init__(self, name):
        self.__name = name
        self.__hunger = 50
        self.__happiness = 50

    def _set_hunger(self, value):
        self.__hunger = max(0, min(100, value))

    def _set_happiness(self, value):
        self.__happiness = max(0, min(100, value))

    @property
    def name(self):
        return self.__name

    @property
    def hunger(self):
        return self.__hunger

    @property
    def happiness(self):
        return self.__happiness

    @property
    def mood(self):
        if self.__happiness >= 70:
            return "счастлив"
        elif self.__happiness >= 40:
            return "нормально"
        else:
            return "грустен"

    def feed(self):
        self._set_hunger(self.__hunger - 20)
        self._set_happiness(self.__happiness + 5)

    def play(self):
        self._set_happiness(self.__happiness + 15)
        self._set_hunger(self.__hunger + 10)

    def tick(self):
        self._set_hunger(self.__hunger + 5)
        self._set_happiness(self.__happiness - 3)

    def show(self):
        print(
            f"Имя: {self.name}, "
            f"голод: {self.hunger}, "
            f"счастье: {self.happiness}, "
            f"настроение: {self.mood}"
        )


pet = Pet("Барсик")

actions = ["кормить", "играть", "ждать"]

for i in range(10):
    action = random.choice(actions)

    print(f"\nШаг {i + 1}. Действие: {action}")

    if action == "кормить":
        pet.feed()
    elif action == "играть":
        pet.play()
    else:
        pet.tick()

    pet.show()