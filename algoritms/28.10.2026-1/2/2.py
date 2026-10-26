class WeatherStation:
    def __init__(self):
        self.temperature = 0
        self.observers = []

    def subscribe(self, observer):
        self.observers.append(observer)

    def unsubscribe(self, observer):
        if observer in self.observers:
            self.observers.remove(observer)

    def set_temperature(self, temperature):
        if temperature != self.temperature:
            self.temperature = temperature

            # Используем копию списка
            for observer in self.observers.copy():
                observer.update(temperature)


class Display:
    def update(self, temperature):
        print("Температура:", temperature)


class Logger:
    def __init__(self):
        self.history = []

    def update(self, temperature):
        self.history.append(temperature)
        print("Записано в журнал:", temperature)


class Alarm:
    def __init__(self, limit):
        self.limit = limit

    def update(self, temperature):
        if temperature > self.limit:
            print("Высокая температура!")


class OneTimeObserver:
    def __init__(self, station):
        self.station = station

    def update(self, temperature):
        print("Одноразовый наблюдатель:", temperature)
        self.station.unsubscribe(self)


station = WeatherStation()

display = Display()
logger = Logger()
alarm = Alarm(30)
observer = OneTimeObserver(station)

station.subscribe(display)
station.subscribe(logger)
station.subscribe(alarm)
station.subscribe(observer)

station.set_temperature(20)
station.set_temperature(35)

station.unsubscribe(display)
station.set_temperature(40)

print("История температур:", logger.history)