namespace AgentInformation
{
    /// <summary>
    /// Класс, содержащий информацию об агенте.
    /// </summary>
    public class AgentInformation
    {
        /// <summary>
        /// Конструктор информации об агенте.
        /// </summary>
        /// <param name="coresCount"> 
        /// Количество ядер агента. 
        /// </param>
        /// <param name="passwordPerSecond"> Количество подбираемых паролей в секунду одним ядром. </param>
        public AgentInformation(long coresCount, long passwordPerSecond)
        {
            CoresCount = coresCount; // agent.GetCores()
            PasswordPerSecond = passwordPerSecond;
        }

        /// <summary>
        /// Метод, позволяющий получить строковое представление информации об агенте.
        /// </summary>
        /// <returns>
        /// Строковое представление информации об агенте.
        /// </returns>
        public override string ToString()
        {
            return CoresCount + " | " + PasswordPerSecond;
        }

        /// <summary>
        /// Метод, сериализующий информацию об агенте в строку.
        /// </summary>
        /// <returns>
        /// Строка, содержащая сериализованную информацию об агенте.
        /// </returns>
        public string Serealize()
        {
            return CoresCount + "|" + PasswordPerSecond;
        }

        /// <summary>
        /// Метод, позволяющий получить информацию об агенте из строки,
        /// содержащей сериализованную информацию об агенте.
        /// </summary>
        /// <param name="str"> 
        /// Строка, содержащая сериализованную информацию об агенте. 
        /// </param>
        /// <returns>
        /// Информация об агенте.
        /// </returns>
        public static AgentInformation Deserealize(string str)
        {
            string[] arr = str.Split('|');

            return new AgentInformation(long.Parse(arr[0]), long.Parse(arr[1]));
        }

        /// <summary>
        /// Количество ядер агента.
        /// </summary>
        public long CoresCount { get; set; }

        /// <summary>
        /// Количество подбираемых паролей в секунду одним ядром. 
        /// </summary>
        public long PasswordPerSecond { get; set; }
    }
}