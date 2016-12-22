using System.Text;

namespace Phoneword.Core
{
    public static class PhonewordTranslator
    {
        public static bool TryToNumber(string raw, out string val)
        {
            if (string.IsNullOrWhiteSpace(raw))
            {
                val = default(string);
                return false;
            }

            raw = raw.ToUpperInvariant();

            var newNumber = new StringBuilder();
            foreach (var c in raw)
            {
                if (" -0123456789".Contains(c))
                    newNumber.Append(c);
                else
                {
                    int result;
                    if (TryTranslateToNumber(c, out result))
                        newNumber.Append(result);// Bad character?
                    else
                    {
                        val = default(string);
                        return false;
                    }
                }
            }
            val = newNumber.ToString();
            return true;
        }

        static bool Contains(this string keyString, char c)
        {
            return keyString.IndexOf(c) >= 0;
        }

        static readonly string[] digits = {
            "ABC", "DEF", "GHI", "JKL", "MNO", "PQRS", "TUV", "WXYZ"
        };

        static bool TryTranslateToNumber(char c, out int val)
        {
            for (int i = 0; i < digits.Length; i++)
            {
                if (digits[i].Contains(c))
                {
                    val = 2 + i;
                    return true;
                }
            }
            val = (default(int));
            return false;
        }
    }
}