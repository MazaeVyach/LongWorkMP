namespace TaskManager.Interface
{
    using System;

    using Task;
    using Alphabet;

    public class Program
    {
        private static void Main(string[] args)
        {
            Console.Write("Введите начало диапазона ");
            string begin = Console.ReadLine();
            Console.Write("Введите конец диапазона ");
            string end = Console.ReadLine();
            Console.Write("Введите md5 свертку ");
            string md5Sum = Console.ReadLine();
            Console.Write("Подождите идет подбор пароля...");
            TaskManager taskManager = new TaskManager(begin, end, md5Sum);       
        }
    }
}