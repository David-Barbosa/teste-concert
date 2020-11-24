using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Text;

namespace Concert.Domain.RabbitMQHelper
{
    public class RabbitMQHelpers : IRabbitMQHelper
    {
        public ConnectionFactory GetConnectionFactory()
        {
            var connectionFactory = new ConnectionFactory
            {
                HostName = "jackal.rmq.cloudamqp.com",
                UserName = "iimsljqd",
                Password = "G9mHWgzzWeStIti4Pwi5FkCmW8XhbnWY",

                Port = 5672
            };
            return connectionFactory;
        }

        public string RetrieveSingleMessage(EventingBasicConsumer consumer)
        {
            var message = string.Empty;
            consumer.Received += (sender, eventArgs) =>
            {
                message = Encoding.UTF8.GetString(eventArgs.Body);

                Console.WriteLine(Environment.NewLine + "[New message received] " + message);
            };

            return message;
        }

        public uint RetrieveMessageCount(string queueName, IConnection connection)
        {
            uint data;
            using (var channel = connection.CreateModel())
            {
                data = channel.MessageCount(queueName);
            }
            return data;
        }

        public IConnection CreateConnection(ConnectionFactory connectionFactory)
        {
            return connectionFactory.CreateConnection();
        }

        public QueueDeclareOk CreateQueue(string queueName, IConnection connection)
        {
            QueueDeclareOk queue;
            using (var channel = connection.CreateModel())
            {
                queue = channel.QueueDeclare(queueName, false, false, false, null);
            }
            return queue;
        }

        public List<string> RetrieveMessageList(string queueName, IConnection connection)
        {
            int count = 0;
            var messageList = new List<string>();

            using (var channel = connection.CreateModel())
            {
                var messageCount = channel.MessageCount(queueName);
                channel.QueueDeclare(queueName, false, false, false, null);

                var consumer = new QueueingBasicConsumer(channel);

                const bool autoAck = false;
                channel.BasicConsume(queueName, autoAck, consumer);

                while (count < messageCount)
                {
                    var dequeue = consumer.Queue.Dequeue();

                    var body = dequeue.Body;
                    var message = Encoding.UTF8.GetString(body);

                    messageList.Add(message);
                    channel.BasicAck(dequeue.DeliveryTag, false);
                    count++;
                }
            }

            return messageList;
        }

        public bool WriteMessageOnQueue(string message, string queueName, IConnection connection)
        {
            using (var channel = connection.CreateModel())
            {
                channel.BasicPublish(string.Empty, queueName, null, Encoding.ASCII.GetBytes(message));
            }
            return true;
        }
    }
}
