

namespace Alphabet.Tests
{
    using System;

    using global::Alphabet;

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
            Assert.That(() => this.myAlph.NumberToString(-1),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Число вне диапазона!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.NumberToString(-1));

            Assert.That(() => this.myAlph.NumberToString(4432676798592),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Число вне диапазона!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.NumberToString(4432676798592));
        }

        /// <summary>
        /// Тесты на числа первого диапазона.
        /// </summary>
        [Test]
        public void FirstRangeTests()
        {
            string start = this.myAlph.NumberToString(0);
            Assert.That(start, Is.EqualTo("A"));

            string rus = this.myAlph.NumberToString(69);
            Assert.That(rus, Is.EqualTo("р"));

            string end = this.myAlph.NumberToString(127);
            Assert.That(end, Is.EqualTo("9"));
        }

        /// <summary>
        /// Тесты на числа второго диапазона.
        /// </summary>
        [Test]
        public void SecondRangeTests()
        {
            string start = this.myAlph.NumberToString(128);
            Assert.That(start, Is.EqualTo("AA"));

            string sec = this.myAlph.NumberToString(130);
            Assert.That(sec, Is.EqualTo("AC"));

            string rus = this.myAlph.NumberToString(15221);
            Assert.That(rus, Is.EqualTo("ЯЯ"));

            string end = this.myAlph.NumberToString(16511);
            Assert.That(end, Is.EqualTo("99"));
        }

        /// <summary>
        /// Тесты на числа третьего диапазона.
        /// </summary>
        [Test]
        public void ThirdRangeTests()
        {
            string start = this.myAlph.NumberToString(16512);
            Assert.That(start, Is.EqualTo("AAA"));

            string rus = this.myAlph.NumberToString(1072438);
            Assert.That(rus, Is.EqualTo("лев"));

            string end = this.myAlph.NumberToString(2113663);
            Assert.That(end, Is.EqualTo("999"));
        }

        /// <summary>
        /// Тесты на числа четвертого диапазона.
        /// </summary>
        [Test]
        public void FourthRangeTests()
        {
            string start = this.myAlph.NumberToString(2113664);
            Assert.That(start, Is.EqualTo("AAAA"));

            string rus = this.myAlph.NumberToString(139535161);
            Assert.That(rus, Is.EqualTo("море"));

            string end = this.myAlph.NumberToString(270549119);
            Assert.That(end, Is.EqualTo("9999"));
        }

        /// <summary>
        /// Тесты на числа пятого диапазона.
        /// </summary>
        [Test]
        public void FifrthRangeTests()
        {
            string start = this.myAlph.NumberToString(270549120);
            Assert.That(start, Is.EqualTo("AAAAA"));

            string rus = this.myAlph.NumberToString(26255914087);
            Assert.That(rus, Is.EqualTo("КРОСС"));

            string end = this.myAlph.NumberToString(34630287487);
            Assert.That(end, Is.EqualTo("99999"));
        }

        /// <summary>
        /// Тесты на числа шестого диапазона.
        /// </summary>
        [Test]
        public void SixthRangeTests()
        {
            string start = this.myAlph.NumberToString(34630287488);
            Assert.That(start, Is.EqualTo("AAAAAA"));

            string rus = this.myAlph.NumberToString(3523067371319);
            Assert.That(rus, Is.EqualTo("Подвиг"));

            string end = this.myAlph.NumberToString(4432676798591);
            Assert.That(end, Is.EqualTo("999999"));
        }
    }
}