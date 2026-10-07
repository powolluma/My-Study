# Модуль игрового поля
def create_board():
    return [" "] * 9


def show_board(board):
    print()
    for i in range(0, 9, 3):
        row = [board[j] if board[j] != " " else str(j + 1) for j in range(i, i + 3)]
        print(" " + " | ".join(row))
        if i < 6:
            print("---+---+---")
    print()


WIN_LINES = [(0, 1, 2), (3, 4, 5), (6, 7, 8),
             (0, 3, 6), (1, 4, 7), (2, 5, 8),
             (0, 4, 8), (2, 4, 6)]


def check_winner(board):
    for a, b, c in WIN_LINES:
        if board[a] != " " and board[a] == board[b] == board[c]:
            return board[a]
    return None


def is_full(board):
    return " " not in board