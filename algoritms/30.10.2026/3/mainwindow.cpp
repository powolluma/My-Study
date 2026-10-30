#include "mainwindow.h"
#include "ui_mainwindow.h"

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent), ui(new Ui::MainWindow)
{
    ui->setupUi(this);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::on_btnCheck_clicked()
{
    bool ok = false;
    const int number = ui->lineEditNumber->text().toInt(&ok);

    if (!ok) {
        ui->labelResult->setText("Введите целое число");
        return;
    }

    if (number % 2 == 0) {
        ui->labelResult->setText("Четное");
    } else {
        ui->labelResult->setText("Нечетное");
    }
}
