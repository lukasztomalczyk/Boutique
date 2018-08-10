using System;
using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Boutique.Consumer
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("UserHasBeenCreatedEvent");
            var factory = new ConnectionFactory() { HostName = "172.17.0.3" };
            using(var connection = factory.CreateConnection())
            using(var channel = connection.CreateModel())
            {
                channel.ExchangeDeclare(exchange: "boutique_event_bus", type: "topic");
                var queueName = channel.QueueDeclare().QueueName;


                var bindingKey = "butique.*";


                    channel.QueueBind(queue: queueName, exchange: "boutique_event_bus", routingKey: bindingKey);


                Console.WriteLine(" [*] Waiting for messages. To exit press CTRL+C");

                var consumer = new EventingBasicConsumer(channel);
                consumer.Received += (model, ea) =>
                {
                    var body = ea.Body;
                    var message = Encoding.UTF8.GetString(body);
                    var routingKey = ea.RoutingKey;
                    Console.WriteLine(" [x] Received '{0}':'{1}'", routingKey, message);
                };
                
                channel.BasicConsume(queue: queueName, autoAck: true, consumer: consumer);

                Console.WriteLine(" Press [enter] to exit.");
                Console.ReadLine();
                
            }
        }
    }
}
