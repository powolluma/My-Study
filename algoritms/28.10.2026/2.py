class TrafficLight:
    def __init__(self):
        self.color = "red"

    def next(self):
        if self.color == "red":
            self.color = "green"
        elif self.color == "green":
            self.color = "yellow"
        else:
            self.color = "red"

    def show(self):
        print(self.color)


light = TrafficLight()

for i in range(5):
    light.next()
    light.show()