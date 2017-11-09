using System;

namespace InterworkingModule
{
    [Serializable]
    public struct Task
    {
        public Task(long rangeStart, long rangeEnd, string md5Sum)
        {
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
            Md5Sum = md5Sum;
        }

        public long RangeStart { get; set; }

        public long RangeEnd { get; set; }

        public string Md5Sum { get; set; }

        public override string ToString()
        {
            return RangeStart + " | " + Md5Sum + " | " + RangeEnd;
        }
    }
}
