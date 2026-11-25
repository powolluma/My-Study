using System;
using System.Drawing;
using System.Windows.Forms;

namespace Praktika
{
    public class GameForm : Form
    {
        private int secretNumber;
        private int attempts;
        private const int maxAttempts = 7;

        private Random rnd = new Random();

        private Label lblTitle;
        private Label lblHint;
        private Label lblAttempts;

        private TextBox txtGuess;

        private Button btnCheck;
        private Button btnNewGame;

        public GameForm()
        {
            // Настройка окна
            this.Text = "Угадай число";
            this.Size = new Size(400, 300);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;

            // Заголовок
            lblTitle = new Label();
            lblTitle.Text = "Игра";
            lblTitle.Location = new Point(80, 20);
            lblTitle.Size = new Size(240, 30);
            lblTitle.Font = new Font("Arial", 14, FontStyle.Bold);
            lblTitle.TextAlign = ContentAlignment.MiddleCenter;

            // Поле ввода
            txtGuess = new TextBox();
            txtGuess.Location = new Point(100, 65);
            txtGuess.Size = new Size(180, 25);
            txtGuess.PlaceholderText = "Введите число от 1 до 100";

            // Кнопка проверки
            btnCheck = new Button();
            btnCheck.Text = "Проверить";
            btnCheck.Location = new Point(100, 105);
            btnCheck.Size = new Size(180, 30);
            btnCheck.Click += btnCheck_Click;

            // Подсказка
            lblHint = new Label();
            lblHint.Text = "Попробуйте угадать число!";
            lblHint.Location = new Point(30, 150);
            lblHint.Size = new Size(320, 30);
            lblHint.TextAlign = ContentAlignment.MiddleCenter;

            // Счетчик попыток
            lblAttempts = new Label();
            lblAttempts.Text = "Попытки: 0 / 7";
            lblAttempts.Location = new Point(100, 185);
            lblAttempts.Size = new Size(180, 25);
            lblAttempts.TextAlign = ContentAlignment.MiddleCenter;

            // Кнопка новой игры
            btnNewGame = new Button();
            btnNewGame.Text = "Новая игра";
            btnNewGame.Location = new Point(100, 220);
            btnNewGame.Size = new Size(180, 30);
            btnNewGame.Click += btnNewGame_Click;

            // Добавление элементов
            this.Controls.Add(lblTitle);
            this.Controls.Add(txtGuess);
            this.Controls.Add(btnCheck);
            this.Controls.Add(lblHint);
            this.Controls.Add(lblAttempts);
            this.Controls.Add(btnNewGame);

            // Запуск игры
            NewGame();
        }

        private void NewGame()
        {
            secretNumber = rnd.Next(1, 101);
            attempts = 0;

            lblAttempts.Text = "Попытки: 0 / 7";
            lblHint.Text = "Попробуйте угадать число!";

            txtGuess.Clear();
            txtGuess.Enabled = true;
            btnCheck.Enabled = true;

            txtGuess.Focus();
        }

        private void btnCheck_Click(object sender, EventArgs e)
        {
            if (attempts >= maxAttempts)
                return;

            int guess;

            if (!int.TryParse(txtGuess.Text, out guess))
            {
                lblHint.Text = "Введите целое число!";
                return;
            }

            if (guess < 1 || guess > 100)
            {
                lblHint.Text = "Число должно быть от 1 до 100!";
                return;
            }
            attempts++;

            lblAttempts.Text = "Попытки: " + attempts + " / 7";

            if (guess == secretNumber)
            {
                lblHint.Text = "Вы угадали! Поздравляю!";
                FinishGame();
            }
            else if (attempts == maxAttempts)
            {
                lblHint.Text = "Попытки закончились! Число: " + secretNumber;
                FinishGame();
            }
            else if (guess < secretNumber)
            {
                lblHint.Text = "Загаданное число больше.";
            }
            else
            {
                lblHint.Text = "Загаданное число меньше.";
            }

            txtGuess.Clear();
        }

        private void FinishGame()
        {
            txtGuess.Enabled = false;
            btnCheck.Enabled = false;
        }

        private void btnNewGame_Click(object sender, EventArgs e)
        {
            NewGame();
        }
    }
}
