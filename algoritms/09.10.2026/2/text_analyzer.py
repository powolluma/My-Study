import mystring

text = input("Введите текст: ")
print("Количество слов:", mystring.count_words(text))
sub = input("Какую подстроку искать? ")
pos = mystring.find_substring(text, sub)
print("Позиции вхождений:", pos if pos else "не найдено")
print("Текст наоборот:", mystring.reverse(text))