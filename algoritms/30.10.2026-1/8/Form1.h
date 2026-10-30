#pragma once

namespace Task8Calculator
{
    public ref class Form1 : public System::Windows::Forms::Form
    {
    private:
        System::Windows::Forms::TextBox^ textBoxResult;
        System::Windows::Forms::TableLayoutPanel^ mainLayout;
        System::Windows::Forms::TableLayoutPanel^ buttonsLayout;
        double firstNumber;
        System::String^ operation;
        bool startNewNumber;

        void AddButton(System::String^ text, int row, int column);

        void button_Click(System::Object^ sender, System::EventArgs^ e);

        void InitializeComponent()
        {
            this->textBoxResult = gcnew System::Windows::Forms::TextBox();
            this->mainLayout = gcnew System::Windows::Forms::TableLayoutPanel();
            this->buttonsLayout = gcnew System::Windows::Forms::TableLayoutPanel();
            this->SuspendLayout();

            // DockStyle::Fill makes the main layout follow the form size.
            this->mainLayout->ColumnCount = 1;
            this->mainLayout->RowCount = 2;
            this->mainLayout->Dock = System::Windows::Forms::DockStyle::Fill;
            this->mainLayout->RowStyles->Add(gcnew System::Windows::Forms::RowStyle(
                System::Windows::Forms::SizeType::Percent, 20));
            this->mainLayout->RowStyles->Add(gcnew System::Windows::Forms::RowStyle(
                System::Windows::Forms::SizeType::Percent, 80));

            // The result field fills the top row, so it stretches across the window.
            this->textBoxResult->Dock = System::Windows::Forms::DockStyle::Fill;
            this->textBoxResult->ReadOnly = true;
            this->textBoxResult->Text = L"0";
            this->textBoxResult->TextAlign = System::Windows::Forms::HorizontalAlignment::Right;
            this->textBoxResult->Font = gcnew System::Drawing::Font(L"Arial", 18);
            this->mainLayout->Controls->Add(this->textBoxResult, 0, 0);

            // The button table fills the lower row and keeps the buttons at the bottom.
            this->buttonsLayout->ColumnCount = 4;
            this->buttonsLayout->RowCount = 4;
            this->buttonsLayout->Dock = System::Windows::Forms::DockStyle::Fill;
            for (int i = 0; i < 4; i++) {
                this->buttonsLayout->ColumnStyles->Add(gcnew System::Windows::Forms::ColumnStyle(
                    System::Windows::Forms::SizeType::Percent, 25));
                this->buttonsLayout->RowStyles->Add(gcnew System::Windows::Forms::RowStyle(
                    System::Windows::Forms::SizeType::Percent, 25));
            }

            AddButton(L"7", 0, 0); AddButton(L"8", 0, 1); AddButton(L"9", 0, 2); AddButton(L"+", 0, 3);
            AddButton(L"4", 1, 0); AddButton(L"5", 1, 1); AddButton(L"6", 1, 2); AddButton(L"-", 1, 3);
            AddButton(L"1", 2, 0); AddButton(L"2", 2, 1); AddButton(L"3", 2, 2); AddButton(L"C", 2, 3);
            AddButton(L"0", 3, 0); AddButton(L"=", 3, 3);
            this->mainLayout->Controls->Add(this->buttonsLayout, 0, 1);

            this->ClientSize = System::Drawing::Size(320, 420);
            this->Controls->Add(this->mainLayout);
            this->Name = L"Form1";
            this->Text = L"Учебный калькулятор";
            this->ResumeLayout(false);
        }

    public:
        Form1() : firstNumber(0), operation(L""), startNewNumber(true)
        {
            InitializeComponent();
        }
    };
}
