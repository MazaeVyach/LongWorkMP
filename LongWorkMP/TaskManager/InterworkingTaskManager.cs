namespace TaskManager
{
    using System;
    using System.Net;
    using System.Net.Sockets;
    using System.Threading;

    /// <summary>
    /// Класс для межсетевого взаимодействия диспетчера задач с агентами.
    /// </summary>
    public class InterworkingTaskManager
    {
        /// <summary>
        /// Конструктор класса межсетевого взаимодействия диспетчера задач с агентами.
        /// </summary>
        /// <param name="address">
        /// Адресс узла диспетчера заданий.
        /// </param>
        /// <param name="port">
        /// Порт указанного узла диспетчера заданий.
        /// </param>
        public InterworkingTaskManager(string address, int port)
        {
            _address = address;
            _port = port;
        }

        /// <summary>
        /// Метод взаимодействия диспетчера заданий с агентами.
        /// </summary>
        public void Interworking(TaskManager taskManager)
        {
            try
            {
                _listener = new TcpListener(IPAddress.Parse(_address), _port);

                _listener.Start();  // Запуск ожидания входящих запросов на подключение.

                while (taskManager.PasswordFoundFlag == false)
                {
                    TcpClient client = _listener.AcceptTcpClient(); // Подключение нового клиента.
                    AgentObject agentObject = new AgentObject(client, ref taskManager);

                    // Создаем новый поток для обслуживания нового клиента.
                    Thread clientThread = new Thread(agentObject.Process);
                    clientThread.Start();   
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

        /// <summary>
        /// Адресс узла диспетчера заданий.
        /// </summary>
        private readonly string _address;

        /// <summary>
        /// Порт указанного узла диспетчера заданий.
        /// </summary>
        private readonly int _port;

        /// <summary>
        /// Объект класса TcpListener, который будет прослушивать
        /// подключения к диспетчеру заданий от агентов.
        /// </summary>
        private static TcpListener _listener;
    }
}