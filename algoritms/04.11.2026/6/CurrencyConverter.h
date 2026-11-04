#pragma once

#include <string>

class CurrencyConverter
{
public:
    // Переводит сумму из рублей в выбранную валюту.
    double fromRubles(double amount, const std::string& currency) const;
};
