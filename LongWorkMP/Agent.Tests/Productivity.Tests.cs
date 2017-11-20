using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Agent.Tests
{
    using NUnit.Framework;

    /// <summary>
    /// Пробные тесты для нахождения продуктивности на каждой конкретной машине
    /// Никогда выполняться не будут!!!
    /// Нельзя сравнивать с чем-либо такие параметры, да еще и на разных машинах
    /// </summary>
    [TestFixture]
    public class ProductivityTests
    {
        /// <summary>
        /// Создание агента.
        /// </summary>
        private readonly Agent myAgent = new Agent();

        /// <summary>
        /// Тест на производительность.
        /// </summary>
        [Test]
        public void PasswordsPerSecond()
        {
            //Assert.That(0, Is.EqualTo(this.myAgent.GetProductivity()));
        }

        /// <summary>
        /// Тест на количество ядер.
        /// </summary>
        [Test]
        public void CoresAmount()
        {
            //Assert.That(4, Is.EqualTo(this.myAgent.GetCores()));
        }
    }
}
