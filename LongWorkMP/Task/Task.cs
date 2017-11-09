namespace Task
{
    public class Task
    {
        public Task(long rangeStart, long rangeEnd, string md5Sum)
        {
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
            Md5Sum = md5Sum;
        }

        public override string ToString()
        {
            return RangeStart + " | " + RangeEnd + " | " + Md5Sum;
        }

        public string Serealize()
        {
            return RangeStart + "|" + RangeEnd + "|" + Md5Sum;
        }

        public static Task Deserealize(string str)
        {
            string[] arr = str.Split('|');

            return new Task(long.Parse(arr[0]), long.Parse(arr[1]), arr[2]);
        }

        public long RangeStart { get; set; }
        public long RangeEnd { get; set; }
        public string Md5Sum { get; set; }
    }
}