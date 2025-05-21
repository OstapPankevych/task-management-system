using Tms.Services.Tasks.DTOs;
using TaskStatus = Tms.Common.Enums.TaskStatus;

namespace Tms.Services.Tasks;

public interface ITaskService
{
    Task<TaskCreatedDto> CreateTaskAsync(CreateTaskDto dto, CancellationToken ct = default);
    Task UpdateTaskStatusAsync(int taskId, UpdateTaskStatusDto dto, CancellationToken ct = default);
    Task<IList<TaskDto>> GetTasksAsync(CancellationToken ct = default);
}