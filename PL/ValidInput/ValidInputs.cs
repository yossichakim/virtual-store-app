namespace PL.ValidInput;

using System.Text.RegularExpressions;

/// <summary>
/// for input valid number
/// </summary>
internal static class ValidInputs
{
    /// <summary>
    /// define valid Number
    /// </summary>
    private static Regex validNumber;

    /// <summary>
    /// constructor
    /// </summary>
    //static ValidInputs()
    //{
    //    validNumber = new Regex("^[0-9]+$");
    //}

    /// <summary>
    /// check if is valid number
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    internal static bool isValidNumber(this string input)
    {
        return !int.TryParse(input, out int n);
    }

    /// <summary>
    /// check if is valid price
    /// </summary>
    /// <param name="input"></param>
    /// <returns></returns>
    internal static bool isValidPrice(this string input) => !double.TryParse(input, out double n);
}