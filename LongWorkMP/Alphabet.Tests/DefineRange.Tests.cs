
namespace Alphabet.Tests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Тесты метода WhichRange().
    /// </summary>
    [TestFixture]
    public class DefineRangeTests
    {
        /// <summary>
        /// Создание алфавита.
        /// </summary>
        private Alphabet myAlph = new Alphabet();

        /// <summary>
        /// Тесты для недопустимых значений.
        /// </summary>
        [Test]
        public void MissRange()
        {
            Assert.That(() => this.myAlph.DefineRange(-15),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Число вне диапазона!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.DefineRange(-15));


            Assert.That(() => this.myAlph.DefineRange(4432676798592),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Число вне диапазона!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.DefineRange(4432676798592));

        }

        /// <summary>
        /// Тесты первого диапазона.
        /// </summary>
        [Test]
        public void TestFirstRange()
        {
            int range = this.myAlph.DefineRange(126);
            Assert.That(range, Is.EqualTo(1));
        }

        /// <summary>
        /// Тесты второго диапазона.
        /// </summary>
        [Test]
        public void TestSecondRange()
        {
            int range = this.myAlph.DefineRange(16511);
            Assert.That(range, Is.EqualTo(2));
        }

        /// <summary>
        /// Тесты третьего диапазона.
        /// </summary>
        [Test]
        public void TestThirdRange()
        {
            int range = this.myAlph.DefineRange(16514);
            Assert.That(range, Is.EqualTo(3));
        }

        /// <summary>
        /// Тесты четвертого диапазона.
        /// </summary>
        [Test]
        public void TestFourthRange()
        {
            int range = this.myAlph.DefineRange(270549110);
            Assert.That(range, Is.EqualTo(4));
        }

        /// <summary>
        /// Тесты пятого диапазона.
        /// </summary>
        [Test]
        public void TestFifthRange()
        {
            int range = this.myAlph.DefineRange(270549130);
            Assert.That(range, Is.EqualTo(5));
        }

        /// <summary>
        /// Тесты шестого диапазона.
        /// </summary>
        [Test]
        public void TestSixthRange()
        {
            int range = this.myAlph.DefineRange(34630287489);
            Assert.That(range, Is.EqualTo(6));
        }
    }
}
