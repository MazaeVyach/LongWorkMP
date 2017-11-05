namespace InterworkingModule
{
    using System;
    using System.Messaging;

    public class ResultsQueue
    {
       /* public void FindQueue()
        {
            foreach (var queue in MessageQueue.GetPrivateQueuesByMachine(Environment.MachineName))
                Console.WriteLine("Очередь: {0}\n", queue.Path);

            Console.ReadLine();

            if (MessageQueue.Exists(@".\private$\MyNewPrivateQueue"))
                var queue = new MessageQueue(@".\private$\MyNewPrivateQueue");
            else
                Console.WriteLine("Очередь не существует");
        }*/
    }
}