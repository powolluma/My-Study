import random

# Задача 1
'''
def add(a, b):
    return a + b

def sub(a, b):
    return a - b

def mul(a, b):
    return a * b

def div(a, b):
    if b == 0:
        return None
    return a / b

a = float(input("Первое число: "))
b = float(input("Второе число: "))
op = input("Операция (+ - * /): ")
if op == "+":
    print(add(a, b))
elif op == "-":
    print(sub(a, b))
elif op == "*":
    print(mul(a, b))
elif op == "/":
    r = div(a, b)
    print("Ошибка: деление на ноль" if r is None else r)
else:
    print("Неизвестная операция")
'''

# Задача 2
'''
def swap_value(a, b):
    a, b = b, a
    print("Внутри swap_value:", a, b)

def swap_ref(pair):
    pair[0], pair[1] = pair[1], pair[0]

x, y = 5, 10
swap_value(x, y)
print("После swap_value:", x, y)

p = [5, 10]
swap_ref(p)
print("После swap_ref:", p)
'''

# Задача 3
'''
def power_iter(x, n):
    result = 1
    for _ in range(n):
        result *= x
    return result

def power_rec(x, n):
    if n == 0:
        return 1
    return x * power_rec(x, n - 1)

x = 2
n = 10
r1 = power_iter(x, n)
r2 = power_rec(x, n)
print("Итеративно:", r1)
print("Рекурсивно:", r2)
print("Результаты совпадают:", r1 == r2)
'''

# Задача 4
'''
def counter():
    counter.count += 1
    print("Функция вызвана раз:", counter.count)
counter.count = 0

def make_counter():
    count = 0
    def inner():
        nonlocal count
        count += 1
        print("Замыкание вызвано раз:", count)
    return inner

counter()
counter()
counter()
c = make_counter()
c()
c()
c()
'''

# Задача 5
'''
def fill_random(rows, cols, lo=0, hi=9):
    return [[random.randint(lo, hi) for _ in range(cols)] for _ in range(rows)]

def print_matrix(m):
    for row in m:
        print(*row)
    print()

def transpose(m):
    return [list(row) for row in zip(*m)]

def multiply(A, B):
    if len(A[0]) != len(B):
        raise ValueError("Размеры матриц не согласованы")
    return [[sum(A[i][k] * B[k][j] for k in range(len(B)))
             for j in range(len(B[0]))] for i in range(len(A))]

A = fill_random(2, 3)
B = fill_random(3, 2)
print("A:")
print_matrix(A)
print("B:")
print_matrix(B)
print("A^T:")
print_matrix(transpose(A))
print("A*B:")
print_matrix(multiply(A, B))
'''

# Задача 6
'''
def find_min(arr):
    m = arr[0]
    for v in arr:
        if v < m:
            m = v
    return m

def find_max(arr):
    m = arr[0]
    for v in arr:
        if v > m:
            m = v
    return m

def average(arr):
    return sum(arr) / len(arr)

def count_even(arr):
    return sum(1 for v in arr if v % 2 == 0)

def reverse(arr):
    i, j = 0, len(arr) - 1
    while i < j:
        arr[i], arr[j] = arr[j], arr[i]
        i += 1
        j -= 1

arr = [3, 8, 1, 6, 9, 2]
print("Массив:", arr)
print("Минимум:", find_min(arr))
print("Максимум:", find_max(arr))
print("Среднее:", average(arr))
print("Чётных:", count_even(arr))
reverse(arr)
print("После переворота:", arr)
'''

# Задача 7
'''
def generate_secret():
    return random.randint(1, 100)

def read_guess():
    while True:
        s = input("Ваша догадка (1-100): ")
        try:
            g = int(s)
            if 1 <= g <= 100:
                return g
            print("Число должно быть от 1 до 100.")
        except ValueError:
            print("Введите целое число.")

def check_guess(secret, guess):
    if guess < secret:
        return "больше"
    if guess > secret:
        return "меньше"
    return "угадал"

def play_game():
    secret = generate_secret()
    tries = 0
    while True:
        guess = read_guess()
        tries += 1
        res = check_guess(secret, guess)
        if res == "угадал":
            print("Угадали за", tries, "попыток!")
            break
        print("Загаданное число", res)

play_game()
'''