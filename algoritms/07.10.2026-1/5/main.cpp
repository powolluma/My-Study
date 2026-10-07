#include <iostream>
#include <vector>
using namespace std;

bool findPath(vector<vector<int>>& maze, vector<vector<bool>>& visited,
              vector<vector<bool>>& path, int x, int y) {
    int n = maze.size();
    int m = maze[0].size();
    if (x < 0 || y < 0 || x >= n || y >= m) return false;
    if (maze[x][y] == 1 || visited[x][y]) return false;
    visited[x][y] = true;
    path[x][y] = true;
    if (x == n - 1 && y == m - 1) return true;
    if (findPath(maze, visited, path, x + 1, y)) return true;
    if (findPath(maze, visited, path, x - 1, y)) return true;
    if (findPath(maze, visited, path, x, y + 1)) return true;
    if (findPath(maze, visited, path, x, y - 1)) return true;
    path[x][y] = false;
    return false;
}

int main() {
    int n, m;
    cout << "Размеры (строки столбцы): ";
    cin >> n >> m;
    vector<vector<int>> maze(n, vector<int>(m));
    for (int i = 0; i < n; i++)
        for (int j = 0; j < m; j++)
            cin >> maze[i][j];

    vector<vector<bool>> visited(n, vector<bool>(m, false));
    vector<vector<bool>> path(n, vector<bool>(m, false));
    if (findPath(maze, visited, path, 0, 0)) {
        for (int i = 0; i < n; i++) {
            for (int j = 0; j < m; j++) {
                if (path[i][j]) cout << "* ";
                else cout << maze[i][j] << " ";
            }
            cout << endl;
        }
    } else {
        cout << "Пути нет" << endl;
    }
    return 0;
}