#include "Form1.h"

[System::STAThread]
int main(array<System::String^>^ args)
{
    System::Windows::Forms::Application::EnableVisualStyles();
    System::Windows::Forms::Application::SetCompatibleTextRenderingDefault(false);
    Task8Calculator::Form1 form;
    System::Windows::Forms::Application::Run(%form);
    return 0;
}
