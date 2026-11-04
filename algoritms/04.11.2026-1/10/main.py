from factory import FigureFactory

factory = FigureFactory()

rectangle = factory.create("rectangle")
circle = factory.create("circle")

print("Площадь прямоугольника:", rectangle.area())
print("Площадь круга:", round(circle.area(), 2))
