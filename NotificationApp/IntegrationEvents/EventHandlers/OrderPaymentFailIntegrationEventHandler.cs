using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationApp.IntegrationEvents.Events;

namespace NotificationApp.IntegrationEvents.EventHandlers
{
    public class OrderPaymentFailIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentFailIntegrationEvent>
    {
        private readonly ILogger<OrderPaymentFailIntegrationEventHandler> logger;

        public OrderPaymentFailIntegrationEventHandler(ILogger<OrderPaymentFailIntegrationEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(OrderPaymentFailIntegrationEvent @event)
        {
            //Send Fail Notification Sms Email  Push

            logger.LogInformation($"OrderPaymentFailIntegrationEvent   {@event.OrderId} Fail notificationları gitti  Error Message : {@event.ErrorMessage} ");

            return Task.CompletedTask;
        }
    }



}