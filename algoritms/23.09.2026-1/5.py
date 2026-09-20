WORDS = ["python", "код", "ключ", "буква"]


def show_positions(letter, words):
    # позиции буквы в каждом слове
    found_any = False
    for word in words:
        positions = []
        for i, ch in enumerate(word):
            if ch == letter:
                positions.append(i)
        if positions:
            found_any = True
            print(f"слово из {len(word)} букв, позиции: {positions}")
    return found_any


def word_guessed(word, guessed_letters):
    for ch in word:
        if ch not in guessed_letters:
            return False
    return True


def main():
    words = WORDS[:]
    guessed_letters = set()
    moves = 0

    print("поле спрятано,", len(words), "слов")

    while True:
        solved = [w for w in words if word_guessed(w, guessed_letters)]
        if len(solved) == len(words):
            print("все слова угаданы, ходов:", moves)
            break

        letter = input("буква: ").strip().lower()
        moves += 1
        guessed_letters.add(letter)

        hit = show_positions(letter, words)
        if not hit:
            print("мимо")


if __name__ == "__main__":
    main()
