namespace Agent
{
    using System;
    using System.Diagnostics;
    using System.Net.Sockets;
    using System.Security.Cryptography;

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
        /// Адресс узла диспетчера заданий.
        /// </summary>
        private const string Address = "127.0.0.1";

        /// <summary>
        /// Порт указанного узла диспетчера заданий.
        /// </summary>
        private const int Port = 8888;

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

        /// <summary>
        /// Функция возвращает производительнсоть в количестве вычисляемых паролей в секунду.
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
        /// Функция возвращает количество процессоров(вычислительных ядер) у многоядерного центрального процессора агента.
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
        /// <param name="StartNumber">
        /// Начало диапазона задачи.
        /// </param>
        /// <param name="EndNumber">
        /// Конец диапазона задачи.
        /// </param>
        /// <param name="cores">
        /// Количество вычислительных ядер.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long GetNumbersInOtherBlocks(long StartNumber, long EndNumber, int cores)
        {
            long diapason = EndNumber - StartNumber + 1;

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
        /// <param name="StartNumber">
        /// Начало диапазона задачи.
        /// </param>
        /// <param name="NumbersInOtherBlocks">
        /// Количество чисел в блоках, кроме последнего блока.
        /// </param>
        /// <param name="NumberOfRange">
        /// Номер выбранного блока распараллеливания.
        /// </param>
        /// <returns>
        /// The <see cref="long"/>.
        /// </returns>
        public static long GetStartRange(long StartNumber, long NumbersInOtherBlocks, int NumberOfRange)
        {
            long firstNumberInRange = StartNumber + NumbersInOtherBlocks * NumberOfRange; // первый элемент в блоке
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
        /// Метод взаимодействия агента с диспетчером заданий.
        /// </summary>
        public static void Interworking()
        {
            TcpClient tcpClient = null;
            Agent agent = new Agent();
            try
            {
                tcpClient = new TcpClient(Address, Port);

                NetworkStream networkStream = tcpClient.GetStream();    // Базовый поток данных для доступа к сети.

                byte[] data = new byte[128]; // Буфер для получаемых/отправляемых данных.

                // Отправляем диспетчеру информацию об агенте.
                AgentInformation agentInfo = new AgentInformation(agent.GetCores(), agent.GetProductivity());   // Здесь Agent отправляет информацию о себе диспетчеру заданий.

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

                    // !!!!!!!!!!!!!Блок обработки задания !!!!!!!!!!!!//
                    // будет вынесено как отдельная функция
                    int cores = agent.GetCores();
                    long numbersInOtherBlocks = GetNumbersInOtherBlocks(task.RangeStart, task.RangeEnd, cores);

                    for (int i = 0; i < agent.GetCores(); ++i)
                    {
                        Thread t = new Thread(
                            () =>
                                {
                                    long startNumberForBrute = GetStartRange(task.RangeStart, numbersInOtherBlocks, i);
                                    long endNumberForBrute = GetEndRange(task.RangeStart, numbersInOtherBlocks, cores, i) + 1;
                                    string result = agent.BruteForce(startNumberForBrute, endNumberForBrute, task.Md5Sum);
                                    
                                    // if(result != String.Empty)
                                       // t.Abort();

                                        /*Console.WriteLine("hello from thread number {0}", i);
                                    for (long curNumber = GetStartRange(task.RangeStart, numbersInOtherBlocks, i);
                                         curNumber < GetEndRange(task.RangeStart, numbersInOtherBlocks, cores, i) + 1;
                                         ++curNumber)
                                    {
                                        
                                        Console.WriteLine("Here are my numbers: {0}", curNumber);
                                    }*/
                                });
                        t.Start();
                        t.Join();
                    }

                    // РАСПАРАЛЛЕЛИТЬ ЗДЕСЬ
                    // написав метод Параллельные вычисления(Task task)

                    // Здесь агент должен принять задание на выполнение.
                    // !!! делить задачу на потоки исходя из колва ядер. 
                    // сделать разбиение диапазона на колво ядер
                    // запускать потоками и в каждом потоке считать свой диапазон
                    // Отправляем диспетчеру вычисленный пароль.

                    // отправлять пустую стркоу в случае не нахождения пароля в заданном диаапазоне по свертуе

                    /*StringBuilder password = new StringBuilder();   // Здесь агент должен предоставить результаты вычислений.
                    password.Append(task.RangeStart);
                    password.Append(" - ");
                    password.Append(task.RangeEnd);
                    data = Encoding.Unicode.GetBytes(password.ToString());

                    networkStream.Write(data, 0, data.Length);
                }
            }
            catch (ApplicationException ex)
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
