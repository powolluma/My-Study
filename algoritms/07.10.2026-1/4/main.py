def binary_search(a, x, left, right):
    if left > right:
        return -1
    mid = (left + right) // 2
    if a[mid] == x:
        return mid
    if a[mid] < x:
        return binary_search(a, x, mid + 1, right)
    return binary_search(a, x, left, mid - 1)

a = list(map(int, input("Отсортированный массив: ").split()))
x = int(input("Искомое значение: "))
print(binary_search(a, x, 0, len(a) - 1))