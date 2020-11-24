using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Collections.Generic;

namespace Concert.Domain.RabbitMQHelper
{
    public interface IRabbitMQHelper
    {
        ConnectionFactory GetConnectionFactory();

        string RetrieveSingleMessage(EventingBasicConsumer consumer);

        uint RetrieveMessageCount(string queueName, IConnection connection);

        IConnection CreateConnection(ConnectionFactory connectionFactory);

        QueueDeclareOk CreateQueue(string queueName, IConnection connection);

        List<string> RetrieveMessageList(string queueName, IConnection connection);

        bool WriteMessageOnQueue(string message, string queueName, IConnection connection);
    }
}
