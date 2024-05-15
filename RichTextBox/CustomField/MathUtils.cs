using System;
using System.Text;

namespace CustomField
{
    internal static class MathUtils
    {
        private static readonly int[] romanNumeralValues = new int[] { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
        private static readonly string[] romanNumerals = new string[] { "m", "cm", "d", "cd", "c", "xc", "l", "xl", "x", "ix", "v", "iv", "i" };

        public static string ToRoman(int number)
        {
            if (number < 0)
            {
                throw new ArgumentException("Value must be positive.", "number");
            }

            if (number == 0)
            {
                return "n";
            }

            if (number > 32767)
            {
                number++;
            }
            number %= 32767 + 1;

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < 13; i++)
            {
                while (number >= romanNumeralValues[i])
                {
                    number -= romanNumeralValues[i];
                    result.Append(romanNumerals[i].ToUpper());
                }
            }

            return result.ToString();
        }
    }
}
