namespace TaskManager.Interface
{
    using System;

    public class Program
    {
        private static void Main()
        {
            Console.Write("Введите начало диапазона: ");
            string begin = Console.ReadLine();

            Console.Write("Введите конец диапазона: ");
            string end = Console.ReadLine();

            Console.Write("Введите md5 свертку: ");
            string md5Sum = Console.ReadLine();
      
            TaskManager taskManager = new TaskManager(begin, end, md5Sum);
            Console.Write("Идет подбор пароля...\n");

            InterworkingTaskManager interworkingTaskManager = new InterworkingTaskManager("127.0.0.1", 8888);
            interworkingTaskManager.Interworking(taskManager);
        }
    }
}