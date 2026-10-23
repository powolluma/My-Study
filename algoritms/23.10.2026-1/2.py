class Animal:
    def __init__(self, name):
        self.name = name

    def speak(self):
        print("Какой то звук")


class Dog(Animal):
    def __init__(self, name):
        super().__init__(name)

    def speak(self):
        print("Гав")


class Cat(Animal):
    def __init__(self, name):
        super().__init__(name)

    def speak(self):
        print("Мяу")


dog = Dog("Тузик")
cat = Cat("Муся")

print(dog.name)
dog.speak()

print(cat.name)
cat.speak()