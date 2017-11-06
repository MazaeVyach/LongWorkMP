
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
        /// Метод вычисления начало диапазона(сдвига) для строки длины count.
        /// </summary>
        /// <param name="count">
        /// Длина строки.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long GetShiftRange(int count)
        {
            int i; // переменная цикла
            long shift = 0; // результат
            for (i = count - 1; i != 0; --i)
            {
                shift += (long)Math.Pow(ActiveAlphabet.Length, i);
            }

            return shift;
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
            long lastCorrectNumber = this.GetShiftRange(7) - 1;

            // в случае выхода за границы 
            if (number < 0 || number >= lastCorrectNumber) throw new ApplicationException("Число вне диапазона!");

            if (number >= this.GetShiftRange(1) && number <= this.GetShiftRange(2) - 1) return 1;

            if (number >= this.GetShiftRange(2) && number <= this.GetShiftRange(3) - 1) return 2;

            if (number >= this.GetShiftRange(3) && number <= this.GetShiftRange(4) - 1) return 3;

            if (number >= this.GetShiftRange(4) && number <= this.GetShiftRange(5) - 1) return 4;

            if (number >= this.GetShiftRange(5) && number <= this.GetShiftRange(6) - 1) return 5;

            if (number >= this.GetShiftRange(6) && number <= lastCorrectNumber - 1) return 6;

            return 0; // в случае выхода за границы
        }

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
                if (ActiveAlphabet.IndexOf(t) == -1)
                    throw new ApplicationException("Символы слова не из действующего алфавита!");
            }

            // начало диапазона (сдвиг) для строки длиной word.Length
            long shift = this.GetShiftRange(word.Length);

            number += shift;

            // вычисление числа
            for (int i = 0; i < word.Length; ++i)
            {
                number += (long)(ActiveAlphabet.IndexOf(word[i]) * Math.Pow(
                                     ActiveAlphabet.Length,
                                     word.Length - i - 1));
            }
            return number;
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
            string result = string.Empty; // результирующая строка
            int range = this.DefineRange(number); // определение диапазона числа

            // вычитаем из числа смещение диапазона для получения исходного числа
            // т.к. диапазон равен числу символов в слове
            number -= this.GetShiftRange(range);

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
            }
            return result;
        }
    }
}
