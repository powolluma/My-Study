class Thermometer:
    def __init__(self, celsius):
        self.celsius = celsius

    @property
    def celsius(self):
        return self.__celsius

    @celsius.setter
    def celsius(self, value):
        if value < -273.15:
            raise ValueError("Температура ниже нуля")
        self.__celsius = value

    @property
    def fahrenheit(self):
        return self.__celsius * 9 / 5 + 32


t = Thermometer(25)

print("Цельсий: ", t.celsius)
print("Фаренгейт: ", t.fahrenheit)

t.celsius = 30
print("Новая температура: ", t.celsius)