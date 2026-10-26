import math


class Point:
    def __init__(self, x, y):
        self.x = x
        self.y = y

    def move(self, dx, dy):
        self.x += dx
        self.y += dy

    def distanceTo(self, other):
        return math.sqrt(
            (other.x - self.x) ** 2 +
            (other.y - self.y) ** 2
        )

    def show(self):
        print("Координаты: ", self.x, self.y)


p1 = Point(1, 2)
p2 = Point(4, 6)

p1.move(1, 1)

p1.show()
p2.show()

print("Расстояние: ", p1.distanceTo(p2))