import random

def play_tanks():
    # Карта 
    field = [
        ['#', '#', '#', '#', '#'],
        ['#', 'P', '.', 'E', '#'],
        ['#', '.', '#', '.', '#'],
        ['#', '.', '.', '.', '#'],
        ['#', '#', '#', '#', '#']
    ]
    px, py = 1, 1   # игрок
    ex, ey = 1, 3   # враг
    p_dir = (-1, 0) # направление вверх
    
    while True:
        # Создание карты
        for row in field: print(" ".join(row))
        print("w/a/s/d - движение, f - выстрел.")
        
        cmd = input("Действие: ").strip().lower()
        
        # Движение и поворот игрока
        if cmd in ['w', 'a', 's', 'd']:
            dr, dc = {'w':(-1,0), 's':(1,0), 'a':(0,-1), 'd':(0,1)}[cmd]
            p_dir = (dr, dc)
            if field[px + dr][py + dc] == '.':
                field[px][py] = '.'
                px, py = px + dr, py + dc
                field[px][py] = 'P'
                
        # Выстрел 
        elif cmd == 'f':
            bx, by = px + p_dir[0], py + p_dir[1]
            while field[bx][by] == '.':
                bx += p_dir[0]
                by += p_dir[1]
            if field[bx][by] == 'E':
                print("Вы уничтожили врага! ПОБЕДА!")
                break
            else:
                print("Промах (попали в стену).")
                
        # Cлучайное движение врага
        if field[ex][ey] == 'E': # Если враг еще жив
            edr, edc = random.choice([(-1,0), (1,0), (0,-1), (0,1)])
            if field[ex + edr][ey + edc] == '.':
                field[ex][ey] = '.'
                ex, ey = ex + edr, ey + edc
                field[ex][ey] = 'E'
            elif field[ex + edr][ey + edc] == 'P':
                print("Враг поймал вас! ПОРАЖЕНИЕ!"); break

play_tanks()
