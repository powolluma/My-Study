using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace WindowsFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        /*
        // Задание 1
        private void button1_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string str = (string)listBox1.Items[index];

            int zeros = 0, ones = 0;
            for (int i = 0; i < str.Length; i++)
            {
                if (str[i] == '0') zeros++;
                else if (str[i] == '1') ones++;
            }
            label1.Text = "Нулей: " + zeros + " Единиц: " + ones;
        }
        */
        // Задание 2
        /*
        private void button2_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string str = (string)listBox1.Items[index];

            string[] words = str.Split(new char[] { ' ' },
                StringSplitOptions.RemoveEmptyEntries);

            label1.Text = "Количество слов = " + words.Length;
        }
        */
        // Задание 4
        /*
        private void button3_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string str = (string)listBox1.Items[index];

            string digits = "";
            for (int i = 0; i < str.Length; i++)
            {
                if (char.IsDigit(str[i]))
                    digits += str[i];
            }

            label1.Text = "Цифры, найденные в строке: " + digits;
        }
        */

        // Задание 8
        /*
        private void button4_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string str = (string)listBox1.Items[index];

            int count = 0;
            for (int i = 0; i < str.Length; i++)
            {
                char c = str[i];
                if ((c >= 'а' && c <= 'я') || c == 'ё')
                    count++;
            }

            label1.Text = "Количество строчных русских букв = " + count;
        }
        */

        // Задание 16
        /*
        private void button5_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string str = (string)listBox1.Items[index];

            string clean = "";
            string lower = str.ToLower();
            for (int i = 0; i < lower.Length; i++)
            {
                if (char.IsLetter(lower[i]))
                    clean += lower[i];
            }

            string reversed = "";
            for (int i = clean.Length - 1; i >= 0; i--)
                reversed += clean[i];

            if (clean == reversed)
                label1.Text = "Строка является палиндромом";
            else
                label1.Text = "Строка не является палиндромом";
        }
        */
    }
}
