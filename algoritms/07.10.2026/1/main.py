def sum_positive(a):
    s = 0
    for x in a:
        if x > 0:
            s += x
    return s

def count_negative(a):
    c = 0
    for x in a:
        if x < 0:
            c += 1
    return c

def max_element(a):
    m = a[0]
    for x in a:
        if x > m:
            m = x
    return m

def average(a):
    s = 0
    for x in a:
        s += x
    return s / len(a)

def main():
    a = list(map(int, input("Введите числа через пробел: ").split()))
    print("Сумма положительных:", sum_positive(a))
    print("Количество отрицательных:", count_negative(a))
    print("Максимум:", max_element(a))
    print("Среднее:", average(a))

main()