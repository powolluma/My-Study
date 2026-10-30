#include "Form1.h"

void Task7Temperature::Form1::buttonConvert_Click(System::Object^ sender, System::EventArgs^ e)
{
    try {
        double celsius = System::Convert::ToDouble(textBoxCelsius->Text);
        double fahrenheit = celsius * 9 / 5 + 32;
        labelResult->Text = System::String::Format(L"{0:F2} °F", fahrenheit);
    }
    catch (System::FormatException^) {
        labelResult->Text = L"Введите число";
    }
    catch (System::OverflowException^) {
        labelResult->Text = L"Число слишком большое";
    }
}
