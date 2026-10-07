from mystring import replace_substring, to_upper, to_lower

text = input("Введите строку: ")
old = input("Что заменить: ")
new = input("На что заменить: ")
result = replace_substring(text, old, new)
print("После замены:", result)
print("ВЕРХНИЙ регистр:", to_upper(result))
print("нижний регистр:", to_lower(result))