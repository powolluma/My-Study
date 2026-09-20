def print_board(board):
    for row in board:
        print(" ".join(row))
    print()

def check_win(board, n, player):
    # Проверка на 5 в ряд
    for r in range(n):
        for c in range(n):
            if board[r][c] != player:
                continue
            # 4 направления
            directions = [(0,1), (1,0), (1,1), (1,-1)]
            for dr, dc in directions:
                count = 0
                for i in range(5):
                    nr, nc = r + dr * i, c + dc * i
                    if 0 <= nr < n and 0 <= nc < n and board[nr][nc] == player:
                        count += 1
                    else:
                        break
                if count == 5:
                    return True
    return False

# Игровой цикл
def tic_tac_toe(n=5):
    # Поле
    board = [['.' for _ in range(n)] for _ in range(n)]
    current_player = 'X'
    moves = 0
    
    while True:
        print_board(board)
        print(f"Ход игрока {current_player}")
        try:
            r, c = map(int, input("Введите строку и столбец через пробел: ").split())
        except ValueError:
            print("Неверный ввод!"); continue
            
        if not (0 <= r < n and 0 <= c < n) or board[r][c] != '.':
            print("Клетка занята или вне поля"); continue
            
        board[r][c] = current_player
        moves += 1
        
        if check_win(board, n, current_player):
            print_board(board)
            print(f"Игрок {current_player} победил!")
            break
            
        if moves == n * n:
            print_board(board)
            print("Ничья!")
            break
            
        current_player = 'O' if current_player == 'X' else 'X'

tic_tac_toe(5)
