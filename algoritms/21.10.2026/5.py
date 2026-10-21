def swap(stack):
    if len(stack) < 2:
        print("Ошибка")
        return

    stack[-1], stack[-2] = stack[-2], stack[-1]


s = [1, 2, 3]
swap(s)
print(s)