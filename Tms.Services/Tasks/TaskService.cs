using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Tms.Common.Exceptions;
using Tms.Common.Extensions;
using Tms.Common.Utils;
using Tms.Db;
using Tms.Db.Entities;
using Tms.Services.Tasks.DTOs;
using Tms.Services.Tasks.Events;
using TaskStatus = Tms.Common.Enums.TaskStatus;

namespace Tms.Services.Tasks;

public class TaskService(ICapPublisher capPublisher, ITmsDbContext dbContext)
    : ITaskService
{
    public async Task<TaskCreatedDto> CreateTaskAsync(CreateTaskDto dto, CancellationToken ct = default)
    {
        dto.ThrowIfInvalid();
        
        var entity = new TaskEntity
        {
            Name = dto.Name,
            Description = dto.Description,
            Status = TaskStatus.NotStarted
        };

        await dbContext.Tasks.AddAsync(entity, ct);
        await dbContext.SaveChangesAsync(ct);

        var result = new TaskCreatedDto
        {
            TaskId = entity.Id
        };
        
        return result;
    }

    public async Task UpdateTaskStatusAsync(int taskId, UpdateTaskStatusDto dto, CancellationToken ct = default)
    {
        dto.ThrowIfInvalid();
        
        var task = await dbContext.Tasks.FirstOrDefaultAsync(x => x.Id == taskId, ct);
        if (task == null)
        {
            throw new NotFoundException($"The task by id {taskId} was not found.");
        }

        var statusToUpdate = dto.GetStatus();
        
        var nextStatus = task.Status.GetNext();
        var canUpdateStatus = nextStatus == statusToUpdate;
        if (!canUpdateStatus)
        {
            throw new ConflictException($"The task by id {taskId} can't be updated to {statusToUpdate}.");
        }
        
        var isTaskCompleted = statusToUpdate == TaskStatus.Completed;
        if (!isTaskCompleted)
        {
            await UpdateTaskStatusAsync(task, statusToUpdate, ct);
            return;
        }

        await using var transaction = await dbContext.BeginTransactionAsync(capPublisher, ct);
        
        await UpdateTaskStatusAsync(task, statusToUpdate, ct);

        var @event = TaskEvents.Completed.Create(taskId);
        await capPublisher.PublishAsync(@event.EventName, @event, cancellationToken: ct);
        
        await transaction.CommitAsync(ct);
    }

    public async Task<IList<TaskDto>> GetTasksAsync(CancellationToken ct = default)
    {
        var tasks = await dbContext.Tasks.AsNoTracking().ToListAsync(ct);

        var results = tasks.Select(x => new TaskDto
            {
                TaskId = x.Id,
                Description = x.Description,
                Name = x.Name,
                Status = x.Status
            })
            .ToList();
        
        return results;
    }
    
    private async Task UpdateTaskStatusAsync(TaskEntity task, TaskStatus status, CancellationToken ct = default)
    {
        task.Status = status;
        await dbContext.SaveChangesAsync(ct);
    }
}