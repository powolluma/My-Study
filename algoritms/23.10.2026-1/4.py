class Shape:
    def area(self):
        return 0.0

    def describe(self):
        print(
            self.__class__.__name__,
            "площадь:",
            self.area()
        )


class Circle(Shape):
    def __init__(self, radius):
        self.radius = radius

    def area(self):
        return 3.14 * self.radius ** 2


class Rectangle(Shape):
    def __init__(self, width, height):
        self.width = width
        self.height = height

    def area(self):
        return self.width * self.height


class Triangle(Shape):
    def __init__(self, width, height):
        self.width = width
        self.height = height

    def area(self):
        return self.width * self.height / 2


def total_area(shapes):
    total = 0

    for shape in shapes:
        total += shape.area()

    return total


shapes = [
    Circle(5),
    Rectangle(4, 6),
    Triangle(4, 3)
]

for shape in shapes:
    shape.describe()

print("Общая площадь:", total_area(shapes))