from shapes import Point, Triangle


a = Point(0, 0)
b = Point(4, 0)
c = Point(0, 3)

triangle = Triangle(a, b, c)
print("Площадь треугольника: ", triangle.area())
print("Расстояние между точками: ", a.distance_to(b))
