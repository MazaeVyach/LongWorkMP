namespace InterworkingModule
{
    public struct Task
    {
        public Task(int rangeStart, int rangeEnd, string md5Sum)
        {
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
            Md5Sum = md5Sum;
        }

        public int RangeStart { get; set; }

        public int RangeEnd { get; set; }

        public string Md5Sum { get; set; }
    }
}
