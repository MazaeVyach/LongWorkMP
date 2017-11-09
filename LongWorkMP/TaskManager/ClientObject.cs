namespace TaskManager
{
    using System;
    using System.Net.Sockets;
    using System.Text;

    using Task;
    using AgentInformation;

    public class ClientObject
    {
        public TcpClient Client;

        public ClientObject(TcpClient tcpClient)
        {
            Client = tcpClient;
        }

        public void Process()
        {
            NetworkStream networkStream = null; // Базовый поток данных для доступа к сети.

            try
            {
                networkStream = Client.GetStream();

                // Получаем информацию о подключенном агенте.
                string agentInfoStr = GetStrFromStream(networkStream);
                AgentInformation agentInfo = AgentInformation.Deserealize(agentInfoStr);

                // Здесь TaskManager заносит себе куда-то информацию о данном агента.


                while (true)
                {
                    // Отправляем задание агенту.
                    Task task = new Task(1, 2, "123");  // Здесь TaskManager выдает задание данному агента.

                    byte[] data = Encoding.Unicode.GetBytes(task.Serealize());
                    networkStream.Write(data, 0, data.Length);

                    // Получаем информацию о подобранном пароле.
                    string password = GetStrFromStream(networkStream);  // Результат работы агента.

                    // Здесь TaskManager обрабатывает результат работы агента.

                }
            }
            catch (Exception ex)
            {
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