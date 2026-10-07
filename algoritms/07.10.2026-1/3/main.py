def reverse(s):
    if len(s) <= 1:
        return s
    return reverse(s[1:]) + s[0]

def is_palindrome(s):
    if len(s) <= 1:
        return True
    if s[0] != s[-1]:
        return False
    return is_palindrome(s[1:-1])

s = input("Введите строку: ")
print("Разворот:", reverse(s))
print("Палиндром:", "да" if is_palindrome(s) else "нет")