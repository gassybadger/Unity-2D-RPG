public delegate void EventBusEvent<TEvent>(TEvent @event) where TEvent : IEvent;

public static class EventBus<TEvent>
    where TEvent : IEvent
{
    public static event EventBusEvent<TEvent> OnEvent;

    public static void Raise(TEvent @event)
    {
        OnEvent?.Invoke(@event);
    }
}
