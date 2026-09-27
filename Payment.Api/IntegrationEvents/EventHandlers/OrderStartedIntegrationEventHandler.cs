using EventBus.Base.Abstraction;
using EventBus.Base.Events;
using Payment.Api.IntegrationEvents.Events;

namespace Payment.Api.IntegrationEvents.EventHandlers
{
    public class OrderStartedIntegrationEventHandler : IIntegrationEventHandler<OrderStartedIntegrationEvent>
    {
        private readonly IConfiguration configuration;

        private readonly IEventBus eventBus;
        private readonly ILogger<OrderStartedIntegrationEventHandler> logger;

        public OrderStartedIntegrationEventHandler(IConfiguration configuration, IEventBus eventBus, ILogger<OrderStartedIntegrationEventHandler> logger)
        {
            this.configuration = configuration;
            this.eventBus = eventBus;
            this.logger = logger;
        }

        public Task Handle(OrderStartedIntegrationEvent @event)
        {
            string keyword = "PaymentSuccess";

            bool paymentSuccessFlag = configuration.GetValue<bool>(keyword);

            //Burada  ödeme işlemlerini yaptıktan sonra işlem sonucu   başarılı true yada false göre  event bus a message çıkarmak gerekiyor .


            logger.LogInformation($" OrderId: {@event.OrderId}  OrderPaymentStartIntegration  ===>>>>>>> Burada  ödeme işlemlerini yaptıktan sonra işlem sonucu   başarılı true yada false göre  event bus a message çıkarmak gerekiyor .");

            IntegrationEvent paymentEvent = paymentSuccessFlag
                ? new OrderPaymentSuccessIntegrationEvent(@event.OrderId)
                : new OrderPaymentFailIntegrationEvent(@event.OrderId,"Ödeme sırasında bazı yanlışlıklar yapıldı özür  dileriz .:D");

            eventBus.Publish(paymentEvent);

            return Task.CompletedTask;
        }
    }
}