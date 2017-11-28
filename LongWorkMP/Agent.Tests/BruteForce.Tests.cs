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
            Assert.That(sourceWord, Is.EqualTo(myAgent.BruteForce(100, 15222, knownHash)));
        }

        /// <summary>
        /// Тесты на не попадание.
        /// </summary>
        [Test]
        public void MissBruteForce()
        {
            string sourceWord = string.Empty;
            string knownHash = "b968cdf2eefde38093b46adafe9c5c5b";
            Assert.That(sourceWord, Is.EqualTo(myAgent.BruteForce(15222, 15223, knownHash)));
        }
    }
}
