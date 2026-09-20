import math

# 1
"""
x0 = -1.5
xk = 3.5
dx = 0.5
a = -1.25
b = -1.5
c = 0.75

x = x0
while x <= xk + dx / 2:
    if x <= 0:
        print(f"x = {x}, y = ?")
    else:
        y = ((10 ** -2) * b * c) / x + (math.cos(math.sqrt((a ** 3) * x)))
        print(f"x = {x}, y = {y}")
    x += dx
"""

# 2
"""
x0 = 5.3
xk = 10.3
dx = 0.25
a = 1.35
b = -6.25

x = x0
while x <= xk + dx / 2:
    if x <= 0:
        print(f"x = {x} y = ?")
    else:
        y = (a * x**3) + (math.cos(x**3 - b)**2)
        print(f"x = {x}, y = {y}")
    x += dx
"""

# 3
"""
x0 = -0.75
xk = -2.05
dx = -0.2

x = x0
while x >= xk + dx / 2:
    if x <= 0:
        print(f"x = {x} y = ?")
    else:
        y = ((9 * x) ** 4) + (math.sin(57.2 + x))
        print(f"x = {x}, y = {y}")
    x += dx
"""

# 4
"""
x0 = -0.73
xk = -1.73
dx = -0.1
b = -2

x = x0
while x >= xk + dx / 2:
    if x <= 0:
        print(f"x = {x} y = ?")
    else:
        y = (math.log(x - b) ** (1/2)) / (((b ** 3) - (x ** 3)) ** 3/2) + (math.log(x - b))
        print(f"x = {x}, y = {y}")
    x += dx
"""

# 5
"""
x0 = 1.23
xk = -2.4
dx = -0.3
b = 12.6

x = x0
while x >= xk + dx / 2:
    if x <= 0:
        print(f"x = {x} y = ?")
    else:
        y = (15.28 * math.log(x ** (-3 / 2))) + math.cos((math.log(x) + b))
        print(f"x = {x}, y = {y}")
    x += dx
"""
