import random

def play_snake(n=10, m=10):
    snake = [[5, 5], [5, 4], [5, 3]] 
    direction = 'd'
    
    # Генерация яблока
    def spawn_apple():
        while True:
            a = [random.randint(0, n-1), random.randint(0, m-1)]
            if a not in snake: return a
            
    apple = spawn_apple()
    opposites = {'w': 's', 's': 'w', 'a': 'd', 'd': 'a'}

    while True:
        # Рисуем поле
        for r in range(n):
            row_str = ""
            for c in range(m):
                if [r, c] == snake[0]: row_str += "O "      # Голова
                elif [r, c] in snake: row_str += "o "       # Тело
                elif [r, c] == apple: row_str += "@ "       # Яблоко
                else: row_str += ". "
            print(row_str)
            
        cmd = input("Ход (w/a/s/d): ").strip().lower()
        if cmd in ['w', 'a', 's', 'd'] and cmd != opposites[direction]:
            direction = cmd
            
        # Подчсчет новых координат головы
        dr, dc = {'w':(-1,0), 's':(1,0), 'a':(0,-1), 'd':(0,1)}[direction]
        new_head = [snake[0][0] + dr, snake[0][1] + dc]
        
        # Проверка столкновения со стеной или собой
        if not (0 <= new_head[0] < n and 0 <= new_head[1] < m) or new_head in snake:
            print("Игра окончена! Вы проиграли."); break
            
        snake.insert(0, new_head) # Новая голова
        
        # Проверка съедания яблока
        if new_head == apple:
            apple = spawn_apple()
        else:
            snake.pop() # Удаление хвоста, если яблоко не съедено

play_snake()
