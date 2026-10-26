import importlib
from calculator import Calculator


operations = []

plugins = ["add", "sub", "mul", "div"]

for name in plugins:
    module = importlib.import_module(name)
    operations.append(module.Operation())

calc = Calculator(operations)

print(calc.calculate("+", 10, 5))
print(calc.calculate("-", 10, 5))
print(calc.calculate("*", 10, 5))

try:
    print(calc.calculate("/", 10, 0))
except ZeroDivisionError as error:
    print("Ошибка:", error)