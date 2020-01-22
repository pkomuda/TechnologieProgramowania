using System.Globalization;
using System.Windows.Controls;

namespace View.Validators
{
    public class NameValidator : ValidationRule
    {
        public override ValidationResult Validate(object value, CultureInfo cultureInfo)
        {
            string name = (string) value;
            if (name.Length == 0)
                return new ValidationResult(false, "value cannot be empty.");
            else if(name.StartsWith(" ") || name.EndsWith(" "))
                return new ValidationResult(false, "value cannot start or end with space.");
            else if (name.Length > 50 || name.Equals(null))
                return new ValidationResult(false, "value is not suitable for database");
            return ValidationResult.ValidResult;
        }
    }
}
