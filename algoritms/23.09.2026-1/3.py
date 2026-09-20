import random
import time
from collections import Counter

DICTIONARY = ["кот", "дом", "мир", "стол", "рука", "нога", "окно",
              "слон", "тигр", "лист", "город", "вода", "звук", "утро"]


def letter_counts(word):
    return Counter(word)


def can_build(word, available):
    word_count = letter_counts(word)
    for ch, cnt in word_count.items():
        if available[ch] < cnt:
            return False
    return True


def main():
    base_word = random.choice(DICTIONARY)
    letters = list(base_word)
    random.shuffle(letters)
    available = letter_counts(base_word)

    print("буквы:", " ".join(letters))
    print("составляйте слова, 'stop' для остановки, время 60 сек")

    found = set()
    start = time.time()

    while True:
        if time.time() - start > 60:
            print("время вышло")
            break

        word = input("слово: ").strip().lower()
        if word == "stop":
            break

        if word in found:
            print("уже было")
            continue

        if not can_build(word, available):
            print("буквы не подходят")
            continue

        if word not in DICTIONARY:
            print("нет в словаре")
            continue

        found.add(word)
        print("принято, счёт:", len(found))

    print("найденные слова:", found)


if __name__ == "__main__":
    main()
