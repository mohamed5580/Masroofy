using System;

namespace Masroofy.Business.Services
{
    public class ValidationService
    {
        public bool IsValidAmount(decimal amount) => amount > 0;

        public bool IsValidDateRange(DateTime start, DateTime end) => start < end;

        public bool IsValidPin(string pin) => pin != null && pin.Length == 4 && int.TryParse(pin, out _);
    }
}