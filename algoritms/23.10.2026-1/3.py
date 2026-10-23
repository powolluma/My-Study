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


circle = Circle(5)
rectangle = Rectangle(4, 6)

circle.describe()
rectangle.describe()