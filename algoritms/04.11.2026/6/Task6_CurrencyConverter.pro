QT += widgets

CONFIG += c++17
CONFIG -= app_bundle

TEMPLATE = app
TARGET = Task6_CurrencyConverter

SOURCES += \
    main.cpp \
    mainwindow.cpp \
    CurrencyConverter.cpp

HEADERS += \
    mainwindow.h \
    CurrencyConverter.h

FORMS += mainwindow.ui

win32-msvc {
    QMAKE_CXXFLAGS += /W4
} else {
    QMAKE_CXXFLAGS += -Wall -Wextra -Wpedantic
}
