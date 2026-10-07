def find_path(maze, visited, path, x, y):
    n = len(maze)
    m = len(maze[0])
    if x < 0 or y < 0 or x >= n or y >= m:
        return False
    if maze[x][y] == 1 or visited[x][y]:
        return False
    visited[x][y] = True
    path.append((x, y))
    if x == n - 1 and y == m - 1:
        return True
    if find_path(maze, visited, path, x + 1, y):
        return True
    if find_path(maze, visited, path, x - 1, y):
        return True
    if find_path(maze, visited, path, x, y + 1):
        return True
    if find_path(maze, visited, path, x, y - 1):
        return True
    path.pop()
    return False

n, m = map(int, input("Размеры (строки столбцы): ").split())
maze = []
for i in range(n):
    maze.append(list(map(int, input().split())))

visited = [[False] * m for _ in range(n)]
path = []
if find_path(maze, visited, path, 0, 0):
    for i in range(n):
        row = ""
        for j in range(m):
            if (i, j) in path:
                row += "* "
            else:
                row += str(maze[i][j]) + " "
        print(row)
else:
    print("Пути нет")