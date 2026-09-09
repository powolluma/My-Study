namespace WinFormsApp1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            label1.Text = "привет";
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.BackColor = Color.LightBlue;
            label1.Text = "до свидания";
        }
    }
}
