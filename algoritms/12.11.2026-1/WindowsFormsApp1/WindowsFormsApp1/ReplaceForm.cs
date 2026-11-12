using System;
using System.Drawing;
using System.Windows.Forms;

namespace TextEditorLab18
{
    public class ReplaceForm : Form
    {
        RichTextBox editor;

        TextBox txtFind;
        TextBox txtReplace;

        Button btnReplace;

        public ReplaceForm(RichTextBox richTextBox)
        {
            editor = richTextBox;

            Text = "Найти и заменить";
            Size = new Size(350, 220);
            StartPosition = FormStartPosition.CenterParent;
            FormBorderStyle = FormBorderStyle.FixedDialog;
            MaximizeBox = false;

            // Поле поиска
            Label lblFind = new Label();
            lblFind.Text = "Найти текст:";
            lblFind.Location = new Point(20, 20);
            lblFind.AutoSize = true;
            Controls.Add(lblFind);

            txtFind = new TextBox();
            txtFind.Location = new Point(120, 20);
            txtFind.Size = new Size(180, 25);
            Controls.Add(txtFind);

            // Поле замены
            Label lblReplace = new Label();
            lblReplace.Text = "Заменить на:";
            lblReplace.Location = new Point(20, 60);
            lblReplace.AutoSize = true;
            Controls.Add(lblReplace);

            txtReplace = new TextBox();
            txtReplace.Location = new Point(120, 60);
            txtReplace.Size = new Size(180, 25);
            Controls.Add(txtReplace);

            // Кнопка
            btnReplace = new Button();
            btnReplace.Text = "Заменить";
            btnReplace.Location = new Point(120, 110);
            btnReplace.Size = new Size(110, 35);
            btnReplace.Click += btnReplace_Click;
            Controls.Add(btnReplace);
        }

        // Замена всех найденных слов
        private void btnReplace_Click(object sender, EventArgs e)
        {
            string searchText = txtFind.Text;
            string replaceText = txtReplace.Text;

            if (searchText == "")
            {
                MessageBox.Show("Введите текст для поиска!");
                return;
            }

            int startIndex = 0;
            int count = 0;

            while (startIndex <= editor.TextLength - searchText.Length)
            {
                int index = editor.Find(
                    searchText,
                    startIndex,
                    RichTextBoxFinds.None
                );

                if (index == -1)
                    break;

                editor.Select(index, searchText.Length);
                editor.SelectedText = replaceText;

                startIndex = index + replaceText.Length;

                count++;
            }

            MessageBox.Show("Количество замен: " + count);
        }
    }
}