import math
## Делил действия примеров, мне так удобнее решать
## 1
"""
x = 14.26
y = -1.22
z = 3.5 * (10**-2)
t_1 = 0.564849

a_1 = 2 * (math.cos(x - (math.pi / 6)))
a_2 = 0.5 + (math.sin(y) ** 2)
a_3 = 1 + ((z**2) / (3 - z**2 / 5))
a_4 = (a_1 / a_2) * a_3

print(t_1)
print(a_4)
"""

## 2
"""
x = -4.5
y = 0.75 * (10**-4)
z = 0.845 * (10**2)
u = -55.6848

a_1 = (8 + abs(x - y) ** 2 + 1) ** (1 / 3)
a_2 = x**2 + y**2 + 2
a_3 = math.exp(abs(x - y))
a_4 = (math.tan(z) ** 2 + 1) ** x
a_5 = (a_1 / a_2) - (a_3 * a_4)

print(u)
print(a_5)
"""

## 3
"""
x = 3.74 * (10**-2)
y = -0.825
z = 0.16 * (10**2)
v = 1.0553

a_1 = 1 + (math.sin(x + y) ** 2)
a_2 = abs(x - ((2 * y) / (1 + x**2 * y**2)))
a_3 = (a_1 / a_2) * (x ** abs(y))
a_4 = a_3 + (math.cos(math.atan(1 / z))) ** 2

print(v)
print(a_4)
"""

## 4
"""
x = 0.4 * (10**4)
y = -0.875
z = -0.475 * (10**-3)
w = 1.9873

a_1 = abs(math.cos(x) - math.cos(y)) ** (1 + (math.sin(y) ** 2) * 2)
a_2 = 1 + z + (z**2 / 2) + (z**3 / 3) + (z**4) / 4
a_3 = a_1 * a_2

print(w)
print(a_3)
"""

## 5
"""
x = -15.246
y = 4.642 * (10**-2)
z = 20.001 * (10**2)
a = -182.036

a_1 = math.log(y ** (-math.sqrt(abs(x))))
a_2 = x - (y / 2)
a_3 = math.sin(math.atan(z)) ** 2
a_4 = a_1 * a_2 + a_3

print(a)
print(a_4)
"""

## 6
"""
x = 16.55 * (10**-3)
y = -2.75
z = 0.15
b = -38.902

a_1 = math.sqrt(10 * (x ** (1 / 3) + x ** (y + 2)))
a_2 = (math.asin(z) ** 2) - abs(x - y)
a_3 = a_1 * a_2

print(b)
print(a_3)
"""

## 7
"""
x = 0.1722
y = 6.33
z = 3.25 * (10**-4)
y_1 = -172.025

a_1 = 5 * math.atan(x)
a_2 = 0.25 * math.acos(x)
a_3 = x + 3 * abs(x - y) + x**2
a_4 = abs(x - y) * z + x**2
a_5 = a_1 - a_2 * (a_3 / a_4)

print(y_1)
print(a_5)
"""
## 8
"""
x = -2.235 * (10**-2)
y = 2.23
z = 15.221
f = 39.374

a_1 = math.exp(abs(x - y)) * abs(x - y) ** (x + y)
a_2 = math.atan(x) + math.atan(z)
a_3 = (x**6 + math.log(y) ** 2) ** (1 / 3)
a_4 = a_1 / a_2 + a_3

print(f)
print(a_4)
"""

## 9
"""
x = 1.825 * (10**2)
y = 18.225 
z = -3.298 * (10**-2)
w = 1.2131

a_1 = abs(x**(y/x) - ((y/x) ** (1/3)))
a_2 = y - x
a_3 = math.cos(y) - (z / (y-x))
a_4 = 1 + (y - x)**2
a_5 = a_1 + (a_2 * (a_3 / a_4))

print(w)
print(a_5)
"""

## 10
"""
x = 3.981 * (10**-2)
y = -1.625 * (10**3)
z = 0.512
a = 1.26185

a_1 = 2**(-x)
a_2 = math.sqrt(x + (abs(y) ** (1/4)))
a_3 = math.exp((x - 1 / math.sin(z)) / 3)
a_4 = a_1 * a_2 * a_3

print(a)
print(a_4)
"""

## 11
"""
x = 6.251
y = 0.827
z = 25.001
b = 0.7121

a_1 = y**(abs(x) ** (1/3)) ###############
a_2 = math.cos(y) ** 3
a_3 = abs(x-y) * (1 + ((math.sin(z)**2) / math.sqrt(x + y)))
a_4 = math.exp(abs(x-y)) + x / 2
a_5 = a_1 + (a_2 * (a_3 / a_4))

print(b)
print(a_5)
"""

## 12
"""
x = 3.251
y = 0.325
z = 0.466 * (10**-4)
c = 4.025

a_1 = 2**(y**x)
a_2 = (3**x)**y
a_3 = y * (math.atan(z) - (math.pi / 6))
a_4 = abs(x) + (1 / ((y**2) + 1))
a_5 = (a_1 + a_2) - (a_3 / a_4)

print(c)
print(a_5)
"""

## 13
"""
x = 17.421
y = 10.365 * (10**-3)
z = 0.828 * (10**5)
f = 0.33056

a_1 = (y + (x - 1) ** (1/3)) ** (1/4)
a_2 = abs(x - y) * ((math.sin(z)**2) + math.tan(z))
a_3 = a_1 / a_2

print(f)
print(a_3)
"""

## 14
"""
x = 12.3 * (10**-1)
y = 15.4
z = 0.252 * (10**3)
g = 82.8257

a_1 = y**(x + 1)
a_2 = ((abs(y - 2)** (1/3)) + 3) 
a_3 = x + (y / 2)
a_4 = 2 * abs(x + y)
a_5 = (x + 1)**(-1 / math.sin(z))
a_6 = (a_1 / a_2) + ((a_3 / a_4) * a_5)

print(g)
print(a_6)
"""

## 15
"""
x = 2.444
y = 0.869 * (10**-2)
z = -0.13 * (10**3)
h = -0.49871

a_1 = x**(y + 1) + math.exp((y - 1)) ##
a_2 = 1 + x * abs(y - math.tan(z))
a_3 = 1 + abs(y - x)
a_4 = (abs(y - x)**2) / 2
a_5 = (abs(y - x)**3) / 3
a_6 = (a_1 / a_2) * a_3 + a_4 - a_5

print(h)
print(a_6)
"""

## 16
"""
a = 3.7
b = 0.07
c = 1.5
x = 5.75

a_1 = math.sqrt(c * x)
a_2 = 2.7 * ((abs(c) + abs(x)) / (c**2 * x**2))
a_3 = math.exp(c * x)
a_4 = math.cos(((a + b)**2) / (c * x - b))
y = a_1 - a_2 * a_3 + a_4

print(y)
"""

## 17
"""
a = 7.5
b = 1.2
x = 0.5

a_1 = 4.5 * ((a + b) ** 2 / (a - b) ** 2)
a_2 = math.sqrt((a + b) * (a - b))
a_3 = (10 ** -1) * (math.log(a - b) / math.log(a + b)) * math.exp(x ** 2)
y = a_1 - a_2 + a_3

print(y)
"""

## 18
"""
a = 5.1
b = 0.7
x = -0.05

a_1 = 2.4 * abs((x**2 + b) / a)
a_2 = (a - b) * (math.sin(a - b)**2)
a_3 = (10**-2) * (x - b)
y = a_1 + a_2 + a_3

print(y)
"""

## 19
"""
a = 0.1
b = 2.4
x = -0.3

a_1 = (a * x - math.sqrt(b)) / (5.7 * (x**2 + b**2))
a_2 = ((abs(x + b) - a**2) / (x**2)) * (math.tan(b)**2)
y = a_1 - a_2

print(y)
"""

## 20
"""
a = 4.5
c = 7.4
d = -2.1
x = 0.15

a_1 = math.sqrt((c - (d * x**2)) / x)
a_2 = (math.log(x**2 + c)) / ((0.7 * x) + (a * d))
a_3 = (10**-2) / (c - (d * x**3))
y = a_1 + a_2 - a_3
print(y)
"""