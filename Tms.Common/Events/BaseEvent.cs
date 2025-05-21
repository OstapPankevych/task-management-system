namespace Tms.Common.Events;

public class BaseEvent(int entityId, string eventName)
{
    public string EventName { get; } = eventName;
    public int EntityId { get; } = entityId;
}