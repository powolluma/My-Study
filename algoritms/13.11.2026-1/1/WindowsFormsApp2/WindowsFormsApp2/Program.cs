using System;
using System.Drawing;
using System.Windows.Forms;

namespace EventDemo
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new Form1());
        }
    }

    public class Form1 : Form
    {
        Button button1;
        TextBox textBox1;
        Label label1;
        CheckBox checkBox1;

        public Form1()
        {
            Text = "Демонстрация событий C#";
            ClientSize = new Size(380, 250);
            StartPosition = FormStartPosition.CenterScreen;

            button1 = new Button();
            button1.Name = "button1";
            button1.Text = "Нажми меня";
            button1.Location = new Point(20, 20);
            button1.Size = new Size(120, 30);

            textBox1 = new TextBox();
            textBox1.Name = "textBox1";
            textBox1.Location = new Point(20, 65);
            textBox1.Width = 250;

            checkBox1 = new CheckBox();
            checkBox1.Name = "checkBox1";
            checkBox1.Text = "Мой флажок";
            checkBox1.Location = new Point(20, 105);

            label1 = new Label();
            label1.Name = "label1";
            label1.Location = new Point(20, 150);
            label1.Size = new Size(330, 50);
            label1.Text = "Здесь будут результаты";

            Controls.Add(button1);
            Controls.Add(textBox1);
            Controls.Add(checkBox1);
            Controls.Add(label1);

            // Подключение событий
            button1.Click += button1_Click;
            textBox1.TextChanged += textBox1_TextChanged;
            checkBox1.CheckedChanged += checkBox1_CheckedChanged;
            this.MouseMove += Form1_MouseMove;
        }

        // Событие нажатия кнопки
        private void button1_Click(object sender, EventArgs e)
        {
            Button btn = sender as Button;

            if (btn != null)
            {
                label1.Text = "Событие вызвано: " + btn.Name;

                MessageBox.Show(
                    "Вы нажали на кнопку: " + btn.Text,
                    "Событие");
            }
        }

        // Событие изменения текста
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox tb = sender as TextBox;

            if (tb != null)
            {
                this.Text = "Длина текста: " +
                    tb.Text.Length + " символов";

                label1.Text = "Текущий текст: " + tb.Text;
            }
        }

        // Событие изменения состояния флажка
        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if (checkBox1.Checked)
            {
                label1.Text = "Флажок включен";
            }
            else
            {
                label1.Text = "Флажок выключен";
            }
        }

        // Событие движения мыши
        private void Form1_MouseMove(object sender, MouseEventArgs e)
        {
            this.Text = "Координаты мыши: X=" +
                e.X + " Y=" + e.Y;
        }
    }
}