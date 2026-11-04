#include "mainwindow.h"
#include "ui_mainwindow.h"

#include <QMessageBox>

MainWindow::MainWindow(QWidget *parent)
    : QMainWindow(parent),
      ui(new Ui::MainWindow),
      timer(new QTimer(this)),
      remainingSeconds(10)
{
    ui->setupUi(this);

    timer->setInterval(1000);

    connect(ui->startButton, &QPushButton::clicked,
            this, &MainWindow::startCountdown);
    connect(ui->stopButton, &QPushButton::clicked,
            this, &MainWindow::stopCountdown);
    connect(timer, &QTimer::timeout,
            this, &MainWindow::updateCountdown);

    remainingSeconds = ui->secondsSpinBox->value();
    updateTimeLabel();
    setIdleState();
}

MainWindow::~MainWindow()
{
    delete ui;
}

void MainWindow::startCountdown()
{
    remainingSeconds = ui->secondsSpinBox->value();
    updateTimeLabel();

    ui->startButton->setEnabled(false);
    ui->stopButton->setEnabled(true);
    ui->secondsSpinBox->setEnabled(false);

    timer->start();
}

void MainWindow::stopCountdown()
{
    timer->stop();
    setIdleState();
    ui->timeLabel->setText(QStringLiteral("Таймер остановлен"));
}

void MainWindow::updateCountdown()
{
    --remainingSeconds;
    updateTimeLabel();

    if (remainingSeconds <= 0) {
        timer->stop();
        setIdleState();
        ui->timeLabel->setText(QStringLiteral("Время вышло!"));
        QMessageBox::information(this,
                                 QStringLiteral("Таймер"),
                                 QStringLiteral("Время вышло!"));
    }
}

void MainWindow::updateTimeLabel()
{
    ui->timeLabel->setText(
        QStringLiteral("Осталось: %1 сек.").arg(remainingSeconds));
}

void MainWindow::setIdleState()
{
    ui->startButton->setEnabled(true);
    ui->stopButton->setEnabled(false);
    ui->secondsSpinBox->setEnabled(true);
}
