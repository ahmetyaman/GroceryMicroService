using EventBus.Base.Events;

namespace NotificationApp.IntegrationEvents.Events
{
    public class OrderPaymentFailIntegrationEvent : IntegrationEvent
    {
        public int OrderId { get; set; }
        public string ErrorMessage { get; set; }

        public OrderPaymentFailIntegrationEvent()
        {
        }

        public OrderPaymentFailIntegrationEvent(int orderId, string errorMessage)
        {
            OrderId = orderId;
            ErrorMessage = errorMessage;
        }
    }
}