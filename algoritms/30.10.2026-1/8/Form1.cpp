#include "Form1.h"

void Task8Calculator::Form1::AddButton(System::String^ text, int row, int column)
{
    System::Windows::Forms::Button^ button = gcnew System::Windows::Forms::Button();
    button->Text = text;
    button->Name = System::String::Format(L"button_{0}_{1}", row, column);
    button->Dock = System::Windows::Forms::DockStyle::Fill;
    button->Margin = System::Windows::Forms::Padding(3);
    button->Click += gcnew System::EventHandler(this, &Task8Calculator::Form1::button_Click);
    this->buttonsLayout->Controls->Add(button, column, row);
}

void Task8Calculator::Form1::button_Click(System::Object^ sender, System::EventArgs^ e)
{
    System::Windows::Forms::Button^ button = safe_cast<System::Windows::Forms::Button^>(sender);
    System::String^ value = button->Text;

    if (value == L"C") {
        textBoxResult->Text = L"0";
        firstNumber = 0;
        operation = L"";
        startNewNumber = true;
        return;
    }

    if (value == L"0" || value == L"1" || value == L"2" || value == L"3" || value == L"4" ||
        value == L"5" || value == L"6" || value == L"7" || value == L"8" || value == L"9") {
        if (startNewNumber || textBoxResult->Text == L"0") {
            textBoxResult->Text = value;
        } else {
            textBoxResult->Text += value;
        }
        startNewNumber = false;
        return;
    }

    if (value == L"+" || value == L"-") {
        firstNumber = System::Convert::ToDouble(textBoxResult->Text);
        operation = value;
        startNewNumber = true;
        return;
    }

    if (value == L"=" && operation != L"") {
        double secondNumber = System::Convert::ToDouble(textBoxResult->Text);
        double result = firstNumber;

        if (operation == L"+") {
            result = firstNumber + secondNumber;
        } else if (operation == L"-") {
            result = firstNumber - secondNumber;
        }

        textBoxResult->Text = result.ToString();
        operation = L"";
        startNewNumber = true;
    }
}
