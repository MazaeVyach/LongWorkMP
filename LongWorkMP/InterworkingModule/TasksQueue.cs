namespace InterworkingModule
{
    using System;
    using System.Messaging;

    public class TasksQueue
    {
        public TasksQueue(MessageQueueTransaction transaction)
        {
            _tasksQueue = MessageQueue.Create(TasksQueuePath, true);

            _transaction = new MessageQueueTransaction();
        }

        public void SendTask(Task task)
        {
            try
            {
                using (var message = new Message(task))
                {
                    message.Priority = MessagePriority.High;
                    message.Recoverable = true;

                    _transaction.Begin();
                    _tasksQueue.Send(message, _transaction);
                    _transaction.Commit();
                }
            }
            catch
            {
                _transaction.Abort();
            }

        }

        public static void ReadMessage()
        {
            var tasksQueue = new MessageQueue(TasksQueuePath);

            tasksQueue.Formatter = new XmlMessageFormatter(
                new Type[]
                {
                    typeof(Task),
                    typeof(int),
                    typeof(int),
                    typeof(string)
                });

            Message message = tasksQueue.Receive();
        }

        private const string TasksQueuePath = ".\\private$\\TasksQueue";
        private readonly MessageQueue _tasksQueue;
        private readonly MessageQueueTransaction _transaction;
    }
}