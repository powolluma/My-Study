namespace Task10
{
    public class Form1
    {
        private int[,] maze = new int[5, 5];
        private bool[,] visited = new bool[5, 5];

        private bool Find(int row, int column)
        {
            if (row >= 5 || column >= 5 || maze[row, column] == 1 || visited[row, column]) return false;
            visited[row, column] = true;
            if (row == 4 && column == 4) return true;
            if (Find(row, column + 1) || Find(row + 1, column)) return true;
            visited[row, column] = false;
            return false;
        }
    }
}
