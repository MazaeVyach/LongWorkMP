namespace Agent
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading;

    using AgentInformation;
    using Alphabet;
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
                string currentString = myAplf.NumberToString(i);

                // хэш от строки на каждой итерации
                string currentHash = GetHash(currentString);

                // если полученных хэш равен известному
                if (hash == currentHash)
                    result = currentString;
            }

            return result;
        }

        /// <summary>
        /// Функция возвращает производительность в количестве вычисляемых паролей в секунду.
        /// </summary>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public long GetProductivity()
        {
            // 100000 паролей из начала среднего диапазона
            long startRange = 2113663;
            long endRange  =  2213663;
            // для слова "AGNf" - последнее в этом диапазоне
            string knownHash = "3742009787090f073b14d9d3b8302ee0"; 

            // класс для точного измерения времемни
            Stopwatch watch = new Stopwatch();
            watch.Start();

            BruteForce(startRange, endRange, knownHash);

            watch.Stop();

            long result = (endRange - startRange) / watch.ElapsedMilliseconds * 1000;
            return result;
        }

        /// <summary>
        /// Функция возвращает количество процессоров(вычислительных ядер) 
        /// у многоядерного центрального процессора агента.
        /// </summary>
        /// <returns>
        /// The <see cref="int"/>.
        /// </returns>
        public int GetCores()
        {
            return Environment.ProcessorCount;
        }

        /// <summary>
        /// Возвращает количество чисел в блоках, кроме последнего блока.
        /// </summary>
        /// <param name="startNumber">
        /// Начало диапазона задачи.
        /// </param>
        /// <param name="endNumber">
        /// Конец диапазона задачи.
        /// </param>
        /// <param name="cores">
        /// Количество вычислительных ядер.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long GetNumbersInOtherBlocks(long startNumber, long endNumber, int cores)
        {
            long diapason = endNumber - startNumber + 1;

            // колво чисел в последнем блоке
            long numbersInLastBlock = (long)Math.Floor((double)diapason / cores);

            diapason -= numbersInLastBlock;
            // колво чисел в оставшихся блоках для потоков
            long numbersInOtherBlocks = diapason / (cores - 1);
            return numbersInOtherBlocks;
        }

        /// <summary>
        /// Вычислить первое число в заданном блоке распараллеливания.
        /// </summary>
        /// <param name="startNumber">
        /// Начало диапазона задачи.
        /// </param>
        /// <param name="numbersInOtherBlocks">
        /// Количество чисел в блоках, кроме последнего блока.
        /// </param>
        /// <param name="numberOfRange">
        /// Номер выбранного блока распараллеливания.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long GetStartRange(long startNumber, long numbersInOtherBlocks, int numberOfRange)
        {
            long firstNumberInRange = startNumber + numbersInOtherBlocks * numberOfRange; // первый элемент в блоке
            return firstNumberInRange;
        }

        /// <summary>
        /// Возвращает последнее число в заданном блоке распараллеливания.
        /// </summary>
        /// <param name="startNumber">
        /// Начало диапазона задачи.
        /// </param>
        /// <param name="numbersInOtherBlocks">
        /// Количество чисел в блоках, кроме последнего блока.
        /// </param>
        /// <param name="cores">
        /// Количество вычислительных ядер
        /// </param>
        /// <param name="NumberOfRange">
        /// Номер выбранного блока распараллеливания.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long GetEndRange(long startNumber, long numbersInOtherBlocks, int cores, int NumberOfRange)
        {
            long firstNumberInNextRange = startNumber + numbersInOtherBlocks * (NumberOfRange + 1); //число следующее за нужным
            //если это последнее число в последнем блоке, в котором на один меньше всегда
            if (NumberOfRange == cores - 1)
                return firstNumberInNextRange - 2;
            return firstNumberInNextRange - 1; //последний элемент в блоке
        }

        /// <summary>
        /// Параллельные вычисления.
        /// </summary>
        /// <param name="givenTask">
        /// Переданная задача.
        /// </param>
        /// <returns>
        /// The <see cref="string"/>.
        /// </returns>
        public static string ParallelComputing(Task givenTask)
        {
            Agent agent = new Agent();
            int cores = agent.GetCores();
            long numbersInOtherBlocks = GetNumbersInOtherBlocks(givenTask.RangeStart, givenTask.RangeEnd, cores);
            string initialString = "----------";

            for (int i = 0; i < agent.GetCores(); ++i)
            {
                Thread t = new Thread(
                    () =>
                        {
                            long startNumberForBrute = GetStartRange(givenTask.RangeStart, numbersInOtherBlocks, i);
                            long endNumberForBrute = GetEndRange(givenTask.RangeStart, numbersInOtherBlocks, cores, i) + 1;
                            string pas = agent.BruteForce(startNumberForBrute, endNumberForBrute, givenTask.Md5Sum);

                            if (pas != string.Empty)
                                initialString = pas;
                        });
                t.Start();
                t.Join();
            }

            return initialString;
        }

        /// <summary>
        /// Метод взаимодействия агента с диспетчером заданий.
        /// </summary>
        /// <param name="address">
        /// Адресс узла диспетчера заданий.
        /// </param>
        /// <param name="port">
        /// Порт указанного узла диспетчера заданий.
        /// </param>
        public static void Interworking(string address, int port)
        {
            TcpClient tcpClient = null;
            Agent agent = new Agent();

            bool isConcted = false;

            Console.WriteLine("Попытка подключиться к {0}:{1}", address, port);
            // Цикл бесконечных попыток подключения к диспетчеру заданий.
            while (true)
            {
                try
                {
                    tcpClient = new TcpClient(address, port);
                    NetworkStream networkStream = tcpClient.GetStream();    // Базовый поток данных для доступа к сети.
                    byte[] data = new byte[128];                            // Буфер для получаемых/отправляемых данных.

                    isConcted = true;
                    Console.WriteLine("Установлено соединение {0}:{1}", address, port);

                    // Отправляем диспетчеру задач информацию об агенте.
                    AgentInformation agentInfo = new AgentInformation(agent.GetCores(), agent.GetProductivity());
                    data = Encoding.Unicode.GetBytes(agentInfo.Serealize());
                    networkStream.Write(data, 0, data.Length);

                    while (true)
                    {
                        // Получаем задание от диспетчера заданий.
                        string taskStr = GetStrFromStream(networkStream);
                        Task task = Task.Deserealize(taskStr);

                        // Обрабатываем задание.
                        string initialString = ParallelComputing(task);

                        // Отправляем результаты обработки задания диспетчеру заданий.
                        data = Encoding.Unicode.GetBytes(initialString);
                        networkStream.Write(data, 0, data.Length);
                    }

                }
                catch (Exception)
                {
                    if (isConcted)
                        Console.WriteLine("Попытка подключиться к {0}:{1}", address, port);

                    isConcted = false;
                }
                finally
                {
                    if (tcpClient != null)
                        tcpClient.Close();
                }
                
                // Задержка перед очередной попыткой подключения к серверу.
                Thread.Sleep(5000);
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
            byte[] bytesBuffer = new byte[128];             // Буфер байтов получаемых данных.
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