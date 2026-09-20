KEYWORDS = ["привет", "тайна", "встреча", "код"]


def extract(text, k):
    # символы с шагом k, индексация с 0
    return text[::k]


def has_keyword(text):
    for word in KEYWORDS:
        if word in text.lower():
            return True
    return False


def main():
    text = input("шифровка: ")
    guess = input("K (или 'auto'): ").strip()

    if guess == "auto":
        for k in range(1, len(text) + 1):
            candidate = extract(text, k)
            if has_keyword(candidate):
                print("подобранное K:", k, "-", candidate)
                return
        print("подобрать не удалось")
    else:
        k = int(guess)
        result = extract(text, k)
        print("расшифровка:", result)
        if has_keyword(result):
            print("текст осмысленный, победа")
        else:
            print("текст не похож на осмысленный")


if __name__ == "__main__":
    main()
