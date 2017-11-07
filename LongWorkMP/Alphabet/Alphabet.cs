
namespace Alphabet
{
    using System;

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
            if (count <= 0)
                throw new ApplicationException("Длина строки должна быть положительна!");

            long shift = 0; // результат

            for (int i = 1; i < count; ++i)
                shift += (long)Math.Pow(ActiveAlphabet.Length, i);

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
            int range = 0;
            long lastCorrectNumber = this.GetShiftRange(7) - 1;

            // в случае выхода за границы 
            if (number < 0 || number > lastCorrectNumber) throw new ApplicationException("Число вне диапазона!");

            if (number >= this.GetShiftRange(1) && number <= this.GetShiftRange(2) - 1) range =  1;

            if (number >= this.GetShiftRange(2) && number <= this.GetShiftRange(3) - 1) range =  2;

            if (number >= this.GetShiftRange(3) && number <= this.GetShiftRange(4) - 1) range =  3;

            if (number >= this.GetShiftRange(4) && number <= this.GetShiftRange(5) - 1) range =  4;

            if (number >= this.GetShiftRange(5) && number <= this.GetShiftRange(6) - 1) range = 5;

            if (number >= this.GetShiftRange(6) && number <= lastCorrectNumber ) range = 6;

            return range; 
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
                throw new ApplicationException("Неверная длина слова!");

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
            int range = this.DefineRange(number); // определение диапазона числа

            char[] sBuilder = new char[range];

            // вычитаем из числа смещение диапазона для получения исходного числа
            // т.к. диапазон равен числу символов в слове
            number -= this.GetShiftRange(range);
          
            for (int i = range - 1; i >= 0; i--)
            {
                sBuilder[i] = ActiveAlphabet[(int)(number % ActiveAlphabet.Length)];
                number = number / ActiveAlphabet.Length;
            }

            return new string(sBuilder);
            }
        }
 }
