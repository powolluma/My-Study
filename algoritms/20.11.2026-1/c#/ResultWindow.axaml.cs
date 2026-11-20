using Avalonia.Controls;

using Avalonia.Interactivity;



namespace StudentSurvey;



public partial class ResultWindow : Window

{

    public ResultWindow(string studentName, string group)

    {

        InitializeComponent();

        ResultText.Text = $"Студент: {studentName}, группа: {group}";

    }



    private void Back_Click(object? sender, RoutedEventArgs e)

    {

        Close();

    }

}
