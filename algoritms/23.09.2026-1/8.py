import time

TEXT = ("город золото гора горизонт река рыба рынок дом дорога "
        "лес лето лодка море можно небо озеро")


def find_words(text, prefixes):
    words = text.split()
    result = []
    for word in words:
        for prefix in prefixes:
            if word.startswith(prefix):
                result.append(word)
                break
    return result


def main():
    prefixes_input = input("префиксы через запятую: ")
    prefixes = [p.strip() for p in prefixes_input.split(",")]

    treasures = find_words(TEXT, prefixes)
    print("всего сокровищ:", len(treasures))

    score = 0
    found = set()
    start = time.time()
    time_limit = 30

    while time.time() - start < time_limit:
        remaining = time_limit - int(time.time() - start)
        word = input(f"слово (осталось {remaining} сек, 'stop' - выход): ").strip()
        if word == "stop":
            break

        if word in found:
            print("уже найдено")
            continue

        if word in treasures:
            found.add(word)
            score += 1
            print("найдено, очки:", score)
        else:
            print("не сокровище")

    print("итог: очки", score, "из", len(treasures))


if __name__ == "__main__":
    main()
