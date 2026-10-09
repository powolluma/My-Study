using System.Drawing;

namespace Task7;

public class MainForm : Form
{
    private readonly NumericUpDown rows = new() { Minimum = 1, Maximum = 20, Value = 3 };
    private readonly NumericUpDown cols = new() { Minimum = 1, Maximum = 20, Value = 3 };
    private readonly DataGridView grid = new() { Width = 330, Height = 220, AllowUserToAddRows = false, ReadOnly = true, AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill };

    public MainForm()
    {
        Text = "Задача 7";
        ClientSize = new Size(380, 360);

        var button = new Button { Text = "Создать матрицу", AutoSize = true };
        button.Click += (_, _) =>
        {
            int[,] matrix = GetMatrix((int)rows.Value, (int)cols.Value);
            PrintMatrix(matrix, grid);
        };

        var layout = new FlowLayoutPanel
        {
            Dock = DockStyle.Fill,
            Padding = new Padding(12),
            FlowDirection = FlowDirection.TopDown,
            WrapContents = false
        };

        layout.Controls.Add(new Label { Text = "Строки:", AutoSize = true });
        layout.Controls.Add(rows);
        layout.Controls.Add(new Label { Text = "Столбцы:", AutoSize = true });
        layout.Controls.Add(cols);
        layout.Controls.Add(button);
        layout.Controls.Add(grid);

        Controls.Add(layout);
    }

    private static int[,] GetMatrix(int rows, int cols)
    {
        var random = new Random();
        var matrix = new int[rows, cols];

        for (int row = 0; row < rows; row++)
        {
            for (int column = 0; column < cols; column++)
            {
                matrix[row, column] = random.Next(0, 100);
            }
        }

        return matrix;
    }

    private static void PrintMatrix(int[,] matrix, DataGridView grid)
    {
        grid.Rows.Clear();
        grid.Columns.Clear();

        for (int column = 0; column < matrix.GetLength(1); column++)
        {
            grid.Columns.Add($"col{column}", (column + 1).ToString());
        }

        grid.Rows.Add(matrix.GetLength(0));

        for (int row = 0; row < matrix.GetLength(0); row++)
        {
            for (int column = 0; column < matrix.GetLength(1); column++)
            {
                grid.Rows[row].Cells[column].Value = matrix[row, column];
            }
        }
    }
}
