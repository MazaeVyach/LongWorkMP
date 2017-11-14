
namespace TaskManager.Tests
{
    using System;

    using NUnit.Framework;

    using Task;

    [TestFixture]
    public class Tests
    {
        [Test]
        public void SearchTests0()
        {
            TaskManager taskManager = new TaskManager("aa", "zzz", "kolka - suchka");
            Task task = null;
            while (taskManager.GetTask(ref task))
                Console.Write(task.ToString());
        }
    }
}
