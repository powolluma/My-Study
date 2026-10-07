# Модуль игроков
def is_valid_move(board, move):
    return 0 <= move < 9 and board[move] == " "


def get_move(board, symbol):
    while True:
        raw = input(f"Ход игрока {symbol} (1-9): ").strip()
        if not raw.isdigit():
            print("Введите число от 1 до 9.")
            continue
        move = int(raw) - 1
        if is_valid_move(board, move):
            return move
        print("Некорректный ход: клетка занята или вне поля.")