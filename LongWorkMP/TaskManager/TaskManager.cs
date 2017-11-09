
namespace TaskManager
{
    using System.Collections.Generic;

    /// <summary>
    /// Task for calculate.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Task"/> class.
        /// </summary>
        /// <param name="beginValue">
        /// The begin value.
        /// </param>
        /// <param name="endValue">
        /// The end value.
        /// </param>
        public Task(long beginValue, long endValue)
        {
            this.BeginValue = beginValue;
            this.EndValue = endValue;
        }

        /// <summary>
        /// Gets or sets the begin value.
        /// </summary>
        private long BeginValue { get; set; }

        /// <summary>
        /// Gets or sets the end value.
        /// </summary>
        private long EndValue { get; set; }
    }

    public class Class1
    {
        private SortedDictionary<int, Task> GetTaskDictionary(string beginValueStr, string endValueStr, int taskSize)
        {
            long beginValue = 0; // = Alphabet.StringToNumber(beginValueStr)
            long endValue = 0; // = Alphabet.StringToNumber(endValueStr)

            SortedDictionary<int, Task> taskDictionary = new SortedDictionary<int, Task>();

            long beginRange = beginValue;
            long endRange = beginValue + taskSize;

            while (beginRange <= endValue)
            {
                if (beginRange + taskSize > endValue)
                    endRange = endValue;
                else
                    endRange += taskSize;

                Task task = new Task(beginRange, endRange);

                beginValue = endRange + 1;
            }

            return taskDictionary;
        }
    }
}