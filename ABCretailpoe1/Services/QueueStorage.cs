
using Azure.Storage.Blobs.Models;
using Azure.Storage.Queues;

namespace ABCretailpoe1.Services
{
    public class QueueStorage
    {
        private readonly QueueClient_queueClient;

            public QueueStorage(string connectionString, string queueName)

        {
            if (string.IsNullOrEmpty(connectionString))

                throw new ArgumentException(nameof(connectionString), "Azure Queue Storage connection string is misssing");
            _queueClient = new QueueClient(connectionString, queueName);


        }

        public async Task SendMessage(string message)
        {
            await _queueClient.SendMessageAsync(message);

        }
    }
}