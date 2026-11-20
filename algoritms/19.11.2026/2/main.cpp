#include <iostream>
#include <string>
#include <vector>
using namespace std;

int main() {
    const int WIDTH = 20;
    const int HEIGHT = 10;

    vector<string> field = {
        "####################",
        "#...#..............#",
        "#...#....##........#",
        "#........##...#....#",
        "#..##.........#....#",
        "#.................##",
        "#....#.............#",
        "#....#....##.......#",
        "#..................#",
        "####################"
    };

    int playerX = 1;
    int playerY = 1;

    // Координаты монет: сначала X, затем Y.
    vector<pair<int, int>> coins = {
        {6, 1}, {15, 1}, {8, 3}, {3, 5}, {12, 6}, {17, 8}
    };

    while (true) {
        // Рисуем поле.
        for (int y = 0; y < HEIGHT; y++) {
            for (int x = 0; x < WIDTH; x++) {
                if (x == playerX && y == playerY) {
                    cout << '@';
                } else {
                    bool coinHere = false;
                    for (const auto& coin : coins) {
                        if (coin.first == x && coin.second == y) {
                            coinHere = true;
                            break;
                        }
                    }

                    if (coinHere) {
                        cout << '*';
                    } else {
                        cout << field[y][x];
                    }
                }
            }
            cout << '\n';
        }

        cout << "Монет осталось: " << coins.size() << '\n';
        cout << "Управление: W A S D, выход: Q\n";

        if (coins.empty()) {
            cout << "Победа! Вы собрали все монеты!\n";
            break;
        }

        cout << "Ваш ход: ";
        char move;
        cin >> move;

        if (move == 'q' || move == 'Q') {
            cout << "Игра завершена.\n";
            break;
        }

        int newX = playerX;
        int newY = playerY;

        if (move == 'w' || move == 'W') {
            newY--;
        } else if (move == 's' || move == 'S') {
            newY++;
        } else if (move == 'a' || move == 'A') {
            newX--;
        } else if (move == 'd' || move == 'D') {
            newX++;
        } else {
            cout << "Неизвестная клавиша. Используйте W, A, S или D.\n";
            continue;
        }

        if (field[newY][newX] != '#') {
            playerX = newX;
            playerY = newY;
        }


        for (int i = 0; i < static_cast<int>(coins.size()); i++) {
            if (coins[i].first == playerX && coins[i].second == playerY) {
                coins.erase(coins.begin() + i);
                break;
            }
        }

        cout << '\n';
    }

    return 0;
}
