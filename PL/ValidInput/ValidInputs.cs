using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace PL.ValidInput
{
    static class ValidInputs
    {
        private static Regex validNumber;

        static ValidInputs()
        {
            validNumber = new Regex("^[0-9]+$");
        }

        internal static bool isValidNumber(this string input) => !int.TryParse(input, out int n);
    }
}
