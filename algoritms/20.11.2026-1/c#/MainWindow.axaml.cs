using Avalonia.Controls;

using Avalonia.Interactivity;



namespace StudentSurvey;



public partial class MainWindow : Window

{

    public MainWindow()

    {

        InitializeComponent();

    }



    private async void Next_Click(object? sender, RoutedEventArgs e)

    {

        string studentName = NameBox.Text?.Trim() ?? "";

        string group = GroupBox.Text?.Trim() ?? "";



        if (studentName == "" || group == "")

        {

            ErrorText.Text = "Заполните оба поля.";

            return;

        }



        ErrorText.Text = "";

        var resultWindow = new ResultWindow(studentName, group);

        await resultWindow.ShowDialog(this);

    }



    private void Exit_Click(object? sender, RoutedEventArgs e)

    {

        Close();

    }

}
