from abc import ABC, abstractmethod
from .point import Point


class Figure(ABC):
    @abstractmethod
    def area(self) -> float:
        pass


class Triangle(Figure):
    def __init__(self, a: Point, b: Point, c: Point):
        self.a = a
        self.b = b
        self.c = c

    def area(self) -> float:
        value = (
            self.a.x * (self.b.y - self.c.y)
            + self.b.x * (self.c.y - self.a.y)
            + self.c.x * (self.a.y - self.b.y)
        )
        return abs(value) / 2
