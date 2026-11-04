#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QMessageBox>
#include <QString>

#include <iomanip>
#include <sstream>
#include <stdexcept>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent),
      ui(new Ui::MainWindow)
{
    ui->setupUi(this);
    connect(ui->convertButton, &QPushButton::clicked,
            this, &MainWindow::onConvertClicked);
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::onConvertClicked()
{
    const double amount = ui->amountSpinBox->value();
    const std::string currency = ui->currencyComboBox->currentText().toStdString();

    try
    {
        const double result = converter.fromRubles(amount, currency);

        std::ostringstream output;
        output << std::fixed << std::setprecision(2)
               << amount << " RUB = " << result << ' ' << currency;

        ui->resultLabel->setText(QString::fromStdString(output.str()));
    }
    catch (const std::exception& error)
    {
        QMessageBox::warning(this, "Ошибка", error.what());
    }
}
