
namespace TaskManager
{
    using System.Collections.Generic;

    using Alphabet;

    public class Class1
    {
        private SortedDictionary<int, Task> GetTaskDictionary(string beginValueStr, string endValueStr, int taskSize, string md5Sum)
        {
            Alphabet alphabet = new Alphabet();
            long beginValue = alphabet.StringToNumber(beginValueStr);
            long endValue = alphabet.StringToNumber(endValueStr);

            SortedDictionary<int, Task> taskDictionary = new SortedDictionary<int, Task>();

            long beginRange = beginValue;
            long endRange = beginValue + taskSize;

            while (beginRange <= endValue)
            {
                if (beginRange + taskSize > endValue)
                    endRange = endValue;
                else
                    endRange += taskSize;

                Task task = new Task(beginRange, endRange, md5Sum);

                beginValue = endRange + 1;
            }

            return taskDictionary;
        }


    }
}