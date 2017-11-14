namespace Task
{
    /// <summary>
    /// Класс, описывающий задание, которое отправляет диспетчер задач агенту.
    /// </summary>
    public class Task
    {
        /// <summary>
        /// Конструктор задания.
        /// </summary>
        /// <param name="rangeStart"> 
        /// Начало диапазона поиска.
        /// </param>
        /// <param name="rangeEnd"> 
        /// Конец диапазона поиска. 
        /// </param>
        /// <param name="md5Sum"> 
        /// MD5 хэш пароля. 
        /// </param>
        public Task(long rangeStart, long rangeEnd, string md5Sum)
        {
            RangeStart = rangeStart;
            RangeEnd = rangeEnd;
            Md5Sum = md5Sum;
        }

        /// <summary>
        /// Метод, позволяющий получить строковое представление задания.
        /// </summary>
        /// <returns>
        /// Строковое представление задания.
        /// </returns>
        public override string ToString()
        {
            return RangeStart + " | " + RangeEnd + " | " + Md5Sum;
        }

        /// <summary>
        /// Метод, сериализующий задание в строку.
        /// </summary>
        /// <returns>
        /// Строка, содержащая сериализованное задание.
        /// </returns>
        public string Serealize()
        {
            return RangeStart + "|" + RangeEnd + "|" + Md5Sum;
        }

        /// <summary>
        /// Метод, позволяющий получить задание из строки, содержащей сериализованное задание.
        /// </summary>
        /// <param name="str"> 
        /// Строка, содержащая сериализованное задание. 
        /// </param>
        /// <returns>
        /// Задание.
        /// </returns>
        public static Task Deserealize(string str)
        {
            string[] arr = str.Split('|');

            return new Task(long.Parse(arr[0]), long.Parse(arr[1]), arr[2]);
        }

        /// <summary>
        /// Начальный диапазон поиска.
        /// </summary>
        public long RangeStart { get; set; }

        /// <summary>
        /// Конечный диапазон поиска.
        /// </summary>
        public long RangeEnd { get; set; }

        /// <summary>
        /// MD5 хэш пароля.
        /// </summary>
        public string Md5Sum { get; set; }
    }
}