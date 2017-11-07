
namespace Agent.Tests
{
    using System;

    using NUnit.Framework;

    /// <summary>
    /// Тесты метода GetMd5Hash()
    /// </summary>
    [TestFixture]
    public class GetMd5HashTests
    {
        /// <summary>
        /// Создание агента.
        /// </summary>
        private Agent myAgent = new Agent();

        /// <summary>
        /// Тесты для функции получения md5.
        /// </summary>
        [Test]
        public void Testmd5()
        {
            string first = "abcdef";
            string firstHash = this.myAgent.GetHash(first);
            Assert.That(firstHash, Is.EqualTo("e80b5017098950fc58aad83c8c14978e"));

            string second = "яблоко";
            string secondHash = this.myAgent.GetHash(second);
            Assert.That(secondHash, Is.EqualTo("1afa148eb41f2e7103f21410bf48346c"));

            string third = "РУЧКА";
            string thirdHash = this.myAgent.GetHash(third);
            Assert.That(thirdHash, Is.EqualTo("4e37345e613a684738c0d6e52fd4b699"));
        }
    }

    /// <summary>
    /// Тесты для функции перебора.
    /// </summary>
    [TestFixture]
    public class BruteForceTests
    {
        /// <summary>
        /// Создание агента.
        /// </summary>
        private Agent myAgent = new Agent();

        /// <summary>
        /// Тесты для функции.
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
            string sourceWord = "ЯЯ"; //15221
            string knownHash = "b968cdf2eefde38093b46adafe9c5c5b";

            Assert.That(() => this.myAgent.BruteForce(15222, 15223, knownHash),
                Throws.TypeOf<ApplicationException>().
                    With.Message.EqualTo("Хэш не найден!"));

            Assert.Throws<ApplicationException>(() => this.myAgent.BruteForce(15222, 15223, knownHash));


        }
    }
}
