# 1
"""
n = int(input("Введите число: "))
count = 1
a = 0
while count <= n:
    a += count
    count += 1
print(f"Сумма всех чисел от 1 до {n} = {a}")
"""

# 2 
"""
n = int(input("Введите число: "))
fact = 1
for i in range(1, n + 1):
    fact *= i
print(f"Факториал числа {n} = {fact}")
"""

# 3
"""
n = int(input("Введите кол-во звездочек: "))

# Квадрат
for i in range(n):
        print("* " * n)
print()
print()
# Треугольник прямоугольный
for i in range(1, n + 1):
      print("* " * i)

print()
print()

# Перевенутый 
for n in range(n, 0, - 1):
       print("* " * n )

print()
print()

# Ромб
for i in range(1, n + 1):
    print(" " * (n - i) + "*" * (2 * i - 1))

for i in range(n, 0, -1):
    print(" " * (n - i) + "*" * (2 * i - 1))
"""

# 4
"""
count = int(input("Введите кол-во чисел: "))
number = 0
for i in range(1, count + 1):
    n = int(input("Введите число: "))
    number += n

print(f"Среднее {count} чисел = {number / count}")
"""

# 5
"""
n = int(input("Ввдите целое число: "))
count = 0
while n > 0:   
    count += 1
    n //= 10
print(f"Кол-во цифр = {count}")
"""

# 6
"""
n = int(input("Сколько чисел вывести: "))
a = 0
b = 1

for i in range(n):
    print(a)
    a, b = b, a + b
"""