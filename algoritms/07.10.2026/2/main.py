def count_vowels(s):
    vowels = "аеёиоуыэюяaeiouy"
    c = 0
    for ch in s.lower():
        if ch in vowels:
            c += 1
    return c

def count_consonants(s):
    c = 0
    for ch in s.lower():
        if ch.isalpha() and ch not in "аеёиоуыэюяaeiouy":
            c += 1
    return c

def longest_word(s):
    best = ""
    for w in s.split():
        if len(w) > len(best):
            best = w
    return best

def is_palindrome(s):
    t = ""
    for ch in s.lower():
        if ch != " ":
            t += ch
    return t == t[::-1]

def main():
    s = input("Введите строку: ")
    print("Гласных:", count_vowels(s))
    print("Согласных:", count_consonants(s))
    print("Самое длинное слово:", longest_word(s))
    print("Палиндром:", "да" if is_palindrome(s) else "нет")

main()