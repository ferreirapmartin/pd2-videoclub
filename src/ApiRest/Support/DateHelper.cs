using System;
using System.Globalization;

namespace ApiRest.Support
{
    public class DateHelper
    {
        private static bool IsValid(string value, out DateTime result)
            => DateTime.TryParseExact(value, Format, CultureInfo.InvariantCulture, DateTimeStyles.None, out result);

        public const string Format = "yyyy/MM/dd";

        public virtual bool IsValid(string value) => IsValid(value, out var _);

        public static DateTime Parse(string value)
        {
            if (value is null) 
                throw new ArgumentNullException(nameof(value));
            if (IsValid(value, out var result))
                return result;
            throw new InvalidCastException(Constants.ExceptionsMessages.InvalidCastDate);
        }
    }
}
