namespace Agent
{
    using System;
    using System.Text;
    using System.Net.Sockets;
    using System.Security.Cryptography;

    using Alphabet;
    using AgentInformation;
    using Task;

    /// <summary>
    /// Класс агента.
    /// </summary>
    public class Agent
    {
        private Alphabet myAplf = new Alphabet();

        /// <summary>
        /// Получение MD5 хэша.
        /// </summary>
        /// <param name="md5Hash">
        /// Метод хэширования.
        /// </param>
        /// <param name="input">
        /// Входящая строка.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string CreateMd5Hash(MD5 md5Hash, string input)
        {
            // Convert the input string to a byte array and compute the hash.
                byte[] data = md5Hash.ComputeHash(Encoding.UTF8.GetBytes(input));

                // Create a new Stringbuilder to collect the bytes
                // and create a string.
                StringBuilder sBuilder = new StringBuilder();

                // Loop through each byte of the hashed data 
                // and format each one as a hexadecimal string.
                for (int i = 0; i < data.Length; i++)
                    sBuilder.Append(data[i].ToString("x2"));

                // Return the hexadecimal string.
                return sBuilder.ToString();
        }

        /// <summary>
        /// Метод получения 32 символьного MD5 хэша.
        /// </summary>
        /// <param name="incoming">
        /// Входящая строка.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string GetHash(string incoming)
        {
            using (MD5 md5Hash = MD5.Create())
            {
                string hash = CreateMd5Hash(md5Hash, incoming);

                return hash;
            }
        }

        /// <summary>
        /// Метод перебора строк из двух диапазонов и сравнения с переданным хешем.
        /// </summary>
        /// <param name="startRange">
        /// Начальный диапазон.
        /// </param>
        /// <param name="endRange">
        /// Конечный диапазон.
        /// </param>
        /// <param name="hash">
        /// Хэш, для которого нужно найти исходную строку.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public string BruteForce(long startRange, long endRange, string hash)
        {
            string result = string.Empty;

            for (long i = startRange; i <= endRange; ++i)
            {
                // известная строка на каждой итерации
                string currentString = this.myAplf.NumberToString(i);

                // хэш от строки на каждой итерации
                string currentHash = GetHash(currentString);

                // если полученных хэш равен известному
                if (hash == currentHash)
                    result = currentString;
            }

            if (result != string.Empty)
                return result;

            throw new ApplicationException("Хэш не найден!");
        }

        // Метод Взаимодействия с другими модулями

        private const int Port = 8888;
        private const string Address = "127.0.0.1";

        public void Interworking()
        {
            TcpClient tcpClient = null;

            try
            {
                tcpClient = new TcpClient(Address, Port);

                NetworkStream networkStream = tcpClient.GetStream();

                byte[] data = new byte[64]; // Буфер для получаемых/отправляемых данных.

                // Отправляем диспетчеру информацию об агенте.
                AgentInformation agentInfo = new AgentInformation(4, 555555);   // Здесь Agent отправляет информацию о себе диспетчеру заданий.

                data = Encoding.Unicode.GetBytes(agentInfo.Serealize());

                networkStream.Write(data, 0, data.Length);

                while (true)
                {
                    // Получаем задание от диспетчера заданий.
                    StringBuilder message = new StringBuilder();

                    do
                    {
                        int bytesCount = networkStream.Read(data, 0, data.Length);
                        message.Append(Encoding.Unicode.GetString(data, 0, bytesCount));

                    } while (networkStream.DataAvailable);

                    Task task = Task.Deserealize(message.ToString());

                    // Здесь агент должен принять задание на выполнение.

                    // Отправляем диспетчеру вычисленный пароль.
                    string password = "";   // Здесь агент должен представить результаты вычислений.

                    data = Encoding.Unicode.GetBytes(password);

                    networkStream.Write(data, 0, data.Length);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            finally
            {
                if (tcpClient != null)
                    tcpClient.Close();
            }
        }
    }
}
