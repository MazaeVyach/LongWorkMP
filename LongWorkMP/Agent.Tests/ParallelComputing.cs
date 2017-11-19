namespace Agent.Tests
{
    using System;
    using NUnit.Framework;

    /// <summary>
    /// Пробные тесты для нахождения продуктивности на каждой конкретной машине
    /// Никогда выполняться не будут!!!
    /// Нельзя сравнивать с чем-либо такие параметры, да еще и на разных машинах
    /// </summary>
    [TestFixture]
    public class ParallelComputing
    {
        /// <summary>
        /// Создание агента.
        /// </summary>
        private readonly Agent myAgent = new Agent();

        /// <summary>
        /// Тест на вычисление первого элемента блока.
        /// </summary>
        [Test]
        public void GetStartRangeTest()
        {
            long startNumber = 120;
            long endNumber = 150;
            int cores = this.myAgent.GetCores();

            long numbersInOtherBlocks = Agent.GetNumbersInOtherBlocks(startNumber, endNumber, cores);

            Assert.That(120, Is.EqualTo(Agent.GetStartRange(startNumber, numbersInOtherBlocks, 0)));
            Assert.That(128, Is.EqualTo(Agent.GetStartRange(startNumber, numbersInOtherBlocks, 1)));
            Assert.That(136, Is.EqualTo(Agent.GetStartRange(startNumber, numbersInOtherBlocks, 2)));
            Assert.That(144, Is.EqualTo(Agent.GetStartRange(startNumber, numbersInOtherBlocks, 3)));
        }

        /// <summary>
        /// Тест на вычисление последнего элемента блока.
        /// </summary>
        [Test]
        public void GetEndRangeTest()
        {
            long startNumber = 120;
            long endNumber = 150;
            int cores = this.myAgent.GetCores();

            long numbersInOtherBlocks = Agent.GetNumbersInOtherBlocks(startNumber, endNumber, cores);

            Assert.That(127, Is.EqualTo(Agent.GetEndRange(startNumber, numbersInOtherBlocks, cores, 0)));
            Assert.That(135, Is.EqualTo(Agent.GetEndRange(startNumber, numbersInOtherBlocks, cores, 1)));
            Assert.That(143, Is.EqualTo(Agent.GetEndRange(startNumber, numbersInOtherBlocks, cores, 2)));
            Assert.That(150, Is.EqualTo(Agent.GetEndRange(startNumber, numbersInOtherBlocks, cores, 3)));
        }
    }
}
