
namespace Alphabet.Tests
{
    using System;

    using global::Alphabet;

    /// <summary>
    /// Тесты метода WhichRange().
    /// </summary>
    [TestFixture]
    public class GetShiftRangeTests
    {
        /// <summary>
        /// Создание алфавита.
        /// </summary>
        private Alphabet myAlph = new Alphabet();

        /// <summary>
        /// Тесты для недопустимых значений.
        /// </summary>
        [Test]
        public void TestMissRange()
        {
            Assert.That(() => this.myAlph.GetShiftRange(-1),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Длина строки должна быть положительна!"));

            Assert.Throws<ApplicationException>(() => this.myAlph.GetShiftRange(-1));
        }

        /// <summary>
        /// Тесты для значений первого диапазона.
        /// </summary>
        [Test]
        public void TestFirstRange()
        {
            long shift = this.myAlph.GetShiftRange(1);
            Assert.That(shift, Is.EqualTo(0));
        }

        /// <summary>
        /// Тесты для значений второго диапазона.
        /// </summary>
        [Test]
        public void TestSecondRange()
        {
            long shift = this.myAlph.GetShiftRange(2);
            Assert.That(shift, Is.EqualTo(128));
        }

        /// <summary>
        /// Тесты для значений третьего диапазона.
        /// </summary>
        [Test]
        public void TestThirdRange()
        {
            long shift = this.myAlph.GetShiftRange(3);
            Assert.That(shift, Is.EqualTo(16512));
        }

        /// <summary>
        /// Тесты для значений первого диапазона.
        /// </summary>
        [Test]
        public void TestFourthRange()
        {
            long shift = this.myAlph.GetShiftRange(4);
            Assert.That(shift, Is.EqualTo(2113664));
        }

        /// <summary>
        /// Тесты для значений второго диапазона.
        /// </summary>
        [Test]
        public void TestFifthRange()
        {
            long shift = this.myAlph.GetShiftRange(5);
            Assert.That(shift, Is.EqualTo(270549120));
        }

        /// <summary>
        /// Тесты для значений третьего диапазона.
        /// </summary>
        [Test]
        public void TestSixthRange()
        {
            long shift = this.myAlph.GetShiftRange(6);
            Assert.That(shift, Is.EqualTo(34630287488));
        }
    }
}
