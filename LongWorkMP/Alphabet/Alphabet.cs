
namespace Alphabet
{
    using System;
    using System.Collections.Generic;

    /// <summary>
    /// Класс Алфавит.
    /// </summary>
    public class Alphabet
    {
        /// <summary>
        /// Действующий алфавит в виде строки.
        /// </summary>
        private const string ActiveAlphabet =
        "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyzабвгдеёжзийклмнопрстуфхцчшщъыьэюяАБВГДЕЁЖЗИЙКЛМНОПРСТУФХЦЧШЩЪЫЬЭЮЯ0123456789";

        /// <summary>
        /// Преобразование из строки в число.
        /// </summary>
        /// <param name="word">
        /// Слово, которое нужно преобразовать в число.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long StringToNumber(string word)
        {
            long number = new long(); // Число на выходе

            // Если слово состоит из символов не из алфавита или слово длиньше 6 символов
            if (word.Length > 6 || word.Length <= 0)
            {
                throw new ApplicationException("Неверная длина слова!");
            }

            foreach (char t in word)
            {
                if (ActiveAlphabet.IndexOf(t) < 0 || ActiveAlphabet.IndexOf(t) > ActiveAlphabet.Length)
                    throw new ApplicationException("Символы слова не из действующего алфавита!");
            }

            switch (word.Length)
            {
                case 1:
                    {
                        number = ActiveAlphabet.IndexOf(word, StringComparison.Ordinal);
                        break;
                    }

                case 2:
                    {
                        // начало диапазона(сдвиг) для двухсимвольной строки
                        long rangeBegining2 = ActiveAlphabet.Length;

                        for (int i = 0; i < word.Length; ++i)
                        {
                            number += (long)(ActiveAlphabet.IndexOf(word[i]) * Math.Pow(
                                                 ActiveAlphabet.Length,
                                                 word.Length - i - 1));
                        }

                        number += rangeBegining2;
                        break;
                    }

                case 3:
                    {
                        // начало диапазона(сдвиг) для трехсимвольной строки
                        long rangeBegining3 = (long)(Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length);

                        for (int i = 0; i < word.Length; ++i)
                        {
                            number += (long)(ActiveAlphabet.IndexOf(word[i]) * Math.Pow(
                                                 ActiveAlphabet.Length,
                                                 word.Length - i - 1));
                        }

                        number += rangeBegining3;
                        break;
                    }

                case 4:
                    {
                        // начало диапазона(сдвиг) для четырехсимвольной строки
                        long rangeBegining4 = (long)(Math.Pow(ActiveAlphabet.Length, 3)
                                                     + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length);

                        for (int i = 0; i < word.Length; ++i)
                        {
                            number += (long)(ActiveAlphabet.IndexOf(word[i]) * Math.Pow(
                                                 ActiveAlphabet.Length,
                                                 word.Length - i - 1));
                        }

                        number += rangeBegining4;
                        break;
                    }

                case 5:
                    {
                        // начало диапазона(сдвиг) для пятисимвольной строки
                        long rangeBegining5 = (long)(Math.Pow(ActiveAlphabet.Length, 4)
                                                     + Math.Pow(ActiveAlphabet.Length, 3)
                                                     + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length);

                        for (int i = 0; i < word.Length; ++i)
                        {
                            number += (long)(ActiveAlphabet.IndexOf(word[i]) * Math.Pow(
                                                 ActiveAlphabet.Length,
                                                 word.Length - i - 1));
                        }

                        number += rangeBegining5;
                        break;
                    }

                case 6:
                    {
                        // начало диапазона(сдвиг) для шестисимвольной строки
                        long rangeBegining6 = (long)(Math.Pow(ActiveAlphabet.Length, 5)
                                                     + Math.Pow(ActiveAlphabet.Length, 4)
                                                     + Math.Pow(ActiveAlphabet.Length, 3)
                                                     + Math.Pow(ActiveAlphabet.Length, 2)
                                                     + ActiveAlphabet.Length);

                        for (int i = 0; i < word.Length; ++i)
                        {
                            number += (long)(ActiveAlphabet.IndexOf(word[i]) * Math.Pow(
                                                 ActiveAlphabet.Length,
                                                 word.Length - i - 1));
                        }

                        number += rangeBegining6;
                        break;
                    }
            }

            return number;
        }

        /// <summary>
        /// Метод определения диапазона значений по числу.
        /// </summary>
        /// <param name="number">
        /// Число, для которого нужно определить диапазон.
        /// </param>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int DefineRange(long number)
        {
            // диапазон, которому принадлежит число
            // в случае выхода за границы = 0
            int range = 0;
            if (number < 0 || number >= Math.Pow(ActiveAlphabet.Length, 6) + Math.Pow(ActiveAlphabet.Length, 5)
                + Math.Pow(ActiveAlphabet.Length, 4) + Math.Pow(ActiveAlphabet.Length, 3)
                + Math.Pow(ActiveAlphabet.Length, 2)
                + ActiveAlphabet.Length) throw new ApplicationException("Число вне диапазона!");

            if (number >= 0 && number <= ActiveAlphabet.Length - 1)
            {
                range = 1;
            }
            if (number >= ActiveAlphabet.Length
                && number <= Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length - 1)
            {
                range = 2;
            }

            if (number >= Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length && number
                <= Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length - 1)
            {
                range = 3;
            }

            if (number >= Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length && number
                <= Math.Pow(ActiveAlphabet.Length, 4) + Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length - 1)
            {
                range = 4;
            }

            if (number >= Math.Pow(ActiveAlphabet.Length, 4) + Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length && number
                <= Math.Pow(ActiveAlphabet.Length, 5) + Math.Pow(ActiveAlphabet.Length, 4) + Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length - 1)
            {
                range = 5;
            }

            if (number >= Math.Pow(ActiveAlphabet.Length, 5) + Math.Pow(ActiveAlphabet.Length, 4) + Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length && number
                <= Math.Pow(ActiveAlphabet.Length, 6) + Math.Pow(ActiveAlphabet.Length, 5) + Math.Pow(ActiveAlphabet.Length, 4) + Math.Pow(ActiveAlphabet.Length, 3) + Math.Pow(ActiveAlphabet.Length, 2) + ActiveAlphabet.Length - 1)
            {
                range = 6;
            }

            return range;
        }

        /// <summary>
        /// Метод перевода из числа в строку.
        /// </summary>
        /// <param name="number">
        /// Число, которое нужно преобразовать в строку.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string NumberToString(long number)
        {
            
            // создание соответсвия диапазонам и сдвигам (начальным значениям)
            Dictionary<int, long> offset = new Dictionary<int, long>
                                               {
                                                   { 1, 0 },
                                                   { 2, ActiveAlphabet.Length },
                                                   {
                                                       3,
                                                       (long)(Math.Pow(ActiveAlphabet.Length, 2)
                                                              + ActiveAlphabet.Length)
                                                   },
                                                   {
                                                       4,
                                                       (long)(Math.Pow(ActiveAlphabet.Length, 3)
                                                              + Math.Pow(ActiveAlphabet.Length, 2)
                                                              + ActiveAlphabet.Length)
                                                   },
                                                   {
                                                       5,
                                                       (long)(Math.Pow(ActiveAlphabet.Length, 4)
                                                              + Math.Pow(ActiveAlphabet.Length, 3)
                                                              + Math.Pow(ActiveAlphabet.Length, 2)
                                                              + ActiveAlphabet.Length)
                                                   },
                                                   {
                                                       6,
                                                       (long)(Math.Pow(ActiveAlphabet.Length, 5)
                                                              + Math.Pow(ActiveAlphabet.Length, 4)
                                                              + Math.Pow(ActiveAlphabet.Length, 3)
                                                              + Math.Pow(ActiveAlphabet.Length, 2)
                                                              + ActiveAlphabet.Length)
                                                   }
                                               };


            int range = this.DefineRange(number); // определение диапазона числа
            string result = string.Empty; // результирующая строка
            
            if (range == 0)
                throw new Exception("Недопустимое число!");

            if (range != 0)
            {
                // вычитаем из числа смещение диапазона для получения исходного числа
                number -= offset[range];
                

                switch (range)
                {
                    case 1:
                    {
                        int index = (int)number;
                        result = ActiveAlphabet[index].ToString();
                        break;
                    }

                    case 2:
                    {
                        // остаток от деления (самая правая буква)
                        int remainder = (int)(number % ActiveAlphabet.Length);

                        // буква левее
                        int quotient = (int)((number - remainder) / ActiveAlphabet.Length);
                        
                        result += ActiveAlphabet[quotient];
                        result += ActiveAlphabet[remainder];
                        break;
                    }

                    case 3:
                    {
                            // правая буква
                        int remainder = (int)(number % ActiveAlphabet.Length);

                        result += ActiveAlphabet[remainder];
                        break;
                    }
                }
            }

            return result;
        }
    }
}
