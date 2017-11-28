namespace TaskManager
{
    using System;
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

        public bool PasswordFoundFlag  { get; set; }

        private Stack<Task> queue;

        public string Password;

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
            PasswordFoundFlag = false;
        }
        
        public void PasswordFound(string password)
        {
            Password = password;
            PasswordFoundFlag = true;
            Console.WriteLine();
        }

        public bool GetTask(long taskSize, ref Task task)
        {
            if (PasswordFoundFlag)
                return false;

            if (queue.Count != 0)
            {
                task = queue.Pop();
                return true;
            }

            int staus = (int)((curentValue - beginRange) * 100 / (this.endRange - this.beginRange));
            Console.Write("\r{0}%", staus);

            long beginValue = curentValue;
            long endValue = beginValue + taskSize;

            if (curentValue > endRange)
            {
                task = new Task(endRange, endRange, md5Sum);
                Console.WriteLine();
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