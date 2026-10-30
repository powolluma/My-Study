#include "Form1.h"

void Task6ButtonClicks::Form1::button1_Click(System::Object^ sender, System::EventArgs^ e)
{
    clickCount++;
    button1->Text = System::String::Format(L"Нажато {0} раз", clickCount);
    label1->Text = System::String::Format(L"Нажатий: {0}", clickCount);
}
