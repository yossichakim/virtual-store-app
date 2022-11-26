using System.ComponentModel.DataAnnotations;

namespace SeviceFunction;

/// <summary>
/// An auxiliary class for the logical layer
/// </summary>
internal static class ServiceFunction
{
    /// <summary>
    /// Accepts a string and returns whether it is a valid email address
    /// </summary>
    /// <param name="mail"></param>
    /// <returns>True if the string is a valid email address, false if the string is an invalid email address</returns>
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