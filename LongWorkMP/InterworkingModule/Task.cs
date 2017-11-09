using System;

namespace InterworkingModule
{
    [Serializable]
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

        public override string ToString()
        {
            return RangeStart + " | " + Md5Sum + " | " + RangeEnd;
        }
    }
}
