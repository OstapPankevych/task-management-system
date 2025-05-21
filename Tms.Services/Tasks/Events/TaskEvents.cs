using Tms.Common.Events;

namespace Tms.Services.Tasks.Events;

public static class TaskEvents
{
    public static class Completed
    {
        public const string EventName = "task.completed";
        public static BaseEvent Create(int entityId) => new(entityId, EventName);
    }
}