using System;
using System.Collections.Generic;
using System.Text;

namespace GLORIA.API.Utils
{

        public static class RomanConverter
        {
                public static string ToRoman(int number)
                {
                        if (number < 1 || number > 3999)
                                throw new ArgumentOutOfRangeException(nameof(number), "The input must be a positive integer between 1 and 3999");

                        int[] values = { 1000, 900, 500, 400, 100, 90, 50, 40, 10, 9, 5, 4, 1 };
                        string[] symbols = { "M", "CM", "D", "CD", "C", "XC", "L", "XL", "X", "IX", "V", "IV", "I" };

                        StringBuilder result = new StringBuilder();

                        for (int i = 0; i < values.Length; i++)
                        {
                                while (number >= values[i])
                                {
                                        result.Append(symbols[i]);
                                        number -= values[i];
                                }
                        }

                        return result.ToString();
                }
        }









}
