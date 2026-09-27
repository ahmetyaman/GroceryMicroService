using EventBus.Base;
using EventBus.Base.Events;
using Newtonsoft.Json;
using Polly;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Client.Exceptions;
using System.Net.Sockets;
using System.Text;

namespace EventBus.RabbitMQ
{
    public class EventBusRabbitMQ : BaseEventBus
    {
        private RabbitMQPersistentConnection persistentConnection;

        private readonly IConnectionFactory connectionFactory;

        private readonly IModel consumerChannel;

        public EventBusRabbitMQ(EventBusConfig config, IServiceProvider serviceProvider) : base(config, serviceProvider)
        {
            if (config.Connection == null)
            {
                var connJson = JsonConvert.SerializeObject(config, new JsonSerializerSettings()
                {
                    ReferenceLoopHandling = ReferenceLoopHandling.Ignore
                });
                connectionFactory = JsonConvert.DeserializeObject<ConnectionFactory>(connJson);
            }
            else
            {
                connectionFactory = new ConnectionFactory();
            }

            persistentConnection = new RabbitMQPersistentConnection(connectionFactory, config.ConnectionRetryCount);

            consumerChannel = CreateConsumerChannel();
        }

        public override void Publish(IntegrationEvent @event)
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            var policy = Policy.Handle<BrokerUnreachableException>()
                    .Or<SocketException>()
                    .WaitAndRetry(EventBusConfig.ConnectionRetryCount, retryAttempt =>
                    TimeSpan.FromSeconds(Math.Pow(2, retryAttempt)), (ex, time) =>
                    {
                    });

            var eventName = @event.GetType().Name;
            eventName = ProcessEventName(eventName);

            consumerChannel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");

            consumerChannel.QueueDeclare(queue: GetSubName(eventName), durable: true,
          exclusive: false, autoDelete: false, arguments: null);

            consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: EventBusConfig.DefaultTopicName,
           routingKey: eventName);

            var message = JsonConvert.SerializeObject(@event);
            var body = Encoding.UTF8.GetBytes(message);

            policy.Execute(() =>
            {
                var properities = consumerChannel.CreateBasicProperties();

                properities.DeliveryMode = 2;

                consumerChannel.BasicPublish(

                    exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName,
                    mandatory: true,
                    basicProperties: properities,
                    body: body

                    );
            });
        }

        public override void Subscribe<T, TH>()
        {
            var eventName = ProcessEventName(typeof(T).Name);
            eventName = ProcessEventName(eventName);

            if (!SubsManager.HasSubscriptionsForEvent(eventName))
            {
                if (!persistentConnection.IsConnected)
                {
                    persistentConnection.TryConnect();
                }

                consumerChannel.QueueDeclare(queue: GetSubName(eventName), durable: true, exclusive: false,
                    autoDelete: false, arguments: null);

                consumerChannel.QueueBind(queue: GetSubName(eventName), exchange: EventBusConfig.DefaultTopicName,
                    routingKey: eventName);
            }

            SubsManager.AddSubscription<T, TH>();
            StartBasicConsume(eventName);

            SubsManager.OnEventRemoved += SubsManager_OnEventRemoved;
        }

        private void SubsManager_OnEventRemoved(object? sender, string eventName)
        {
            eventName = ProcessEventName(eventName);

            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }

            consumerChannel.QueueUnbind(queue: GetSubName(eventName), exchange: EventBusConfig.DefaultTopicName,
                routingKey: eventName);

            if (SubsManager.IsEmpty)
            {
                consumerChannel.Close();
            }
        }

        private void StartBasicConsume(string eventName)
        {
            if (consumerChannel != null)
            {
                var consumer = new EventingBasicConsumer(consumerChannel);
                consumer.Received += Consumer_Received;

                consumerChannel.BasicConsume(queue: GetSubName(eventName), autoAck: false, consumer: consumer);
            }
        }

        private void Consumer_Received(object? sender, BasicDeliverEventArgs e)
        {
            var eventName = e.RoutingKey;
            eventName = ProcessEventName(eventName);

            var message = Encoding.UTF8.GetString(e.Body.Span);

            try
            {
                ProcessEvent(eventName, message).GetAwaiter().GetResult();
            }
            catch (Exception ex)
            {
            }

            consumerChannel.BasicAck(e.DeliveryTag, multiple: false);
        }

        private IModel CreateConsumerChannel()
        {
            if (!persistentConnection.IsConnected)
            {
                persistentConnection.TryConnect();
            }
            var channel = persistentConnection.CreateModel();
            channel.ExchangeDeclare(exchange: EventBusConfig.DefaultTopicName, type: "direct");
            return channel;
        }

        public override void UnSubscribe<T, TH>()
        {
            SubsManager.RemoveSubscription<T, TH>();
        }
    }
}