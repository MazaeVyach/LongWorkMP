namespace TaskManager.Interface
{
    using System;

    public class Program
    {
        private static void Main()
        {
            Console.Write("Введите адрес узла диспетчера заданий: ");
            string address = Console.ReadLine();

            Console.Write("Введите порт указанного узла диспетчера заданий: ");
            string portStr = Console.ReadLine();
            int port = int.Parse(portStr);

            while (true)
            {
                Console.Write("Введите начало диапазона: ");
                string begin = Console.ReadLine();

                Console.Write("Введите конец диапазона: ");
                string end = Console.ReadLine();

                Console.Write("Введите md5 свертку: ");
                string md5Sum = Console.ReadLine();

                TaskManager taskManager = new TaskManager(begin, end, md5Sum);
                Console.Write("Идет подбор пароля...\n");

                InterworkingTaskManager interworkingTaskManager = new InterworkingTaskManager(address, port);
                interworkingTaskManager.Interworking(taskManager);

                if (!taskManager.PasswordFoundFlag)
                    Console.WriteLine("Пароль не найден");
                else
                    Console.WriteLine("Найденный пароль: {0}", taskManager.Password);
            }
        }
    }
}