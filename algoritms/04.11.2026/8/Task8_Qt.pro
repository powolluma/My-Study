QT += widgets

CONFIG += c++17
CONFIG -= app_bundle

TEMPLATE = app
TARGET = Task8_Qt

SOURCES += \
    main.cpp \
    mainwindow.cpp

HEADERS += \
    mainwindow.h

FORMS += \
    mainwindow.ui

QMAKE_CXXFLAGS += -Wall -Wextra

win32-msvc* {
    QMAKE_CXXFLAGS += /W4
}
