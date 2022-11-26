using System.ComponentModel.DataAnnotations;

namespace SeviceFunction;

internal static class ServiceFunction
{
    internal static bool IsValidEmail(this string mail)
    {
        return new EmailAddressAttribute().IsValid(mail);
    }

    internal static void CheckPositiveNumber(this int number)
    {
        if (number < 0)
        {
            throw new Exception("not positive number");
        }
    }
}