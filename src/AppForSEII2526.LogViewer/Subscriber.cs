using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;
using System.Threading.Channels;

namespace AppForSEII2526.LogViewer
{
    public class Subscriber
    {
        private readonly string _hostname = "localhost"; //cambiar por la dirección que corresponda
        private readonly string _exchangeName = "logs";
        private readonly string _userName = "guest"; //utilizar las credenciales de un usuario de RabbitMQ
        private readonly string _password = "guest";
        private readonly int _port = 5672; //reemplazar por el puerto AMQP de RabbitMQ
        private readonly string _topicPattern;


        private readonly IConnection _connection;
        private readonly IModel _channel;
        private string _queueName;

        public Subscriber(string topicPattern)
        {
            _topicPattern = topicPattern ?? throw new ArgumentNullException(nameof(topicPattern));
            _hostname = Environment.GetEnvironmentVariable("RABBITMQ_HOST") ?? "localhost";
            var factory = new ConnectionFactory()
            {
                HostName = _hostname,
                UserName = _userName,
                Password = _password,
                Port = _port
            };

            _connection = factory.CreateConnection();

            _channel = _connection.CreateModel();

            // Cambiar a ExchangeType.Topic
            _channel.ExchangeDeclare(_exchangeName, ExchangeType.Topic, true);

            var tempQueue = _channel.QueueDeclare(
                queue: "",  
                durable: false,
                exclusive: true,
                autoDelete: true,
                arguments: null
            );

            _queueName = tempQueue.QueueName;

            _channel.QueueBind(queue: _queueName, exchange: _exchangeName, routingKey: _topicPattern);

        }
        public void StartConsuming()
        {
            var consumer = new EventingBasicConsumer(_channel);
            consumer.Received += (model, ea) =>
            {
                var body = ea.Body.ToArray(); //contenido del mensaje (array de bytes)
                var message = Encoding.UTF8.GetString(body); //se convierte de vuelta a string
                var routingKey = ea.RoutingKey;

                // Mostrar también la routing key
                Console.WriteLine($"[{routingKey}] {message}");

            };

            _channel.BasicConsume(
            queue: _queueName,
            autoAck: true,
            consumer: consumer// Confirmación automática de recepción del mensaje
            );
        }
    }
}
