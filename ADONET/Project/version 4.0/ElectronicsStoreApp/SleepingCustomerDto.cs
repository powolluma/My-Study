using System;

namespace ElectronicsStoreApp
{
    // Задание 5.2
    // DTO класс для результата запроса
    public class SleepingCustomerDto
    {
        public string FullName { get; set; }

        public string Email { get; set; }

        public DateTime RegistrationDate { get; set; }
    }
}
