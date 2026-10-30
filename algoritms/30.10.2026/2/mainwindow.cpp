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

void MainWindow::on_btnSum_clicked()
{
    bool okA = false;
    bool okB = false;
    const double numberA = ui->lineEditA->text().toDouble(&okA);
    const double numberB = ui->lineEditB->text().toDouble(&okB);

    if (!okA || !okB) {
        ui->labelResult->setText("Введите два числа");
        return;
    }

    ui->labelResult->setText(QString("Сумма: %1").arg(numberA + numberB));
}
