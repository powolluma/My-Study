#pragma once

namespace Task7Temperature
{
    public ref class Form1 : public System::Windows::Forms::Form
    {
    private:
        System::Windows::Forms::TextBox^ textBoxCelsius;
        System::Windows::Forms::Button^ buttonConvert;
        System::Windows::Forms::Label^ labelResult;

        void buttonConvert_Click(System::Object^ sender, System::EventArgs^ e);

        void InitializeComponent()
        {
            this->textBoxCelsius = gcnew System::Windows::Forms::TextBox();
            this->buttonConvert = gcnew System::Windows::Forms::Button();
            this->labelResult = gcnew System::Windows::Forms::Label();
            this->SuspendLayout();

            this->textBoxCelsius->Location = System::Drawing::Point(30, 25);
            this->textBoxCelsius->Name = L"textBoxCelsius";
            this->textBoxCelsius->Size = System::Drawing::Size(200, 23);

            this->buttonConvert->Location = System::Drawing::Point(30, 65);
            this->buttonConvert->Name = L"buttonConvert";
            this->buttonConvert->Size = System::Drawing::Size(200, 32);
            this->buttonConvert->Text = L"Перевести";
            this->buttonConvert->UseVisualStyleBackColor = true;
            this->buttonConvert->Click += gcnew System::EventHandler(this, &Form1::buttonConvert_Click);

            this->labelResult->AutoSize = true;
            this->labelResult->Location = System::Drawing::Point(30, 115);
            this->labelResult->Name = L"labelResult";
            this->labelResult->Text = L"Результат появится здесь";

            this->ClientSize = System::Drawing::Size(280, 165);
            this->Controls->Add(this->textBoxCelsius);
            this->Controls->Add(this->buttonConvert);
            this->Controls->Add(this->labelResult);
            this->Name = L"Form1";
            this->Text = L"Конвертер температуры";
            this->ResumeLayout(false);
            this->PerformLayout();
        }

    public:
        Form1()
        {
            InitializeComponent();
        }
    };
}
