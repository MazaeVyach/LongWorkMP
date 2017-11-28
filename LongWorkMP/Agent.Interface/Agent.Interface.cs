namespace Agent.Interface
{
    using System;

    public class AgentInterface
    {
        private static void Main()
        {
            Console.Write("Введите адрес узла диспетчера заданий: ");
            string address = Console.ReadLine();

            Console.Write("Введите порт указанного узла диспетчера заданий: ");
            string portStr = Console.ReadLine();
            int port = int.Parse(portStr);

            Agent.Interworking(address, port);
        }
    }
}