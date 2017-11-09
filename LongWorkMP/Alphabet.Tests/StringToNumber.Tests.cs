
namespace Alphabet.Tests
{
    using System;

    using global::Alphabet;

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
            Assert.That(() => this.myAlph.StringToNumber(string.Empty),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Неверная длина слова!"));
            
            Assert.Throws<ApplicationException>(() => this.myAlph.StringToNumber(string.Empty));

            Assert.That(() => this.myAlph.StringToNumber("AAAAAAA"),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Неверная длина слова!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.StringToNumber("AAAAAAA"));
        }

        /// <summary>
        /// Тесты на принадлежность символов строки действующему алфавиту.
        /// </summary>
        [Test]
        public void AlphabetTests()
        {
            Assert.That(() => this.myAlph.StringToNumber("侍"),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Символы слова не из действующего алфавита!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.StringToNumber("侍"));

            Assert.That(() => this.myAlph.StringToNumber("können"),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Символы слова не из действующего алфавита!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.StringToNumber("können"));
        }

        /// <summary>
        /// Тесты для односимвольных строк
        /// </summary>
        [Test]
        public void SingleCharacterTests()
        {
            long start = this.myAlph.StringToNumber("A");
            Assert.That(start, Is.EqualTo(0));

            long rus = this.myAlph.StringToNumber("Я");
            Assert.That(rus, Is.EqualTo(117));

            long end = this.myAlph.StringToNumber("9");
            Assert.That(end, Is.EqualTo(127));
        }

        /// <summary>
        /// Тесты для двухсимвольных строк.
        /// </summary>
        [Test]
        public void TwoCharacterTests()
        {
            long start = this.myAlph.StringToNumber("AA");
            Assert.That(start, Is.EqualTo(128));

            long sec = this.myAlph.StringToNumber("AC");
            Assert.That(sec, Is.EqualTo(130));

            long thrd = this.myAlph.StringToNumber("CD");
            Assert.That(thrd, Is.EqualTo(387));

            long rus = this.myAlph.StringToNumber("ЯЯ");
            Assert.That(rus, Is.EqualTo(15221));

            long end = this.myAlph.StringToNumber("99");
            Assert.That(end, Is.EqualTo(16511));
        }

        /// <summary>
        /// Тесты для трёхсимвольных строк.
        /// </summary>
        [Test]
        public void ThreeCharacterTest()
        {
            long start = this.myAlph.StringToNumber("AAA");
            Assert.That(start, Is.EqualTo(16512));

            long rus = this.myAlph.StringToNumber("лев");
            Assert.That(rus, Is.EqualTo(1072438));

            long end = this.myAlph.StringToNumber("999");
            Assert.That(end, Is.EqualTo(2113663));
        }

        /// <summary>
        /// Тесты для четырёхсимвольных строк.
        /// </summary>
        [Test]
        public void FourCharacterTest()
        {
            long start = this.myAlph.StringToNumber("AAAA");
            Assert.That(start, Is.EqualTo(2113664));

            long eng = this.myAlph.StringToNumber("test");
            Assert.That(eng, Is.EqualTo(96982701));

            long rus = this.myAlph.StringToNumber("море");
            Assert.That(rus, Is.EqualTo(139535161));

            long end = this.myAlph.StringToNumber("9999");
            Assert.That(end, Is.EqualTo(270549119));
        }

        /// <summary>
        /// Тесты для пятисимвольных строк.
        /// </summary>
        [Test]
        public void FiveCharacterTest()
        {
            long start = this.myAlph.StringToNumber("AAAAA");
            Assert.That(start, Is.EqualTo(270549120));

            long rus = this.myAlph.StringToNumber("КРОСС");
            Assert.That(rus, Is.EqualTo(26255914087));

            long end = this.myAlph.StringToNumber("99999");
            Assert.That(end, Is.EqualTo(34630287487));
        }

        /// <summary>
        /// Тесты для шестисимвольных строк.
        /// </summary>
        [Test]
        public void SixCharacterTest()
        {
            long start = this.myAlph.StringToNumber("AAAAAA");
            Assert.That(start, Is.EqualTo(34630287488));

            long rus = this.myAlph.StringToNumber("Подвиг");
            Assert.That(rus, Is.EqualTo(3523067371319));

            long end = this.myAlph.StringToNumber("999999");
            Assert.That(end, Is.EqualTo(4432676798591));
        }
    }
}
