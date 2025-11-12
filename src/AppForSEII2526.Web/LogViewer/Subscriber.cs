using RabbitMQ.Client;
using System.Threading.Channels;

namespace AppForSEII2526.Web.LogViewer
{
    public class Subscriber
    {
        private readonly string _hostname = "localhost"; //cambiar por la dirección que corresponda
        private readonly string _exchangeName = "logs";
        private readonly string _userName = "guest"; //utilizar las credenciales de un usuario de RabbitMQ
        private readonly string _password = "guest";
        private readonly int _port = 5672; //reemplazar por el puerto AMQP de RabbitMQ


        private readonly IConnection _connection;
        private readonly IModel _channel;
        private readonly IBasicProperties _properties;

        public Subscriber()
        {
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _userName,
                Password = _password,
                Port = _port
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Fanout);

            var tempQueue = _channel.QueueDeclare(
                queue: "",  
                durable: false,
                exclusive: true,
                autoDelete: true,
                arguments: null
            );

            var queueName = tempQueue.QueueName;

            _channel.QueueBind(queue: queueName, exchange: _exchangeName, routingKey: "");




        }
    }
}
