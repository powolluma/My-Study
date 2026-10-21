#include <iostream>
#include <stack>
#include <vector>
#include <algorithm>
using namespace std;

int main() {
    const int R = 5, C = 5;

    int maze[R][C] = {
        {0, 0, 1, 0, 0},
        {1, 0, 1, 0, 1},
        {0, 0, 0, 0, 0},
        {0, 1, 1, 1, 0},
        {0, 0, 0, 1, 0}
    };

    bool visited[R][C] = {};
    pair<int, int> parent[R][C];

    for (int i = 0; i < R; i++)
        for (int j = 0; j < C; j++)
            parent[i][j] = {-1, -1};

    pair<int, int> start = {0, 0};
    pair<int, int> finish = {4, 4};

    stack<pair<int, int>> s;
    s.push(start);
    visited[0][0] = true;

    int dr[] = {-1, 1, 0, 0};
    int dc[] = {0, 0, -1, 1};
    bool found = false;

    while (!s.empty()) {
        pair<int, int> p = s.top();
        s.pop();

        int r = p.first;
        int c = p.second;

        if (p == finish) {
            found = true;
            break;
        }

        for (int k = 0; k < 4; k++) {
            int nr = r + dr[k];
            int nc = c + dc[k];

            if (nr >= 0 && nr < R &&
                nc >= 0 && nc < C &&
                maze[nr][nc] == 0 &&
                !visited[nr][nc]) {

                visited[nr][nc] = true;
                parent[nr][nc] = {r, c};
                s.push({nr, nc});
            }
        }
    }

    if (!found) {
        cout << "Путь не существует\n";
        return 0;
    }

    vector<pair<int, int>> path;

    for (pair<int, int> p = finish;
         p.first != -1;
         p = parent[p.first][p.second]) {
        path.push_back(p);
    }

    reverse(path.begin(), path.end());

    cout << "Путь:\n";
    for (auto p : path)
        cout << '(' << p.first << ", " << p.second << ")\n";

    return 0;
}