# Модуль игры
import board as bd
import players


def switch_player(symbol):
    return "O" if symbol == "X" else "X"


def play_game():
    board = bd.create_board()
    current = "X"
    while True:
        bd.show_board(board)
        move = players.get_move(board, current)
        board[move] = current
        winner = bd.check_winner(board)
        if winner:
            bd.show_board(board)
            print(f"Победил игрок {winner}!")
            return
        if bd.is_full(board):
            bd.show_board(board)
            print("Ничья!")
            return
        current = switch_player(current)