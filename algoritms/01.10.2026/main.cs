// Задание 1
/*
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Practice9
{
    // Запись типизированного файла
    struct Student
    {
        public string FIO;
        public int Year;
        public string Address;
        public double Avg;
    }

    class MainForm : Form
    {
        const string BinFile = "students.dat";   
        const string TxtFile = "result1.txt";    

        TextBox[] tb;
        DataGridView grid = new DataGridView();
        List<Student> selected = new List<Student>();

        public MainForm()
        {
            Text = "Задание 1: Студенты";
            Width = 950;
            Height = 580;

            // Поля ввода
            string[] names = { "ФИО", "Год рождения", "Адрес", "Средний балл" };
            tb = new TextBox[names.Length];
            Panel panel = new Panel { Dock = DockStyle.Left, Width = 250 };
            for (int i = 0; i < names.Length; i++)
            {
                panel.Controls.Add(new Label { Text = names[i], Left = 10, Top = 10 + i * 50, Width = 220 });
                tb[i] = new TextBox { Left = 10, Top = 30 + i * 50, Width = 220 };
                panel.Controls.Add(tb[i]);
            }

            // Кнопки
            AddButton(panel, "Добавить запись", 240, BtnAdd_Click);
            AddButton(panel, "Тестовые данные", 278, BtnTest_Click);
            AddButton(panel, "Показать все", 316, BtnShowAll_Click);
            AddButton(panel, "Выборка (балл > 3,8)", 354, BtnSelect_Click);
            AddButton(panel, "Сохранить выборку в txt", 392, BtnSave_Click);

            // Таблица
            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(grid);
            Controls.Add(panel);

            ShowTable(ReadAll());
        }

        static void AddButton(Panel p, string text, int top, EventHandler h)
        {
            Button b = new Button { Text = text, Left = 10, Top = top, Width = 220, Height = 30 };
            b.Click += h;
            p.Controls.Add(b);
        }

        static void WriteRecord(BinaryWriter w, Student s)
        {
            w.Write(s.FIO);
            w.Write(s.Year);
            w.Write(s.Address);
            w.Write(s.Avg);
        }

        static Student ReadRecord(BinaryReader r)
        {
            Student s = new Student();
            s.FIO = r.ReadString();
            s.Year = r.ReadInt32();
            s.Address = r.ReadString();
            s.Avg = r.ReadDouble();
            return s;
        }

        static List<Student> ReadAll()
        {
            List<Student> list = new List<Student>();
            if (!File.Exists(BinFile)) return list;
            using (FileStream fs = new FileStream(BinFile, FileMode.Open))
            using (BinaryReader r = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                    list.Add(ReadRecord(r));
            }
            return list;
        }

        void ShowTable(List<Student> list)
        {
            DataTable t = new DataTable();
            t.Columns.Add("ФИО");
            t.Columns.Add("Год рождения", typeof(int));
            t.Columns.Add("Адрес");
            t.Columns.Add("Средний балл", typeof(double));
            foreach (Student s in list)
                t.Rows.Add(s.FIO, s.Year, s.Address, s.Avg);
            grid.DataSource = t;
        }

        void BtnAdd_Click(object sender, EventArgs e)
        {
            double avg;
            int year;
            string avgText = tb[3].Text.Replace(',', '.');
            if (tb[0].Text == "" || tb[2].Text == ""
                || !int.TryParse(tb[1].Text, out year)
                || !double.TryParse(avgText, NumberStyles.Float, CultureInfo.InvariantCulture, out avg))
            {
                MessageBox.Show("Проверьте правильность введённых данных");
                return;
            }

            Student s = new Student { FIO = tb[0].Text, Year = year, Address = tb[2].Text, Avg = avg };
            using (FileStream fs = new FileStream(BinFile, FileMode.Append))
            using (BinaryWriter w = new BinaryWriter(fs))
                WriteRecord(w, s);

            foreach (TextBox t in tb) t.Clear();
            ShowTable(ReadAll());
        }

        void BtnTest_Click(object sender, EventArgs e)
        {
            Student[] data =
            {
                new Student { FIO = "Иванов Иван Иванович",    Year = 2004, Address = "ул. Ленина, 5",     Avg = 4.5 },
                new Student { FIO = "Петрова Анна Сергеевна",  Year = 2005, Address = "ул. Мира, 12",      Avg = 3.6 },
                new Student { FIO = "Сидоров Пётр Олегович",   Year = 2003, Address = "пр. Победы, 33",    Avg = 3.9 },
                new Student { FIO = "Кузнецова Мария Андреевна", Year = 2004, Address = "ул. Гагарина, 7", Avg = 4.8 },
                new Student { FIO = "Смирнов Дмитрий Павлович", Year = 2005, Address = "ул. Пушкина, 21",  Avg = 3.2 },
                new Student { FIO = "Орлова Екатерина Ильинична", Year = 2004, Address = "ул. Садовая, 9", Avg = 3.8 }
            };
            // FileMode.Create: старый файл удаляется
            using (FileStream fs = new FileStream(BinFile, FileMode.Create))
            using (BinaryWriter w = new BinaryWriter(fs))
                foreach (Student s in data) WriteRecord(w, s);

            ShowTable(ReadAll());
        }

        void BtnShowAll_Click(object sender, EventArgs e)
        {
            ShowTable(ReadAll());
        }

        void BtnSelect_Click(object sender, EventArgs e)
        {
            selected.Clear();
            foreach (Student s in ReadAll())
                if (s.Avg > 3.8) selected.Add(s);

            ShowTable(selected);
            if (selected.Count == 0) MessageBox.Show("Нет студентов с баллом > 3,8");
        }

        void BtnSave_Click(object sender, EventArgs e)
        {
            // Если выборка ещё не делалась, делаем её
            if (selected.Count == 0)
                foreach (Student s in ReadAll())
                    if (s.Avg > 3.8) selected.Add(s);

            using (StreamWriter sw = new StreamWriter(TxtFile, false))
            {
                sw.WriteLine("Студенты со средним баллом > 3,8");
                foreach (Student s in selected)
                    sw.WriteLine("{0}; {1}; {2}; {3:F2}", s.FIO, s.Year, s.Address, s.Avg);
            }
            MessageBox.Show("Сохранено в файл " + Path.GetFullPath(TxtFile));
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
*/
// Задание 2
/*
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Windows.Forms;

namespace Practice9
{
    struct Employee
    {
        public string FIO;
        public int BirthYear;
        public string Position;
        public int HireYear;
    }

    class MainForm : Form
    {
        const string BinFile = "employees.dat"; 
        const string TxtFile = "result2.txt";    
        TextBox[] tb;
        DataGridView grid = new DataGridView();
        List<Employee> selected = new List<Employee>();

        public MainForm()
        {
            Text = "Задание 2: Сотрудники фирмы";
            Width = 950;
            Height = 580;

            // Поля ввода
            string[] names = { "ФИО", "Год рождения", "Должность", "Год поступления" };
            tb = new TextBox[names.Length];
            Panel panel = new Panel { Dock = DockStyle.Left, Width = 250 };
            for (int i = 0; i < names.Length; i++)
            {
                panel.Controls.Add(new Label { Text = names[i], Left = 10, Top = 10 + i * 50, Width = 220 });
                tb[i] = new TextBox { Left = 10, Top = 30 + i * 50, Width = 220 };
                panel.Controls.Add(tb[i]);
            }

            AddButton(panel, "Добавить запись", 240, BtnAdd_Click);
            AddButton(panel, "Тестовые данные", 278, BtnTest_Click);
            AddButton(panel, "Показать все", 316, BtnShowAll_Click);
            AddButton(panel, "Выборка (приняты 1999-2002)", 354, BtnSelect_Click);
            AddButton(panel, "Сохранить выборку в txt", 392, BtnSave_Click);

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(grid);
            Controls.Add(panel);

            ShowTable(ReadAll());
        }

        static void AddButton(Panel p, string text, int top, EventHandler h)
        {
            Button b = new Button { Text = text, Left = 10, Top = top, Width = 220, Height = 30 };
            b.Click += h;
            p.Controls.Add(b);
        }

        static void WriteRecord(BinaryWriter w, Employee e)
        {
            w.Write(e.FIO);
            w.Write(e.BirthYear);
            w.Write(e.Position);
            w.Write(e.HireYear);
        }

        static Employee ReadRecord(BinaryReader r)
        {
            Employee e = new Employee();
            e.FIO = r.ReadString();
            e.BirthYear = r.ReadInt32();
            e.Position = r.ReadString();
            e.HireYear = r.ReadInt32();
            return e;
        }

        static List<Employee> ReadAll()
        {
            List<Employee> list = new List<Employee>();
            if (!File.Exists(BinFile)) return list;
            using (FileStream fs = new FileStream(BinFile, FileMode.Open))
            using (BinaryReader r = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                    list.Add(ReadRecord(r));
            }
            return list;
        }

        void ShowTable(List<Employee> list)
        {
            DataTable t = new DataTable();
            t.Columns.Add("ФИО");
            t.Columns.Add("Год рождения", typeof(int));
            t.Columns.Add("Должность");
            t.Columns.Add("Год поступления", typeof(int));
            foreach (Employee e in list)
                t.Rows.Add(e.FIO, e.BirthYear, e.Position, e.HireYear);
            grid.DataSource = t;
        }
        void BtnAdd_Click(object sender, EventArgs ev)
        {
            int birth, hire;
            if (tb[0].Text == "" || tb[2].Text == ""
                || !int.TryParse(tb[1].Text, out birth)
                || !int.TryParse(tb[3].Text, out hire))
            {
                MessageBox.Show("Проверьте правильность введённых данных");
                return;
            }

            Employee emp = new Employee { FIO = tb[0].Text, BirthYear = birth, Position = tb[2].Text, HireYear = hire };
            using (FileStream fs = new FileStream(BinFile, FileMode.Append))
            using (BinaryWriter w = new BinaryWriter(fs))
                WriteRecord(w, emp);

            foreach (TextBox t in tb) t.Clear();
            ShowTable(ReadAll());
        }

        void BtnTest_Click(object sender, EventArgs ev)
        {
            Employee[] data =
            {
                new Employee { FIO = "Алексеев Олег Николаевич",  BirthYear = 1975, Position = "Директор",     HireYear = 1998 },
                new Employee { FIO = "Белова Ирина Петровна",     BirthYear = 1980, Position = "Бухгалтер",    HireYear = 1999 },
                new Employee { FIO = "Волков Сергей Иванович",   BirthYear = 1985, Position = "Программист",  HireYear = 2001 },
                new Employee { FIO = "Громова Ольга Викторовна",  BirthYear = 1990, Position = "Менеджер",     HireYear = 2002 },
                new Employee { FIO = "Дмитриев Максим Андреевич", BirthYear = 1992, Position = "Инженер",      HireYear = 2005 },
                new Employee { FIO = "Егорова Татьяна Юрьевна",   BirthYear = 1988, Position = "Секретарь",    HireYear = 2000 }
            };
            // FileMode.Create: старый файл удаляется
            using (FileStream fs = new FileStream(BinFile, FileMode.Create))
            using (BinaryWriter w = new BinaryWriter(fs))
                foreach (Employee emp in data) WriteRecord(w, emp);

            ShowTable(ReadAll());
        }

        void BtnShowAll_Click(object sender, EventArgs ev)
        {
            ShowTable(ReadAll());
        }

        void BtnSelect_Click(object sender, EventArgs ev)
        {
            selected.Clear();
            foreach (Employee emp in ReadAll())
                if (emp.HireYear >= 1999 && emp.HireYear <= 2002) selected.Add(emp);

            ShowTable(selected);
            if (selected.Count == 0) MessageBox.Show("Нет сотрудников, принятых с 1999 по 2002 год");
        }

        void BtnSave_Click(object sender, EventArgs ev)
        {
            // Если выборка ещё не делалась, делаем её
            if (selected.Count == 0)
                foreach (Employee emp in ReadAll())
                    if (emp.HireYear >= 1999 && emp.HireYear <= 2002) selected.Add(emp);

            using (StreamWriter sw = new StreamWriter(TxtFile, false))
            {
                sw.WriteLine("Сотрудники, принятые с 1999 по 2002 год");
                foreach (Employee emp in selected)
                    sw.WriteLine("{0}; {1}; {2}; {3}", emp.FIO, emp.BirthYear, emp.Position, emp.HireYear);
            }
            MessageBox.Show("Сохранено в файл " + Path.GetFullPath(TxtFile));
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
*/
// Задание 3
/*
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Windows.Forms;

namespace Practice9
{

    struct Car
    {
        public string Brand;
        public string Model;
        public int Year;
        public int Mileage;
        public double Price;
    }

    class MainForm : Form
    {
        const string BinFile = "cars.dat";     
        const string TxtFile = "result3.txt";   

        TextBox[] tb;
        DataGridView grid = new DataGridView();
        List<Car> selected = new List<Car>();

        public MainForm()
        {
            Text = "Задание 3: Автомобили";
            Width = 950;
            Height = 600;

            string[] names = { "Марка", "Модель", "Год выпуска", "Пробег (км)", "Цена (руб.)" };
            tb = new TextBox[names.Length];
            Panel panel = new Panel { Dock = DockStyle.Left, Width = 250 };
            for (int i = 0; i < names.Length; i++)
            {
                panel.Controls.Add(new Label { Text = names[i], Left = 10, Top = 10 + i * 50, Width = 220 });
                tb[i] = new TextBox { Left = 10, Top = 30 + i * 50, Width = 220 };
                panel.Controls.Add(tb[i]);
            }

            AddButton(panel, "Добавить запись", 290, BtnAdd_Click);
            AddButton(panel, "Тестовые данные", 328, BtnTest_Click);
            AddButton(panel, "Показать все", 366, BtnShowAll_Click);
            AddButton(panel, "Выборка (пробег < 50 000)", 404, BtnSelect_Click);
            AddButton(panel, "Сохранить выборку в txt", 442, BtnSave_Click);

            grid.Dock = DockStyle.Fill;
            grid.ReadOnly = true;
            grid.AllowUserToAddRows = false;
            grid.AutoSizeColumnsMode = DataGridViewAutoSizeColumnsMode.Fill;

            Controls.Add(grid);
            Controls.Add(panel);

            ShowTable(ReadAll());
        }

        static void AddButton(Panel p, string text, int top, EventHandler h)
        {
            Button b = new Button { Text = text, Left = 10, Top = top, Width = 220, Height = 30 };
            b.Click += h;
            p.Controls.Add(b);
        }

        static void WriteRecord(BinaryWriter w, Car c)
        {
            w.Write(c.Brand);
            w.Write(c.Model);
            w.Write(c.Year);
            w.Write(c.Mileage);
            w.Write(c.Price);
        }

        static Car ReadRecord(BinaryReader r)
        {
            Car c = new Car();
            c.Brand = r.ReadString();
            c.Model = r.ReadString();
            c.Year = r.ReadInt32();
            c.Mileage = r.ReadInt32();
            c.Price = r.ReadDouble();
            return c;
        }

        static List<Car> ReadAll()
        {
            List<Car> list = new List<Car>();
            if (!File.Exists(BinFile)) return list;
            using (FileStream fs = new FileStream(BinFile, FileMode.Open))
            using (BinaryReader r = new BinaryReader(fs))
            {
                while (fs.Position < fs.Length)
                    list.Add(ReadRecord(r));
            }
            return list;
        }

        void ShowTable(List<Car> list)
        {
            DataTable t = new DataTable();
            t.Columns.Add("Марка");
            t.Columns.Add("Модель");
            t.Columns.Add("Год выпуска", typeof(int));
            t.Columns.Add("Пробег (км)", typeof(int));
            t.Columns.Add("Цена (руб.)", typeof(double));
            foreach (Car c in list)
                t.Rows.Add(c.Brand, c.Model, c.Year, c.Mileage, c.Price);
            grid.DataSource = t;
        }

        void BtnAdd_Click(object sender, EventArgs e)
        {
            int year, mileage;
            double price;
            string priceText = tb[4].Text.Replace(',', '.');
            if (tb[0].Text == "" || tb[1].Text == ""
                || !int.TryParse(tb[2].Text, out year)
                || !int.TryParse(tb[3].Text, out mileage)
                || !double.TryParse(priceText, NumberStyles.Float, CultureInfo.InvariantCulture, out price))
            {
                MessageBox.Show("Проверьте правильность введённых данных");
                return;
            }

            Car c = new Car { Brand = tb[0].Text, Model = tb[1].Text, Year = year, Mileage = mileage, Price = price };
            using (FileStream fs = new FileStream(BinFile, FileMode.Append))
            using (BinaryWriter w = new BinaryWriter(fs))
                WriteRecord(w, c);

            foreach (TextBox t in tb) t.Clear();
            ShowTable(ReadAll());
        }

        void BtnTest_Click(object sender, EventArgs e)
        {
            Car[] data =
            {
                new Car { Brand = "Toyota",  Model = "Camry",   Year = 2019, Mileage = 45000,  Price = 2100000 },
                new Car { Brand = "Kia",     Model = "Rio",     Year = 2015, Mileage = 120000, Price = 650000 },
                new Car { Brand = "BMW",     Model = "X5",      Year = 2021, Mileage = 18000,  Price = 6500000 },
                new Car { Brand = "Lada",    Model = "Vesta",   Year = 2018, Mileage = 78000,  Price = 720000 },
                new Car { Brand = "Skoda",   Model = "Octavia", Year = 2020, Mileage = 49999,  Price = 1850000 },
                new Car { Brand = "Hyundai", Model = "Solaris", Year = 2017, Mileage = 50000,  Price = 800000 }
            };
            // FileMode.Create: старый файл удаляется
            using (FileStream fs = new FileStream(BinFile, FileMode.Create))
            using (BinaryWriter w = new BinaryWriter(fs))
                foreach (Car c in data) WriteRecord(w, c);

            ShowTable(ReadAll());
        }

        void BtnShowAll_Click(object sender, EventArgs e)
        {
            ShowTable(ReadAll());
        }

        void BtnSelect_Click(object sender, EventArgs e)
        {
            selected.Clear();
            foreach (Car c in ReadAll())
                if (c.Mileage < 50000) selected.Add(c);

            ShowTable(selected);
            if (selected.Count == 0) MessageBox.Show("Нет автомобилей с пробегом < 50 000 км");
        }

        void BtnSave_Click(object sender, EventArgs e)
        {
            if (selected.Count == 0)
                foreach (Car c in ReadAll())
                    if (c.Mileage < 50000) selected.Add(c);

            using (StreamWriter sw = new StreamWriter(TxtFile, false))
            {
                sw.WriteLine("Автомобили с пробегом менее 50 000 км");
                foreach (Car c in selected)
                    sw.WriteLine("{0}; {1}; {2}; {3} км; {4:F2} руб.", c.Brand, c.Model, c.Year, c.Mileage, c.Price);
            }
            MessageBox.Show("Сохранено в файл " + Path.GetFullPath(TxtFile));
        }
    }

    static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new MainForm());
        }
    }
}
*/
