namespace TaskManager
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    using Task;
    using AgentInformation;

    /// <summary>
    /// Класс для взаимодействия диспетчера заданий с агентом.
    /// </summary>
    public class AgentObject
    {
        public TcpClient Client;

        private readonly TaskManager _taskManager;

        public AgentObject(TcpClient tcpClient, TaskManager  taskManager)
        {
            Client = tcpClient;
            _taskManager = taskManager;
        }

        public void Process()
        {
            NetworkStream networkStream = null; // Базовый поток данных для доступа к сети.
            Task task = null;

            try
            {
                networkStream = Client.GetStream();

                // Получаем информацию о подключенном агенте.
                string agentInfoStr = GetStrFromStream(networkStream);
                AgentInformation agentInfo = AgentInformation.Deserealize(agentInfoStr);

                // Здесь TaskManager заносит себе куда-то информацию о данном агента.
                long taskSize = agentInfo.CoresCount * agentInfo.PasswordPerSecond * 5;
                

                while (_taskManager.GetTask(taskSize, ref task))
                {
                    byte[] data = Encoding.Unicode.GetBytes(task.Serealize());
                    networkStream.Write(data, 0, data.Length);

                    // Получаем информацию о подобранном пароле.
                    string password = GetStrFromStream(networkStream);  // Результат работы агента.
                    Console.WriteLine(password);
                    // Здесь TaskManager обрабатывает результат работы агента.

                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("Провалилось на диапазоне {0} - {1}", task.RangeStart, task.RangeEnd);
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (networkStream != null)
                    networkStream.Close();

                if (Client != null)
                    Client.Close();
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
        private string GetStrFromStream(NetworkStream networkStream)
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
    }
}