import random

def create_minesweeper(n, m, k):
    # Пустое поле
    field = [['.' for _ in range(m)] for _ in range(n)]
    
    # Расстановка мин в рандомные места
    mines = set()
    while len(mines) < k:
        r = random.randint(0, n - 1)
        c = random.randint(0, m - 1)
        mines.add((r, c))
    
    for r, c in mines:
        field[r][c] = '*'
        
    # Подсчет мин вокруг полей
    for r in range(n):
        for c in range(m):
            if field[r][c] == '*':
                continue
                
            # Проверка 8 направлений
            mine_count = 0
            for dr in [-1, 0, 1]:
                for dc in [-1, 0, 1]:
                    nr, nc = r + dr, c + dc
                    if 0 <= nr < n and 0 <= nc < m and field[nr][nc] == '*':
                        mine_count += 1
                        
            if mine_count > 0:
                field[r][c] = str(mine_count)
                
    # Вывод поля
    for row in field:
        print(" ".join(row))

# Пример запуска для поля 8x8 и 10 мин
create_minesweeper(8, 8, 10)
