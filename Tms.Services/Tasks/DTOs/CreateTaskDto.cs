using System.ComponentModel.DataAnnotations;

namespace Tms.Services.Tasks.DTOs;

public class CreateTaskDto
{
    [Required]
    [MinLength(1)]
    public string Description { get; set; } = null!;
    
    [Required]
    [MinLength(1)]
    public string Name { get; set; } = null!;
}