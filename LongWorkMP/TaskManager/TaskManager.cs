namespace TaskManager
{
    using System.Collections.Generic;

    using Alphabet;
    using Task;

    public class TaskManager
    {
        /// <summary>
        /// The begin range.
        /// </summary>
        private long beginRange;

        /// <summary>
        /// The end range.
        /// </summary>
        private long endRange;

        /// <summary>
        /// The curent value.
        /// </summary>
        private long curentValue;

        private string md5Sum;

        private Alphabet alphabet;

        private Stack<Task> queue;

        /// <summary>
        /// Initializes a new instance of the <see cref="TaskManager"/> class.
        /// </summary>
        /// <param name="beginRange">
        /// The begin range.
        /// </param>
        /// <param name="endRange">
        /// The end range.
        /// </param>
        public TaskManager(string beginRange, string endRange, string md5Sum)
        {
            alphabet = new Alphabet();
            this.beginRange = alphabet.StringToNumber(beginRange);
            curentValue = this.beginRange;
            this.endRange = alphabet.StringToNumber(endRange);
            this.md5Sum = md5Sum;
            queue = new Stack<Task>();
        }

        public bool GetTask(long taskSize, ref Task task)
        {
            if (queue.Count != 0)
            {
                task = queue.Pop();
                return true;
            }

            long beginValue = curentValue;
            long endValue = beginValue + taskSize;

            if (curentValue > endRange)
            {
                task = new Task(endRange, endRange, md5Sum);
                return false;
            }
               
            if (endValue > endRange)
            {
                endValue = endRange;
            }

            curentValue = endValue + 1;
            task = new Task(beginValue, endValue, md5Sum);

            return true;
        }

        public void PushTask(Task task)
        {
            queue.Push(task);
        }
    }
}