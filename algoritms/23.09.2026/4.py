from collections import deque

def solve_maze(maze):
    n, m = len(maze), len(maze[0])
    start, end = None, None
    
    # поиск старта и выхода
    for r in range(n):
        for c in range(m):
            if maze[r][c] == 'S': start = (r, c)
            elif maze[r][c] == 'E': end = (r, c)

    queue = deque([[start]])
    visited = {start}
    
    while queue:
        path = queue.popleft()
        r, c = path[-1]
        
        if (r, c) == end:
            print(f"Кратчайший путь: {len(path) - 1} шагов.")
            # путь точками 'x'
            for pr, pc in path[1:-1]:
                maze[pr][pc] = 'x'
            for row in maze: print("".join(row))
            return
            
        # Проверка  направлений движения
        for dr, dc in [(-1,0), (1,0), (0,-1), (0,1)]:
            nr, nc = r + dr, c + dc
            if 0 <= nr < n and 0 <= nc < m and maze[nr][nc] != '#' and (nr, nc) not in visited:
                visited.add((nr, nc))
                queue.append(path + [(nr, nc)])
                
    print("Выход не найден!")

# Лабиринт
maze_map = [
    ['#', '#', '#', '#', '#', '#'],
    ['#', 'S', '.', '.', '#', '#'],
    ['#', '#', '#', '.', '.', '#'],
    ['#', '.', '.', '.', '.', 'E'],
    ['#', '#', '#', '#', '#', '#']
]
solve_maze(maze_map)
