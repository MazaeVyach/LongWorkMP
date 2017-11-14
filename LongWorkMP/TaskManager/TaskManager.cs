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
        }

        public bool GetTask(long taskSize, ref Task task)
        {
            long beginValue = this.curentValue;
            long endValue = beginValue + taskSize;

            if (this.curentValue > this.endRange)
            {
                task = new Task(this.endRange, this.endRange, this.md5Sum);
                return false;
            }

            if (endValue > this.endRange)
            {
                endValue = this.endRange;
            }

            this.curentValue = endValue + 1;
            task = new Task(beginValue, endValue, this.md5Sum);

            return true;
        }

        public bool FindPassword(ref string password)
        {
            while (true)
            {

                break;
            }

            return false;
        }

       
    }
}