using ConsoleAppEventBusTest.Events.EventHandlers;
using ConsoleAppEventBusTest.Events.Events;
using EventBus.Base;
using EventBus.Base.Abstraction;
using EventBus.Factory;
using Microsoft.Extensions.DependencyInjection;

namespace ConsoleAppEventBusTest
{
    internal  class Program
    {
        private static  ServiceCollection services;
        private static void Main()
        {
            services = new ServiceCollection();

            services.AddLogging();
            //services.AddLogging(configure => configure.AddConsole());


            Subscribe_event_on_rabbitmq_test();
            send_message_rabbitmq_test();

            Console.ReadLine();
        
        }



      

        private static void Subscribe_event_on_rabbitmq_test()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetRabbitMQConfig(), sp);
            });

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

            eventBus.UnSubscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        }


        private static void send_message_rabbitmq_test()
        {
            services.AddSingleton<IEventBus>(sp =>
            {
                return EventBusFactory.Create(GetRabbitMQConfig(), sp);
            });

            var sp = services.BuildServiceProvider();

            var eventBus = sp.GetRequiredService<IEventBus>();

            for (int i = 0; i < 5; i++)
            {
                eventBus.Publish(new OrderCreatedIntegrationEvent(i));
            }
           


            Console.ReadLine();

        }



        //[TestMethod]
        //public void send_message_azure_test()
        //{
        //    services.AddSingleton<IEventBus>(sp =>
        //    {
        //        return EventBusFactory.Create(GetAzureConfig(), sp);
        //    });

        //    var sp = services.BuildServiceProvider();

        //    var eventBus = sp.GetRequiredService<IEventBus>();

        //    eventBus.Publish(new OrderCreatedIntegrationEvent(1));
        //}

        //[TestMethod]
        //public void Subscribe_event_on_azure_test()
        //{
        //    services.AddSingleton<IEventBus>(sp =>
        //    {
        //        return EventBusFactory.Create(GetAzureConfig(), sp);
        //    });

        //    var sp = services.BuildServiceProvider();

        //    var eventBus = sp.GetRequiredService<IEventBus>();

        //    eventBus.Subscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();

        //    eventBus.UnSubscribe<OrderCreatedIntegrationEvent, OrderCreatedIntegrationEventHandler>();
        //}






        private  static EventBusConfig GetRabbitMQConfig()
        {
            return new EventBusConfig()
            {
                ConnectionRetryCount = 5,
                SubscriberClientAppName = "ConsoleAppEventBusTest",
                DefaultTopicName = "GroceryTopicName",
                EventBusType = EventBusType.RabbitMQ,
                EventNameSuffix = "IntegrationEvent",
                //Connection = new ConnectionFactory()
                //{
                //    HostName = "localhost",
                //    Port = 5672,
                //    UserName = "guest",
                //    Password = "guest"
                //}
            };
        }


        //private EventBusConfig GetAzureConfig()
        //{
        //    return new EventBusConfig()
        //    {
        //        ConnectionRetryCount = 5,
        //        SubscriberClientAppName = "EventBus.UnitTest",
        //        DefaultTopicName = "GroceryTopicName",
        //        EventBusType = EventBusType.RabbitMQ,
        //        EventNameSuffix = "IntegrationEvent",
        //        EventBusConnectionString = ""
        //    };
        //}

    }
}