#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QComboBox>
#include <QFont>
#include <QSpinBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent)
    , ui(new Ui::MainWindow)
{
    ui->setupUi(this);

    // Отдельный слот для изменения выбранного пункта
    connect(ui->comboBox,
            QOverload<int>::of(&QComboBox::currentIndexChanged),
            this,
            &MainWindow::onComboBoxChanged);

    // Отдельный слот для изменения шрифта
    connect(ui->spinBox,
            QOverload<int>::of(&QSpinBox::valueChanged),
            this,
            &MainWindow::onFontSizeChanged);

    onComboBoxChanged(ui->comboBox->currentIndex());
    onFontSizeChanged(ui->spinBox->value());
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::onComboBoxChanged(int index)
{
    if (index >= 0) {
        ui->label->setText(ui->comboBox->itemText(index));
    }
}

void MainWindow::onFontSizeChanged(int size)
{
    QFont font = ui->label->font();
    font.setPointSize(size);
    ui->label->setFont(font);
}
