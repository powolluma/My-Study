#include "CurrencyConverter.h"

#include <stdexcept>

// Учебные фиксированные курсы: 1 USD = 90 RUB, 1 EUR = 100 RUB.
// Это пример для задания, а не актуальные курсы валют.
double CurrencyConverter::fromRubles(double amount, const std::string& currency) const
{
    if (currency == "RUB")
    {
        return amount;
    }
    if (currency == "USD")
    {
        return amount / 90.0;
    }
    if (currency == "EUR")
    {
        return amount / 100.0;
    }

    throw std::invalid_argument("Unknown currency");
}
