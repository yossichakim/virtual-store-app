using System.Globalization;
using System.Text.RegularExpressions;
using System.Windows.Controls;

namespace PL.ValidInput;

public class CastStringToIntValidationRule : ValidationRule
{
    public string? ErrorMessageEmpty { get; set; }
    public string? ErrorMessageMaxLength { get; set; }
    public string? ErrorMessageOnlyDigits { get; set; }
    public int MinLength { get; set; }

    public override ValidationResult Validate(object value, CultureInfo cultureInfo)
    {
        string str = value.ToString()!;

        if (string.IsNullOrEmpty(str))
        {
            return new ValidationResult(false, ErrorMessageEmpty);
        }

        if (str.Length < MinLength && new Regex("[0-9]").IsMatch(str[str.Length - 1].ToString()))
        {
            return new ValidationResult(false, ErrorMessageMaxLength);
        }
        return ValidationResult.ValidResult;
    }
}