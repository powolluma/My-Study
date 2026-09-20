import random
from collections import Counter

LETTERS_POOL = "абвгдежзиклмнопрстуфхцчш"
MAX_GLASS = 15  # переполнение стакана
BONUS = "*"  # бонусная буква
BOMB = "!"  # бомба, очищает стакан


def new_letters(n):
    result = []
    for _ in range(n):
        r = random.random()
        if r < 0.1:
            result.append(BONUS)
        elif r < 0.15:
            result.append(BOMB)
        else:
            result.append(random.choice(LETTERS_POOL))
    return result


def can_build(word, glass_count):
    word_count = Counter(word)
    for ch, cnt in word_count.items():
        if glass_count[ch] < cnt:
            return False
    return True


def remove_word(word, glass):
    for ch in word:
        glass.remove(ch)


def main():
    glass = []
    print("текстовый тетрис, 'stop' - остановка")

    while True:
        glass.extend(new_letters(3))
        print("стакан:", "".join(glass), "(", len(glass), "/", MAX_GLASS, ")")

        if len(glass) >= MAX_GLASS:
            print("стакан переполнен, проигрыш")
            break

        word = input("слово: ").strip().lower()
        if word == "stop":
            break

        if BOMB in word:
            glass = []
            print("бомба, стакан очищен")
            continue

        glass_count = Counter(glass)
        if can_build(word, glass_count):
            remove_word(word, glass)
            print("слово составлено, стакан:", "".join(glass))
        else:
            print("не хватает букв")


if __name__ == "__main__":
    main()
