namespace TaskManager
{
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
            this.alphabet = new Alphabet();
            this.beginRange = this.alphabet.StringToNumber(beginRange);
            this.curentValue = this.beginRange;
            this.endRange = this.alphabet.StringToNumber(endRange);
            this.md5Sum = md5Sum;
            CreateTaskQueue(5000);
        }

        public bool GetTask(long taskSize, ref Task task)
        {
            if (queue.Count == 0)
                return false;

            task = this.queue.Pop();

            return true;
        }

        public void PushTask(Task task)
        {
            queue.Push(task);
        }

        public void CreateTaskQueue(long taskSize)
        {
            long beginValue = curentValue;
            queue = new Stack<Task>();

            while (beginValue <= endRange)
            {
                long endValue = beginValue + taskSize;

                if (endValue > this.endRange)
                    endValue = this.endRange;

                queue.Push(new Task(beginValue, endValue, md5Sum));
                beginValue += endValue + 1;
            }
        }

       
    }
}