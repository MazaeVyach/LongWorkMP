
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
            TaskManager taskManager = new TaskManager("a", "zz", "0cc175b9c0f1b6a831c399e269772661");
            Task task = null;
            while (taskManager.GetTask(5000, ref task))
                Console.Write(task.ToString());
        }
    }
}
