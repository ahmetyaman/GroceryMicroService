using EventBus.Base.Abstraction;
using Microsoft.Extensions.Logging;
using NotificationApp.IntegrationEvents.Events;

namespace NotificationApp.IntegrationEvents.EventHandlers
{
    public class OrderPaymentSuccessIntegrationEventHandler : IIntegrationEventHandler<OrderPaymentSuccessIntegrationEvent>
    {
        private readonly ILogger<OrderPaymentSuccessIntegrationEventHandler> logger;

        public OrderPaymentSuccessIntegrationEventHandler(ILogger<OrderPaymentSuccessIntegrationEventHandler> logger)
        {
            this.logger = logger;
        }

        public Task Handle(OrderPaymentSuccessIntegrationEvent @event)
        {
            //Send Success Notification Sms Email  Push

            logger.LogInformation($"OrderPaymentSuccessIntegrationEvent   {@event.OrderId} Success notificationları gitti");

            return Task.CompletedTask;
        }
    }



}