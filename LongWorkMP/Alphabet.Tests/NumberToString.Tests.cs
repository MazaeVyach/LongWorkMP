

namespace Alphabet.Tests
{
    using System;

    using global::Alphabet;

    using NUnit.Framework;

    /// <summary>
    /// Тесты метода NumberToString().
    /// </summary>
    [TestFixture]
    public class NumberToStringTest
    {
        /// <summary>
        /// Создание алфавита
        /// </summary>
        private Alphabet myAlph = new Alphabet();

        /// <summary>
        /// Тесты на недопустимые числа.
        /// </summary>
        [Test]
        public void MissRangeTests()
        {
            Assert.That(() => myAlph.NumberToString(-1),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Число вне диапазона!"));

            Assert.Throws<ApplicationException>(() => myAlph.NumberToString(-1));

            Assert.That(() => myAlph.NumberToString(4432676798592),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Число вне диапазона!"));

            Assert.Throws<ApplicationException>(() => myAlph.NumberToString(4432676798592));
        }

        /// <summary>
        /// Тесты на числа первого диапазона.
        /// </summary>
        [Test]
        public void FirstRangeTests()
        {
            string start = myAlph.NumberToString(0);
            Assert.That(start, Is.EqualTo("A"));

            string rus = myAlph.NumberToString(69);
            Assert.That(rus, Is.EqualTo("р"));

            string end = myAlph.NumberToString(127);
            Assert.That(end, Is.EqualTo("9"));
        }

        /// <summary>
        /// Тесты на числа второго диапазона.
        /// </summary>
        [Test]
        public void SecondRangeTests()
        {
            string start = myAlph.NumberToString(128);
            Assert.That(start, Is.EqualTo("AA"));

            string sec = myAlph.NumberToString(130);
            Assert.That(sec, Is.EqualTo("AC"));

            string rus = myAlph.NumberToString(15221);
            Assert.That(rus, Is.EqualTo("ЯЯ"));

            string end = myAlph.NumberToString(16511);
            Assert.That(end, Is.EqualTo("99"));
        }

        /// <summary>
        /// Тесты на числа третьего диапазона.
        /// </summary>
        [Test]
        public void ThirdRangeTests()
        {
            string start = myAlph.NumberToString(16512);
            Assert.That(start, Is.EqualTo("AAA"));

            string rus = myAlph.NumberToString(1072438);
            Assert.That(rus, Is.EqualTo("лев"));

            string end = myAlph.NumberToString(2113663);
            Assert.That(end, Is.EqualTo("999"));
        }

        /// <summary>
        /// Тесты на числа четвертого диапазона.
        /// </summary>
        [Test]
        public void FourthRangeTests()
        {
            string start = myAlph.NumberToString(2113664);
            Assert.That(start, Is.EqualTo("AAAA"));

            string rus = myAlph.NumberToString(139535161);
            Assert.That(rus, Is.EqualTo("море"));

            string end = myAlph.NumberToString(270549119);
            Assert.That(end, Is.EqualTo("9999"));
        }

        /// <summary>
        /// Тесты на числа пятого диапазона.
        /// </summary>
        [Test]
        public void FifrthRangeTests()
        {
            string start = myAlph.NumberToString(270549120);
            Assert.That(start, Is.EqualTo("AAAAA"));

            string rus = myAlph.NumberToString(26255914087);
            Assert.That(rus, Is.EqualTo("КРОСС"));

            string end = myAlph.NumberToString(34630287487);
            Assert.That(end, Is.EqualTo("99999"));
        }

        /// <summary>
        /// Тесты на числа шестого диапазона.
        /// </summary>
        [Test]
        public void SixthRangeTests()
        {
            string start = myAlph.NumberToString(34630287488);
            Assert.That(start, Is.EqualTo("AAAAAA"));

            string rus = myAlph.NumberToString(3523067371319);
            Assert.That(rus, Is.EqualTo("Подвиг"));

            string end = myAlph.NumberToString(4432676798591);
            Assert.That(end, Is.EqualTo("999999"));
        }
    }
}