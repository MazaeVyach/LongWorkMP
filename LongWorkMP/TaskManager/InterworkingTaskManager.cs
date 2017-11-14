namespace TaskManager
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    using Task;

    public class InterworkingTaskManager
    {
        /// <summary>
        /// Адресс узла диспетчера заданий.
        /// </summary>
        private const string Address = "127.0.0.1";

        /// <summary>
        /// Порт указанного узла диспетчера заданий.
        /// </summary>
        private const int Port = 8888;

        /// <summary>
        /// Объект класса TcpListener, который будет прослушивать
        /// подключения к диспетчеру заданий от агентов.
        /// </summary>
        private static TcpListener _listener;

        /// <summary>
        /// Метод взаимодействия диспетчера заданий с агентами.
        /// </summary>
        public void Interworking(TaskManager taskManager)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Parse(Address), Port);

                _listener.Start();  // Запуск ожидания входящих запросов на подключение.

                while (true)
                {
                    TcpClient client = _listener.AcceptTcpClient(); // Подключение нового клиента.
                    AgentObject agentObject = new AgentObject(client, ref taskManager);

                    // Создаем новый поток для обслуживания нового клиента.
                    Thread clientThread = new Thread(agentObject.Process);
                    clientThread.Start();

                   /* Task task = null;
                    taskManager.GetTask(ref task);

                    Console.WriteLine("Провалилось на задании: ", task);*/
                    
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (_listener != null)
                    _listener.Stop();
            }
        }
    }
}
