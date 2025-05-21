using Microsoft.AspNetCore.Mvc;
using Tms.Services.Tasks;
using Tms.Services.Tasks.DTOs;

namespace Tms.WebApi.Endpoints;

public static class TaskApis
{
    public static WebApplication MapTaskApis(this WebApplication app)
    {
        app.MapGet("api/tasks", async (ITaskService taskService, CancellationToken ct) =>
        {
            var data = await taskService.GetTasksAsync(ct);
            return data;
        });
        
        app.MapPost("api/tasks", async ([FromBody] CreateTaskDto dto, ITaskService taskService, CancellationToken ct) =>
        {
            var data = await taskService.CreateTaskAsync(dto, ct);
            return data;
        });
        
        app.MapPatch("api/tasks/{taskId:int}/status", async (int taskId, [FromBody] UpdateTaskStatusDto dto, ITaskService taskService, 
            CancellationToken ct) =>
        {
            await taskService.UpdateTaskStatusAsync(taskId, dto, ct);
            return Results.NoContent();
        });

        return app;
    }
}