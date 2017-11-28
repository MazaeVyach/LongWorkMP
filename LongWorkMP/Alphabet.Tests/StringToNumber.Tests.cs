
namespace Alphabet.Tests
{
    using System;

    using global::Alphabet;

    using NUnit.Framework;

    /// <summary>
    /// Тесты метода StringToNumber()
    /// </summary>
    [TestFixture]
    public class StringToNumberTests
    {
        /// <summary>
        /// Создание алфавита
        /// </summary>
        private Alphabet myAlph = new Alphabet();

        /// <summary>
        /// Тесты на длину приходящего слова.
        /// </summary>
        [Test]
        public void LengthTests()
        {
            Assert.That(() => myAlph.StringToNumber(string.Empty),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Неверная длина слова!"));
            
            Assert.Throws<ApplicationException>(() => myAlph.StringToNumber(string.Empty));

            Assert.That(() => myAlph.StringToNumber("AAAAAAA"),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Неверная длина слова!"));

            Assert.Throws<ApplicationException>(() => myAlph.StringToNumber("AAAAAAA"));
        }

        /// <summary>
        /// Тесты на принадлежность символов строки действующему алфавиту.
        /// </summary>
        [Test]
        public void AlphabetTests()
        {
            Assert.That(() => myAlph.StringToNumber("侍"),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Символы слова не из действующего алфавита!"));

            Assert.Throws<ApplicationException>(() => myAlph.StringToNumber("侍"));

            Assert.That(() => myAlph.StringToNumber("können"),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Символы слова не из действующего алфавита!"));

            Assert.Throws<ApplicationException>(() => myAlph.StringToNumber("können"));
        }

        /// <summary>
        /// Тесты для односимвольных строк
        /// </summary>
        [Test]
        public void SingleCharacterTests()
        {
            long start = myAlph.StringToNumber("A");
            Assert.That(start, Is.EqualTo(0));

            long rus = myAlph.StringToNumber("Я");
            Assert.That(rus, Is.EqualTo(117));

            long end = myAlph.StringToNumber("9");
            Assert.That(end, Is.EqualTo(127));
        }

        /// <summary>
        /// Тесты для двухсимвольных строк.
        /// </summary>
        [Test]
        public void TwoCharacterTests()
        {
            long start = myAlph.StringToNumber("AA");
            Assert.That(start, Is.EqualTo(128));

            long sec = myAlph.StringToNumber("AC");
            Assert.That(sec, Is.EqualTo(130));

            long thrd = myAlph.StringToNumber("CD");
            Assert.That(thrd, Is.EqualTo(387));

            long rus = myAlph.StringToNumber("ЯЯ");
            Assert.That(rus, Is.EqualTo(15221));

            long end = myAlph.StringToNumber("99");
            Assert.That(end, Is.EqualTo(16511));
        }

        /// <summary>
        /// Тесты для трёхсимвольных строк.
        /// </summary>
        [Test]
        public void ThreeCharacterTest()
        {
            long start = myAlph.StringToNumber("AAA");
            Assert.That(start, Is.EqualTo(16512));

            long rus = myAlph.StringToNumber("лев");
            Assert.That(rus, Is.EqualTo(1072438));

            long end = myAlph.StringToNumber("999");
            Assert.That(end, Is.EqualTo(2113663));
        }

        /// <summary>
        /// Тесты для четырёхсимвольных строк.
        /// </summary>
        [Test]
        public void FourCharacterTest()
        {
            long start = myAlph.StringToNumber("AAAA");
            Assert.That(start, Is.EqualTo(2113664));

            long eng = myAlph.StringToNumber("test");
            Assert.That(eng, Is.EqualTo(96982701));

            long rus = myAlph.StringToNumber("море");
            Assert.That(rus, Is.EqualTo(139535161));

            long end = myAlph.StringToNumber("9999");
            Assert.That(end, Is.EqualTo(270549119));
        }

        /// <summary>
        /// Тесты для пятисимвольных строк.
        /// </summary>
        [Test]
        public void FiveCharacterTest()
        {
            long start = myAlph.StringToNumber("AAAAA");
            Assert.That(start, Is.EqualTo(270549120));

            long rus = myAlph.StringToNumber("КРОСС");
            Assert.That(rus, Is.EqualTo(26255914087));

            long end = myAlph.StringToNumber("99999");
            Assert.That(end, Is.EqualTo(34630287487));
        }

        /// <summary>
        /// Тесты для шестисимвольных строк.
        /// </summary>
        [Test]
        public void SixCharacterTest()
        {
            long start = myAlph.StringToNumber("AAAAAA");
            Assert.That(start, Is.EqualTo(34630287488));

            long rus = myAlph.StringToNumber("Подвиг");
            Assert.That(rus, Is.EqualTo(3523067371319));

            long end = myAlph.StringToNumber("999999");
            Assert.That(end, Is.EqualTo(4432676798591));
        }
    }
}
