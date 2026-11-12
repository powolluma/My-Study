using System;
using System.Windows.Forms;
using System.Drawing;

namespace AgeCalculator
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
        }
    }

    public class MainForm : Form
    {
        DateTimePicker dtpBirthDate;
        DateTimePicker dtpCurrentDate;
        NumericUpDown numWorkHours;
        Button btnCalculate;
        Label lblResult;

        public MainForm()
        {
            Text = "Калькулятор возраста и стажа";
            Size = new Size(450, 400);
            StartPosition = FormStartPosition.CenterScreen;

            Label lblBirth = new Label();
            lblBirth.Text = "Дата рождения:";
            lblBirth.Location = new Point(20, 20);
            lblBirth.AutoSize = true;
            Controls.Add(lblBirth);

            dtpBirthDate = new DateTimePicker();
            dtpBirthDate.Location = new Point(180, 20);
            dtpBirthDate.MaxDate = DateTime.Today;
            Controls.Add(dtpBirthDate);

            Label lblCurrent = new Label();
            lblCurrent.Text = "Текущая дата:";
            lblCurrent.Location = new Point(20, 60);
            lblCurrent.AutoSize = true;
            Controls.Add(lblCurrent);

            dtpCurrentDate = new DateTimePicker();
            dtpCurrentDate.Location = new Point(180, 60);
            dtpCurrentDate.Value = DateTime.Today;
            Controls.Add(dtpCurrentDate);

            Label lblHours = new Label();
            lblHours.Text = "Рабочих часов в день:";
            lblHours.Location = new Point(20, 100);
            lblHours.AutoSize = true;
            Controls.Add(lblHours);

            numWorkHours = new NumericUpDown();
            numWorkHours.Location = new Point(180, 100);
            numWorkHours.Minimum = 1;
            numWorkHours.Maximum = 24;
            numWorkHours.Value = 8;
            Controls.Add(numWorkHours);

            btnCalculate = new Button();
            btnCalculate.Text = "Рассчитать";
            btnCalculate.Location = new Point(150, 150);
            btnCalculate.Size = new Size(120, 35);
            btnCalculate.Click += Calculate;
            Controls.Add(btnCalculate);

            lblResult = new Label();
            lblResult.Location = new Point(20, 210);
            lblResult.Size = new Size(390, 130);
            lblResult.BorderStyle = BorderStyle.FixedSingle;
            Controls.Add(lblResult);
        }

        private void Calculate(object sender, EventArgs e)
        {
            DateTime birthDate = dtpBirthDate.Value.Date;
            DateTime currentDate = dtpCurrentDate.Value.Date;

            if (currentDate < birthDate)
            {
                MessageBox.Show("Текущая дата не может быть раньше даты рождения");
                return;
            }

            int years = currentDate.Year - birthDate.Year;

            if (birthDate.AddYears(years) > currentDate)
            {
                years--;
            }

            DateTime dateAfterYears = birthDate.AddYears(years);

            int months = 0;

            while (dateAfterYears.AddMonths(months + 1) <= currentDate)
            {
                months++;
            }

            DateTime dateAfterMonths = dateAfterYears.AddMonths(months);

            int days = (currentDate - dateAfterMonths).Days;

            int workDays = 0;

            for (DateTime date = birthDate; date < currentDate; date = date.AddDays(1))
            {
                if (date.DayOfWeek != DayOfWeek.Saturday &&
                    date.DayOfWeek != DayOfWeek.Sunday)
                {
                    workDays++;
                }
            }

            int workHours = workDays * (int)numWorkHours.Value;

            lblResult.Text =
                "Возраст: " + years + " лет, " +
                months + " месяцев, " + days + " дней\n\n" +
                "Рабочих дней: " + workDays + "\n" +
                "Отработано часов: " + workHours;
        }
    }
}