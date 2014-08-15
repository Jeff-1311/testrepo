using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Trudoyomkost
{
    class MathFunctionForSeries
    {
        //преобразует серийный номер самолёта в порядковый (число). Правильно работает только при серии в 10 машин
        public static int GetIntSeriaNumber(string arg)
        {
            if (arg == "99999999" || arg.Trim() == ""|| arg =="999999" || arg =="9999999")//Кульбака требует чтобы неогр. номер не отображался
            {
                return 99999999;
            }
            int number = 0;
            //-------------Ввод номера машины через тире
            if (arg.Contains('-'))
            {
                string[] splitStrings = arg.Split('-');
                if (splitStrings.Length != 2)
                {
                    return -1;
                }
                if (splitStrings[1].Length != 2)
                {
                    return -1;
                }
                string strNum = splitStrings[0] + splitStrings[1];
                try
                {
                    number = Convert.ToInt32(strNum);

                    if (Convert.ToInt32(splitStrings[1]) > 10)
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
            else
            {
                try
                {
                    number = Convert.ToInt32(arg);
                    int devideModRes = number % 10;
                    if (number >= 1 && number < 10)
                    {
                        return -1;
                    }
                    string newstr = arg.Substring(arg.Length - 2, 2);
                    if (Convert.ToInt32(arg.Substring(arg.Length - 2, 2)) > 10)
                    {
                        return -1;
                    }
                }
                catch
                {
                    return -1;
                }
            }
            int high = number / 100;
            int low = number - high * 100;
            high--;
            return high * 10 + low;
        }
        //Преобразует порядковый номер машины в серийный. Правильно работает только при серии в 10 машин

        public static string GetStringSeriaNumber(int arg)
        {
            if (arg == 99999999 || arg== 9999999 || arg==999999)
            {
                return " ";//Кульбака требует чтобы неогр. номер не отображался
                //return "99999999";
            }string high = (arg / 10 + 1).ToString("00");
            string low = (arg - 10 * (arg / 10)).ToString("00");
            if (low == "00")
            {
                low = "10";
                high = (arg / 10).ToString("00");
            }

            return high + low;
        }

        //по-моему, на практике ни разу не использовал этот метод
        //Не совсем нормализация - просто элементы массива делятся на свою сумму, т.е. сумма всех элементов выходного
        //массива будет равна единице. Да. Скорее, этот метод выполняем масштабирование к единице
        //Насколько я понял, на входе должен быть массив длин отрезков, а метод вернёт массив отрезков, который
        //является подобным (геометрия) к исходному массиву, но всё умещается на отрезке в MULTIPLIER пикселей
        public static float[] normalize(int[] input, float multiplier)
        {
            if (input.Length == 0)
            {
                return null;
            }

            float sum = 0;

            for (int i = 0; i < input.Length; i++)
            {
                sum += input[i];
            }

            float[] output = new float[input.Length];

            for (int i = 0; i < input.Length; i++)
            {
                output[i] = multiplier * (float)input[i] / sum;
            }

            return output;
        }

        //Если аргумент - полное имя, то возвращает первую строку полностью (предположительно, фамилия),
        //а от остальных строк берёт по первому символу (инициалы)
        public static string getFirstNameInitials(string fullname)
        {
            string output = "";
            try
            {
                string[] mySubstrings = fullname.Split(' ');
                output = mySubstrings[0];
                for (int i = 1; i < mySubstrings.Length; i++)
                {
                    output += " " + mySubstrings[i].Substring(0, 1) + ".";
                }
            }
            catch
            {

            }
            return output;
        }

        public static string formatIntTo8Numbers(int input)
        {
            if (input > 99999999)
            {
                throw new Exception("Слишком большое значение. Должно быть меньше восьми символов.");
            }
            if (input < 0)
            {
                throw new Exception("Номер изделия не может быть отрицательным");

            }
            string output = new string(' ', 8 - input.ToString().Length);
            output += input.ToString();
            return output;
        }

        public static string GetOldFormatSeria(int inputnum)
        {
            if (inputnum<=9)
            {
                return "       " + inputnum.ToString();
            }
            if (inputnum<=99)
            {
                return "      " + inputnum.ToString();
            }
            if (inputnum <=999)
            {
                return "     " + inputnum.ToString();
            }
            if (inputnum <=9999)
            {
                return "    " + inputnum.ToString();
            }
            if (inputnum <=99999)
            {
                return "   " + inputnum.ToString();
            }
            if (inputnum == 99999999 || inputnum == 9999999 || inputnum == 999999)
            {
                return "99999999";
            }
            return "       1";
        }

        public static string GetOldFormatDepNum(int inputnum)
        {
            if (inputnum <= 9)
            {
                return "  " + inputnum.ToString();
            }
            if (inputnum <= 99)
            {
                return " " + inputnum.ToString();
            }
            if (inputnum <= 999)
            {
                return " ";
            }
            return " ";
        }


        // Расчеты для записей нормокарты 

        public static double CalculateCoeffCTN(double ItemPayNorm, double PrepareTimeNorm, double ItemCTN,
                                               double PrepareTimeCTN, int CountPerProduct)
        {
            if (ItemPayNorm == 0 ||  ItemCTN == 0 )
                return 0;
            //return Math.Round(((ItemPayNorm * CountPerProduct + PrepareTimeNorm) / (ItemCTN * CountPerProduct + PrepareTimeCTN)), 4); Старая формула для росчета Повішаещего коєфициєнта
            return  Math.Round((ItemPayNorm/ItemCTN), TrudoyomkostSettings.RoundNum);
        }

        public static double CalculateValuation(double ItemPayNorm, double HourCost)
        {
            return Math.Round((ItemPayNorm * HourCost), TrudoyomkostSettings.RoundNum);
        }

        public static double CalculateValPrepareTime(double PrepareTimePayNorm, double HourCost)
        {
            return Math.Round((PrepareTimePayNorm * HourCost), TrudoyomkostSettings.RoundNum);
        }

        public static double ReCalcItemPayNorm(double LoverCoeff, double ItemPayNorm)
        {
            return ItemPayNorm*LoverCoeff; 
        }
    }
}
