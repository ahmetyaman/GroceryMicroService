using EventBus.Base.Abstraction;
using ConsoleAppEventBusTest.Events.Events;

namespace ConsoleAppEventBusTest.Events.EventHandlers
{
    public class OrderCreatedIntegrationEventHandler : IIntegrationEventHandler<OrderCreatedIntegrationEvent>
    {
        public Task Handle(OrderCreatedIntegrationEvent @event)
        {

            Console.WriteLine("Handle method worked with id : " + @event.Id);

            return Task.CompletedTask; 
        }
    }
}