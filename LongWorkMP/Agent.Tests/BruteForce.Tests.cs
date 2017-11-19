namespace Agent.Tests
{
    using System;

    using NUnit.Framework;

    /// <summary>
    /// Тесты для функции перебора.
    /// </summary>
    [TestFixture]
    public class BruteForceTests
    {
        /// <summary>
        /// Создание агента.
        /// </summary>
        private readonly Agent myAgent = new Agent();

        /// <summary>
        /// Тесты для функции перебора.
        /// </summary>
        [Test]
        public void FirstBruteForce()
        {
            string sourceWord = "ЯЯ"; //15221
            string knownHash = "b968cdf2eefde38093b46adafe9c5c5b";
            Assert.That(sourceWord, Is.EqualTo(this.myAgent.BruteForce(100, 15222, knownHash)));
        }

        /// <summary>
        /// Тесты на не попадание.
        /// </summary>
        [Test]
        public void MissBruteForce()
        {
            //string sourceWord = "ЯЯ"; //15221
            string knownHash = "b968cdf2eefde38093b46adafe9c5c5b";

            Assert.That(() => this.myAgent.BruteForce(15222, 15223, knownHash),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Хэш не найден!"));
            //250000 за 2 секунды
            Assert.Throws<ApplicationException>(() => this.myAgent.BruteForce(15222, 15223, knownHash));
        }
    }
}
