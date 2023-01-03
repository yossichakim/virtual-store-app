using System.Text.RegularExpressions;

namespace PL.ValidInput
{
    internal static class ValidInputs
    {
        private static Regex validNumber;

        static ValidInputs()
        {
            validNumber = new Regex("^[0-9]+$");
        }

        internal static bool isValidNumber(this string input) => !int.TryParse(input, out int n);
    }
}