import math

# 1
"""
x = float(input("Введите x: "))
y = float(input("Введите y: "))
f = float(input("Введите f: "))
n = 0
if ((x * y) > 0):
    n = ((f * (x) + y) ** 2) - math.sqrt(f * (x) * y)
elif ((x * y) < 0):
    n = ((f * (x) + y) ** 2) + (math.sqrt(math.log(f * (x) * y)))
elif (x * y == 0):
    n = ((f * (x) + y) ** 2) + 1

if n is None:
    print(f"Нет ответа")
elif n is not None:
    print(f"Ответ = {n}")
"""

# 2
"""
x = float(input("Введите x: "))
y = float(input("Введите y: "))
f = float(input("Введите f: "))
n = 0
if (x / y) > 0:
    n = math.log(f * (x)) + ((f * (x **2) + y) ** 3)
elif (x / y) < 0:
    n = math.log(abs(f * (x) / y)) + ((f * (x) + y) ** 3)
elif x == 0:
    n = (f * (x ** 2) + y) ** 3
elif y == 0:
    n = 0
if n is None:
    print(f"Нет ответа")
elif n is not None:
    print(f"Ответ = {n}")
"""

# 3
"""
x = float(input("Введите x: "))
a = float(input("Введите y: "))
f = float(input("Введите f: "))
n = 0

if 1 < abs(x) < 3:
    n = (((a * (x ** 2)) + 2) / ((x ** 2) + 1)) * (f * x)
elif abs(x) >= 3:
    n = (a ** 2) + (f * (x))
elif abs(x) <= 1:
    n = (a * x) * ((f * x) / (x + 2))
if n is None:
    print(f"Нет ответа")
elif n is not None:
    print(f"Ответ = {n}")
"""

# 4
"""
x = float(input("Введите x: "))
b = float(input("Введите b: "))
f = float(input("Введите f: "))
n = 0

if 1 < (x * b) < 3:
    n = math.exp(f * x)
elif 12 < (x * b) < 40:
    n = math.sqrt(abs((f * x) + (4 * b)))
else:
    n = (b * f * (x ** 2))
if n is None:
    print(f"Нет ответа")
elif n is not None:
    print(f"Ответ = {n}")
"""

# 5
x = float(input("Введите x: "))
y = float(input("Введите y: "))
z = float(input("Введите z: "))
f = float(input("Введите f: "))
m = 0

m = ((max(f*(x), y, z)) / (min(f*x, y))) + 5

if m is None:
    print(f"Нет ответа")
elif m is not None:
    print(f"Ответ = {m}")


