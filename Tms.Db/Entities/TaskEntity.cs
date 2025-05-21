using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using TaskStatus = Tms.Common.Enums.TaskStatus;

namespace Tms.Db.Entities;

[Table("Tasks")]
public class TaskEntity
{
    [Key]
    public int Id { get; set; }
    
    public string Name { get; set; } = null!;
    
    public string Description { get; set; } = null!;
    
    [EnumDataType(typeof(TaskStatus))]
    public TaskStatus Status { get; set; }
}