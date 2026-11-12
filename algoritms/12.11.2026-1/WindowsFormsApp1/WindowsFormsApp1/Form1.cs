using System;
using System.Drawing;
using System.IO;
using System.Windows.Forms;

namespace TextEditorLab18
{
    public partial class Form1 : Form
    {
        RichTextBox editorBox;
        ToolStripStatusLabel statusLabel;

        OpenFileDialog openDialog;
        SaveFileDialog saveDialog;
        FontDialog fontDialog;
        ColorDialog colorDialog;

        string currentFile = "";

        public Form1()
        {
            // Настройки окна
            Text = "Текстовый редактор";
            Size = new Size(800, 600);
            StartPosition = FormStartPosition.CenterScreen;

            // Создание меню
            MenuStrip menuStrip = new MenuStrip();

            // Меню Файл
            ToolStripMenuItem fileMenu = new ToolStripMenuItem("Файл");

            ToolStripMenuItem mnuOpen = new ToolStripMenuItem("Открыть...");
            mnuOpen.Click += mnuOpen_Click;

            ToolStripMenuItem mnuSave = new ToolStripMenuItem("Сохранить");
            mnuSave.Click += mnuSave_Click;

            ToolStripMenuItem mnuExit = new ToolStripMenuItem("Выход");
            mnuExit.Click += mnuExit_Click;

            fileMenu.DropDownItems.Add(mnuOpen);
            fileMenu.DropDownItems.Add(mnuSave);
            fileMenu.DropDownItems.Add(mnuExit);

            // Меню Правка
            ToolStripMenuItem editMenu = new ToolStripMenuItem("Правка");

            ToolStripMenuItem mnuReplace = new ToolStripMenuItem("Найти и заменить");
            mnuReplace.Click += mnuReplace_Click;

            editMenu.DropDownItems.Add(mnuReplace);

            // Меню Формат
            ToolStripMenuItem formatMenu = new ToolStripMenuItem("Формат");

            ToolStripMenuItem mnuFont = new ToolStripMenuItem("Шрифт...");
            mnuFont.Click += mnuFont_Click;

            ToolStripMenuItem mnuColor = new ToolStripMenuItem("Цвет текста...");
            mnuColor.Click += mnuColor_Click;

            formatMenu.DropDownItems.Add(mnuFont);
            formatMenu.DropDownItems.Add(mnuColor);

            menuStrip.Items.Add(fileMenu);
            menuStrip.Items.Add(editMenu);
            menuStrip.Items.Add(formatMenu);

            MainMenuStrip = menuStrip;

            // Поле для текста
            editorBox = new RichTextBox();
            editorBox.Dock = DockStyle.Fill;
            editorBox.Font = new Font("Segoe UI", 11);
            editorBox.TextChanged += editorBox_TextChanged;

            // Строка состояния
            StatusStrip statusStrip = new StatusStrip();

            statusLabel = new ToolStripStatusLabel();
            statusLabel.Spring = true;
            statusLabel.TextAlign = ContentAlignment.MiddleLeft;

            statusStrip.Items.Add(statusLabel);

            // Диалоговые окна
            openDialog = new OpenFileDialog();
            openDialog.Filter = "Текстовые файлы (*.txt)|*.txt|Все файлы (*.*)|*.*";

            saveDialog = new SaveFileDialog();
            saveDialog.Filter = "Текстовые файлы (*.txt)|*.txt";
            saveDialog.DefaultExt = "txt";
            saveDialog.AddExtension = true;

            fontDialog = new FontDialog();
            colorDialog = new ColorDialog();

            // Добавление элементов на форму
            Controls.Add(editorBox);
            Controls.Add(statusStrip);
            Controls.Add(menuStrip);

            UpdateStatus();
        }

        // Открытие файла
        private void mnuOpen_Click(object sender, EventArgs e)
        {
            if (openDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    using (StreamReader reader = new StreamReader(openDialog.FileName))
                    {
                        editorBox.Text = reader.ReadToEnd();
                    }

                    currentFile = openDialog.FileName;
                    UpdateStatus();
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Ошибка открытия файла: " + ex.Message);
                }
            }
        }

        // Сохранение файла
        private void mnuSave_Click(object sender, EventArgs e)
        {
            if (currentFile == "")
            {
                if (saveDialog.ShowDialog() != DialogResult.OK)
                    return;

                currentFile = saveDialog.FileName;
            }

            try
            {
                using (StreamWriter writer = new StreamWriter(currentFile))
                {
                    writer.Write(editorBox.Text);
                }

                MessageBox.Show("Файл сохранён!");

                UpdateStatus();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Ошибка сохранения: " + ex.Message);
            }
        }

        // Изменение шрифта
        private void mnuFont_Click(object sender, EventArgs e)
        {
            if (fontDialog.ShowDialog() == DialogResult.OK)
            {
                if (editorBox.SelectionLength > 0)
                {
                    editorBox.SelectionFont = fontDialog.Font;
                }
                else
                {
                    editorBox.Font = fontDialog.Font;
                }
            }
        }

        // Изменение цвета текста
        private void mnuColor_Click(object sender, EventArgs e)
        {
            if (colorDialog.ShowDialog() == DialogResult.OK)
            {
                if (editorBox.SelectionLength > 0)
                {
                    editorBox.SelectionColor = colorDialog.Color;
                }
                else
                {
                    editorBox.ForeColor = colorDialog.Color;
                }
            }
        }

        // Поиск и замена
        private void mnuReplace_Click(object sender, EventArgs e)
        {
            ReplaceForm form = new ReplaceForm(editorBox);
            form.ShowDialog();
        }

        // Выход
        private void mnuExit_Click(object sender, EventArgs e)
        {
            Close();
        }

        // Обновление информации при изменении текста
        private void editorBox_TextChanged(object sender, EventArgs e)
        {
            UpdateStatus();
        }

        // Обновление строки состояния
        private void UpdateStatus()
        {
            string fileName;

            if (currentFile == "")
                fileName = "Новый документ";
            else
                fileName = currentFile;

            statusLabel.Text = "Файл: " + fileName +
                               " | Символов: " + editorBox.TextLength;
        }
    }
}