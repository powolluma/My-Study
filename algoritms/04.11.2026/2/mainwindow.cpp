#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    // Соединение сигналов компонентов с обработчиками
    connect(ui->checkBox, &QCheckBox::toggled,
            this, &MainWindow::onCheckBoxToggled);
    connect(ui->radioButton, &QRadioButton::toggled,
            this, &MainWindow::onFirstRadioButtonToggled);
    connect(ui->radioButton_2, &QRadioButton::toggled,
            this, &MainWindow::onSecondRadioButtonToggled);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::onCheckBoxToggled(bool checked)
{
    if (checked) {
        ui->label->setStyleSheet("background-color: lightgreen;");
    } else {
        ui->label->setStyleSheet("");
    }
}

void MainWindow::onFirstRadioButtonToggled(bool checked)
{
    if (checked) {
        ui->label->setText("Привет!");
    }
}

void MainWindow::onSecondRadioButtonToggled(bool checked)
{
    if (checked) {
        ui->label->setText("Добро пожаловать!");
    }
}
