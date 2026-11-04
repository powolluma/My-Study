cmake_minimum_required(VERSION 3.16)
project(Task6_CurrencyConverter LANGUAGES CXX)

set(CMAKE_CXX_STANDARD 17)
set(CMAKE_CXX_STANDARD_REQUIRED ON)
set(CMAKE_CXX_EXTENSIONS OFF)

set(CMAKE_AUTOMOC ON)
set(CMAKE_AUTOUIC ON)
set(CMAKE_AUTORCC ON)

find_package(QT NAMES Qt6 Qt5 REQUIRED COMPONENTS Widgets)
find_package(Qt${QT_VERSION_MAJOR} REQUIRED COMPONENTS Widgets)

add_executable(Task6_CurrencyConverter
    main.cpp
    mainwindow.cpp
    mainwindow.h
    mainwindow.ui
    CurrencyConverter.cpp
    CurrencyConverter.h
)

target_link_libraries(Task6_CurrencyConverter PRIVATE Qt${QT_VERSION_MAJOR}::Widgets)

if(MSVC)
    target_compile_options(Task6_CurrencyConverter PRIVATE /W4)
else()
    target_compile_options(Task6_CurrencyConverter PRIVATE -Wall -Wextra -Wpedantic)
endif()
