#pragma once

namespace Task6ButtonClicks
{
    public ref class Form1 : public System::Windows::Forms::Form
    {
    private:
        int clickCount;
        System::Windows::Forms::Button^ button1;
        System::Windows::Forms::Label^ label1;

        void button1_Click(System::Object^ sender, System::EventArgs^ e);

        void InitializeComponent()
        {
            this->button1 = gcnew System::Windows::Forms::Button();
            this->label1 = gcnew System::Windows::Forms::Label();
            this->SuspendLayout();

            this->button1->Location = System::Drawing::Point(35, 30);
            this->button1->Name = L"button1";
            this->button1->Size = System::Drawing::Size(180, 35);
            this->button1->Text = L"Нажми меня";
            this->button1->UseVisualStyleBackColor = true;
            this->button1->Click += gcnew System::EventHandler(this, &Form1::button1_Click);

            this->label1->AutoSize = true;
            this->label1->Location = System::Drawing::Point(35, 85);
            this->label1->Name = L"label1";
            this->label1->Text = L"Нажатий: 0";

            this->ClientSize = System::Drawing::Size(260, 145);
            this->Controls->Add(this->button1);
            this->Controls->Add(this->label1);
            this->Name = L"Form1";
            this->Text = L"Кнопка-счётчик";
            this->ResumeLayout(false);
            this->PerformLayout();
        }

    public:
        Form1() : clickCount(0)
        {
            InitializeComponent();
        }
    };
}
