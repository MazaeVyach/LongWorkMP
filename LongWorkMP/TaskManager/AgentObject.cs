namespace TaskManager
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    using Task;
    using AgentInformation;

    /// <summary>
    /// Класс для взаимодействия диспетчера заданий с конкретным агентом в отдельном потоке.
    /// </summary>
    public class AgentObject
    {
        /// <summary>
        /// Конструктор класса взаимодействия диспетчера заданий с конкретным агентом в отдельном потоке.
        /// </summary>
        /// <param name="tcpClient">
        /// Клиентское подключение для сетевой службы протокола TCP.
        /// </param>
        /// <param name="taskManager">
        /// Диспетчер заданий.
        /// </param>
        public AgentObject(TcpClient tcpClient, ref TaskManager taskManager)
        {
            _client = tcpClient;
            _taskManager = taskManager;
        }

        /// <summary>
        /// Метод обработки конкретного клиента в отдельном потоке.
        /// </summary>
        public void Process()
        {
            NetworkStream networkStream = null; // Базовый поток данных для доступа к сети.
            Task task = null;                   // Отправляемое задание.

            try
            {
                networkStream = _client.GetStream();

                // Получаем информацию о подключённом агенте.
                string agentInfoStr = GetStrFromStream(networkStream);
                AgentInformation agentInfo = AgentInformation.Deserealize(agentInfoStr);
                
                // ???????
                long taskSize = agentInfo.CoresCount * agentInfo.PasswordPerSecond * 5;

                while (_taskManager.GetTask(taskSize, ref task))
                {
                    // Отправляем задание агенту.
                    byte[] data = Encoding.Unicode.GetBytes(task.Serealize());
                    networkStream.Write(data, 0, data.Length);

                    // Получаем результат работы агента.
                    string password = GetStrFromStream(networkStream);

                    // Обрабатываем результат работы агента.
                    if (password != "----------")
                    {
                        Console.WriteLine("Найденный пароль: {0}", password);

                        _taskManager.PasswordFound();
                    }
                }

                Console.WriteLine("В данном диапазоне пароль не найден");
            }
            catch (Exception)
            {
                // Отправляем диспетчеру задач задание, во время 
                // выполнения которого агент завершил свою работу.
                _taskManager.PushTask(task);
            }
            finally
            {
                if (networkStream != null)
                    networkStream.Close();

                if (_client != null)
                    _client.Close();
            }
        }

        /// <summary>
        /// Метод считывания строки из базового потока данных для доступа к сети.
        /// </summary>
        /// <param name="networkStream">
        /// Базовый поток данных для доступа к сети.
        /// </param>
        /// <returns>
        /// Строка, считанная из потока.
        /// </returns>
        private static string GetStrFromStream(NetworkStream networkStream)
        {
            byte[] bytesBuffer = new byte[128];  // Буфер байтов получаемых данных.
            StringBuilder dataStr = new StringBuilder();    // Строка получаемых данных.

            do
            {
                int bytesCount = networkStream.Read(bytesBuffer, 0, bytesBuffer.Length);
                dataStr.Append(Encoding.Unicode.GetString(bytesBuffer, 0, bytesCount));

            } while (networkStream.DataAvailable);

            return dataStr.ToString();
        }

        /// <summary>
        /// Клиентское подключение для сетевой службы протокола TCP.
        /// </summary>
        private readonly TcpClient _client;

        /// <summary>
        /// Диспетчер заданий.
        /// </summary>
        private readonly TaskManager _taskManager;
    }
}