using TaskStatus = Tms.Common.Enums.TaskStatus;

namespace Tms.Services.Tasks.DTOs;

public class TaskDto
{
    public int TaskId { get; set; }
    public string Description { get; set; } = null!;
    public string Name { get; set; } = null!;
    public TaskStatus Status { get; set; }
}