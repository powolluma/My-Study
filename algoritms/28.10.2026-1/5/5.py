class EventBus:
    def __init__(self):
        self.subscribers = {}

    def subscribe(self, event, handler):
        if event not in self.subscribers:
            self.subscribers[event] = []

        self.subscribers[event].append(handler)

    def publish(self, event, value):
        handlers = self.subscribers.get(event, []).copy()

        for handler in handlers:
            handler(value)


class TemperatureSensor:
    def __init__(self, bus):
        self.bus = bus

    def set_temperature(self, value):
        print("\nТемпература изменилась:", value)
        self.bus.publish("temperature_changed", value)


class Heater:
    def __init__(self, bus):
        self.on = True
        bus.subscribe("temperature_changed", self.update)

    def update(self, temperature):
        if temperature >= 22 and self.on:
            self.on = False
            print("Нагреватель выключен")

        elif temperature <= 18 and not self.on:
            self.on = True
            print("Нагреватель включен")


class Logger:
    def __init__(self, bus):
        self.history = []
        bus.subscribe("temperature_changed", self.update)

    def update(self, temperature):
        self.history.append(temperature)
        print("Запись в журнал:", temperature)


class Alarm:
    def __init__(self, bus, limit):
        self.limit = limit
        bus.subscribe("temperature_changed", self.update)

    def update(self, temperature):
        if temperature > self.limit:
            print("Перегрев")


bus = EventBus()

sensor = TemperatureSensor(bus)
heater = Heater(bus)
logger = Logger(bus)
alarm = Alarm(bus, 30)

sensor.set_temperature(15)
sensor.set_temperature(22)
sensor.set_temperature(35)

print("\nИстория:", logger.history)