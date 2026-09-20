import random
import time

MESSAGES = [
    "attack at dawn near the bridge",
    "secret meeting in the old tower",
    "package hidden under the bench",
]
KEYWORDS = ["attack", "secret", "package", "meeting", "bridge", "tower"]

ALPHA = "abcdefghijklmnopqrstuvwxyz"


def shift_char(ch, k):
    idx = ALPHA.find(ch.lower())
    if idx == -1:
        return ch
    new_ch = ALPHA[(idx + k) % 26]
    return new_ch.upper() if ch.isupper() else new_ch


def shift_text(text, k):
    return "".join(shift_char(c, k) for c in text)


def has_keyword(text):
    for word in KEYWORDS:
        if word in text.lower():
            return True
    return False


def brute_force(encrypted):
    # перебор всех сдвигов
    for k in range(1, 26):
        candidate = shift_text(encrypted, -k)
        if has_keyword(candidate):
            return k, candidate
    return None, None


def main():
    message = random.choice(MESSAGES)
    k = random.randint(1, 25)
    encrypted = shift_text(message, k)

    print("перехваченное сообщение:", encrypted)

    attempts = 0
    start = time.time()

    while attempts < 5 and time.time() - start < 60:
        guess = input("сдвиг: ").strip()
        if guess == "auto":
            found_k, decoded = brute_force(encrypted)
            print("подобранный сдвиг:", found_k, "-", decoded)
            break

        guess_k = int(guess)
        attempts += 1
        decoded = shift_text(encrypted, -guess_k)
        print("результат:", decoded)

        if has_keyword(decoded):
            print("победа, сообщение расшифровано")
            break
    else:
        print("попытки закончились")


if __name__ == "__main__":
    main()
