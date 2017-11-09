namespace AgentInformation
{
    public class AgentInformation
    {
        public AgentInformation(long coresCount, long passwordPerSecond)
        {
            CoresCount = coresCount;
            PasswordPerSecond = passwordPerSecond;
        }

        public override string ToString()
        {
            return CoresCount + " | " + PasswordPerSecond;
        }

        public string Serealize()
        {
            return CoresCount + "|" + PasswordPerSecond;
        }

        public static AgentInformation Deserealize(string str)
        {
            string[] arr = str.Split('|');

            return new AgentInformation(long.Parse(arr[0]), long.Parse(arr[1]));
        }

        public long CoresCount { get; set; }
        public long PasswordPerSecond { get; set; }
    }
}