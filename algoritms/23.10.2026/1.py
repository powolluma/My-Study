class Bicycle:
    def __init__(self, speed, gear):
        self.speed = speed
        self.gear = gear

    def speed_up(self, amount):
        self.speed += amount


if __name__ == "__main__":
    bicycle = Bicycle(10, 3)
    bicycle.speed_up(5)
    print(bicycle.speed)
