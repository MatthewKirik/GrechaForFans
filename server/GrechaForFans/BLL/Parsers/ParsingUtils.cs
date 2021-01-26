using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace BLL.Parsers
{
    static class ParsingUtils
    {
        private static Regex floatKilogramsRegex = new Regex("(\\d,)\\d+\\s*(кг)", RegexOptions.IgnoreCase);
        private static Regex kilogramsRegex = new Regex("\\d+\\s*(кг)", RegexOptions.IgnoreCase);
        private static Regex gramsRegex = new Regex("\\d+\\s*(г)", RegexOptions.IgnoreCase);

        public static int GetGrams(string str)
        {
            var gramsRes = gramsRegex.Match(str);
            if (gramsRes.Success)
                return int.Parse(new string(gramsRes.Value.TakeWhile(x => char.IsDigit(x)).ToArray()));

            string kiloStr;

            var floatKilogramsRes = floatKilogramsRegex.Match(str);
            if (floatKilogramsRes.Success)
            {
                kiloStr = new string(
                    floatKilogramsRes.Value.TakeWhile(x => char.IsDigit(x) || x == ',' || x == '.')
                    .ToArray());
                float kilograms = float.Parse(kiloStr, System.Globalization.NumberStyles.Any);
                return Convert.ToInt32(kilograms * 1000);
            }

            var kilogramsRes = kilogramsRegex.Match(str);
            if (kilogramsRes.Success)
            {
                kiloStr = new string(kilogramsRes.Value.TakeWhile(x => char.IsDigit(x)).ToArray());
                int grams = int.Parse(kiloStr) * 1000;
                return grams;
            }
            return 1000;
        }
    }
}
