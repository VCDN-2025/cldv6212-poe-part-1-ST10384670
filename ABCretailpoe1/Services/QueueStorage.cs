
using Azure.Storage.Queues;
using System;
using System.Threading.Tasks;

namespace ABCretailpoe1.Services
{
    public class QueueStorage
    {
        private readonly QueueClient _queueClient;

        public QueueStorage(string connectionString, string queueName)
        {
            // Initialize the QueueClient
            _queueClient = new QueueClient(connectionString, queueName);

            // Ensure the queue exists
            _queueClient.CreateIfNotExists();
        }

        /// <summary>
        /// Sends a message to the Azure Queue.
        /// </summary>
        public async Task SendMessageAsync(string message)
        {
            if (string.IsNullOrWhiteSpace(message))
                throw new ArgumentException("Message cannot be empty.", nameof(message));

            await _queueClient.SendMessageAsync(message);
        }

        /// <summary>
        /// Receives a message from the Azure Queue.
        /// </summary>
        public async Task<string?> ReceiveMessageAsync()
        {
            var response = await _queueClient.ReceiveMessageAsync();

            if (response.Value != null)
            {
                // Delete the message after reading it
                await _queueClient.DeleteMessageAsync(response.Value.MessageId, response.Value.PopReceipt);
                return response.Value.MessageText;
            }

            return null;
        }

        /// <summary>
        /// Peeks at the next message in the queue without removing it.
        /// </summary>
        public async Task<string?> PeekMessageAsync()
        {
            var response = await _queueClient.PeekMessageAsync();
            return response.Value?.MessageText;
        }

        /// <summary>
        /// Clears all messages from the queue.
        /// </summary>
        public async Task ClearQueueAsync()
        {
            await _queueClient.ClearMessagesAsync();
        }
    }
}
