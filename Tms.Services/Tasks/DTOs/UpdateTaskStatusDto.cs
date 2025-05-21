using Tms.Common.ValidationAttributes;
using TaskStatus = Tms.Common.Enums.TaskStatus;

namespace Tms.Services.Tasks.DTOs;

public class UpdateTaskStatusDto
{
    [AllowedEnumValues<TaskStatus>]
    public string Status { get; set; } = null!;

    public TaskStatus GetStatus() => Enum.Parse<TaskStatus>(Status, ignoreCase: true);
}