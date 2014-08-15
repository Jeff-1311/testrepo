using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trudoyomkost

{
    static class VtboxMethods
    {
        public static bool CheckSeria(string input)
        {
            if (input.Equals(""))
                return true;
            if (input.Length == 9)
            {
                return false;
            }
            int number = 0;
            if (int.TryParse(input, out number))
            {
                if (number >= 1 && number < 100)
                {
                    return false;
                }

                else
                {
                    return true;
                }
            }

            return false;
        }

        public static bool checkForInt(string input)
        {
            try
            {
                Int32.Parse(input);
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool checkForDouble(string input)
        {
            try
            {
                Decimal val = Decimal.Parse(input);
                Decimal povCheck = (Decimal)Math.Pow(10, TrudoyomkostSettings.RoundNum);
                Decimal difference = 0;

                difference = val * povCheck - System.Math.Round(val * povCheck);
                if (difference != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }

        public static bool checkWorkRate(string input)
        {
            try
            {
                double difference = 0;
                double val = Double.Parse(input);
                if (val < 1 || val > 6)
                {
                    return false;
                }
                difference = val * 10 - System.Math.Round(val * 10);
                if (difference != 0)
                {
                    return false;
                }
                return true;
            }
            catch
            {
                return false;
            }
        }
        public static bool checkForNonEmpty(string input)
        {
            if (input.Trim().Length == 0)
            {
                return false;
            }
            return true;
        }

        public static string correctForDouble(string input)
        {
            bool oneSeparatorOccurred = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i]))
                {
                    if (input[i] ==
                    System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0])
                    {
                        oneSeparatorOccurred = true;
                        continue;
                    }
                    if (!oneSeparatorOccurred)
                    {
                        input = input.Replace(input[i],
                        System.Globalization.CultureInfo.CurrentCulture.NumberFormat.CurrencyDecimalSeparator[0]);
                    }
                    else
                    {
                        input = input.Replace(new String(input[i], 1), "");
                    }
                }
            }
            return input;
        }
        public static string correctForInt(string input)
        {
            bool oneSeparatorOccurred = false;

            for (int i = 0; i < input.Length; i++)
            {
                if (!Char.IsDigit(input[i]))
                {
                    oneSeparatorOccurred = true;
                }
                if (oneSeparatorOccurred)
                    input = input.Replace(new String(input[i], 1), "");
            }
            return input;
        }
        public static bool CheckProdNume(string productName)
        {

            if (!FillTrudoyomkostDB.DcInfProducts.ContainsKey(productName))
            {
                return false;
            }
            if (string.IsNullOrEmpty(productName))
            {
                return false;
            }

            return true;
        }
    }
}
