using DotNetCore.CAP;
using Microsoft.Extensions.Logging;
using Tms.Common.Events;

namespace Tms.Services.Tasks.Events.Handlers;

public class TaskEventHandler(ILogger<TaskEventHandler> logger) : ICapSubscribe
{
    [CapSubscribe(TaskEvents.Completed.EventName)]
    public Task ProcessAsync(BaseEvent @event, CancellationToken cancellationToken)
    {
        logger.LogInformation("Task with id {EntityId} has been completed", @event.EntityId);
        return Task.CompletedTask;
    }
}