def divide(a, b):
    if b == 0:
        raise ValueError("Деление на ноль!")

    return a / b


try:
    print(divide(10, 2))
except ValueError as e:
    print("Ошибка:", e)
