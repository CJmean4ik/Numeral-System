using System;
using System.Linq;
using System.Collections.Generic;

namespace numeral_system
{
    class NumeralSystemConverter
    {
        
        private static readonly Dictionary<int, string> _HexadecimalAlphabet = new Dictionary<int, string>()
        { 
            [10] = "A",
            [11] = "B",
            [12] = "C",
            [13] = "D",
            [14] = "E",
            [15] = "F"
        };

        /// <summary>
        /// Переводит двоичного числа в десятичное 
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в десятичном виде</returns>
        public static int BinaryToDecimal(string currentNumb)
        {
            var binaryArr = currentNumb.ToArray();

            int resultDecimal = 0;
            for (int i = 0, j = binaryArr.Length - 1; i < binaryArr.Length; i++)
            {
                resultDecimal += int.TryParse(binaryArr[i].ToString(), out int result) == true
                                    && result == 0
                                    || result == 1
                                    ? result * (int)Math.Pow(2, j--) : default;
            }
            return resultDecimal;
        }

        /// <summary>
        /// Переводит двоичнго числа в восьмеричное 
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в восьмеричном виде</returns>
        public static int BinaryToOctal(string currentNumb)
        {          
           return DecimalToOctal(BinaryToDecimal(currentNumb));
        }

        /// <summary>
        /// Переводит двоичного числа в шестнадцатеричное 
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в шестнадцатеричном виде</returns>
        public static string BinaryToHexadecimal(string currentNumb)
        {            
                return ConditionDivisionSixteen(BinaryToDecimal(currentNumb));
           
        }

        /// <summary>
/// Первод восьмеричного числа в десятичное
/// </summary>
/// <param name="currentNumb"></param>
/// <returns>значение в десятичном виде</returns>
        public static int OctalToDecimal(int currentNumb)
        {
            var octalArr = currentNumb.ToString()
                .Select(x => x)
                .ToArray();
            int resultOctal = 0;
            for (int i = 0, j = octalArr.Length - 1; i < octalArr.Length; i++)
            {
                resultOctal += int.TryParse(octalArr[i].ToString(), out int result) == true
                                    && result >= 0
                                    && result < 8
                                    ? result * (int)Math.Pow(8, j--) : default;
            }
            return resultOctal;
        }

        /// <summary>
        /// Перевод восьмеричного числа в двоичное
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в двоичном виде</returns>
        public static string OctalToBinary(int currentNumb)
        {
           return DecimalToBinary(OctalToDecimal(currentNumb)).ToString();
        }

        /// <summary>
        /// Перевод восьмеричного числа в шестнадцатиричное
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>возврат в  шестнадцатиричном виде </returns>
        public static string OctalToHexadecimal(int currentNumb)
        {
            int decimalNumb = OctalToDecimal(currentNumb);

            return ConditionDivisionSixteen(decimalNumb);
        }

        /// <summary>
        /// Перевод десятичного числа в двоичный
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в двоичном виде</returns>
        public static string DecimalToBinary(int currentNumb)
        {
            string binarySrting = string.Empty; 
            do
            {
                binarySrting += currentNumb % 2;             
                if ((currentNumb /= 2) == 1)
                {
                    binarySrting += currentNumb;
                    break;
                }
            } while (true);

            return new String(binarySrting.Reverse().ToArray());
        }            

        /// <summary>
        /// Перевод десятичного числа в восьмеричное 
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в восьмеричном виде</returns>
        public static int DecimalToOctal(int currentNumb)
        {
            string octalNumb = string.Empty;
            do
            {
                octalNumb += currentNumb % 8;
                if ((currentNumb /= 8) == 0) break;                            
                
            } while (true);
     
            return int.Parse(new String(octalNumb.Reverse().ToArray()));
        }

        /// <summary>
        /// Перевод десятичного числа в шестнадцатиричное
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в шестнадцатиричном виде</returns>
        public static string DecimalToHexadecimal(int currentNumb)
        {
            return ConditionDivisionSixteen(currentNumb);

        }

       
        /// <summary>
        /// Перевод шестнадцатиричного числа в двоичное
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в двоичном виде</returns>
        public static string HexadecimalToBinary(string currentNumb)
        {
            int decimalNumb = HexadecimalToDecimal(currentNumb);

            return DecimalToBinary(decimalNumb);
        }

        /// <summary>
        /// Перевод шестнадцатиричного числа в восьмеричное
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в восьмеричном виде</returns>
        public static int HexadecimalToOctal(string currentNumb)
        {
            int decimalNumb = HexadecimalToDecimal(currentNumb);

            return DecimalToOctal(decimalNumb);
        }

        /// <summary>
        /// Перевод шестнадцатиричного числа в двоичное
        /// </summary>
        /// <param name="currentNumb"></param>
        /// <returns>значение в десятичном виде</returns>
        public static int HexadecimalToDecimal(string currentNumb)
        {
            var decimalArr = currentNumb.ToArray();
            int decimalNumb = 0;
            for (int i = 0, j = decimalArr.Length - 1; i < decimalArr.Length; i++)
            {
                if (_HexadecimalAlphabet.ContainsValue(decimalArr[i].ToString()))
                    decimalNumb += GetHexadecimalNumber(decimalArr[i]) * (int)Math.Pow(16, j--);
                else
                {
                    decimalNumb += int.TryParse(decimalArr[i].ToString(), out int hexadecNumb) == true
                                    && hexadecNumb >= 0
                                    && hexadecNumb <= 15
                                    ? hexadecNumb * (int)Math.Pow(16, j--) : default;
                }
            }
            return decimalNumb;
        }

        /// <summary>
        /// Выполняет деление числа на 16 и возвращает остаток от деление в виде двоичного кода
        /// </summary>
        /// <param name="compared"></param>
        /// <returns>значение в двоичном виде</returns>
        private static string ConditionDivisionSixteen(int compared)
        {
            int MOD = 0;
            string octalNumb = string.Empty;
            do
            {
                MOD = compared % 16;
                if (MOD > 9 && MOD <= 15)
                    octalNumb += GetHexadecimalChar(MOD);
                else octalNumb += MOD;
                if ((compared /= 16) == 0) break;

            } while (true);
            return new String(octalNumb.Reverse().ToArray());
        }

        /// <summary>
        /// Возвращает значение из Словаря по ключу в виде буквы из шестнадцатиричной СС
        /// </summary>
        /// <param name="numbKey"></param>
        /// <returns>Буква из словаря</returns>
        private static string GetHexadecimalChar(int numbKey)
        {
            return _HexadecimalAlphabet[numbKey];
        }

        /// <summary>
        /// Возврат значение ключа по значению буквы 
        /// </summary>
        /// <param name="symbol"></param>
        /// <returns>значение ключа</returns>
        private static int GetHexadecimalNumber(char symbol)
        {
            foreach (var item in _HexadecimalAlphabet)
            {
                if (item.Value == symbol.ToString())
                    return item.Key;                
            }
            return 0;
        }

    }
}
