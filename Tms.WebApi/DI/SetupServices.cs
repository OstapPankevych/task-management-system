using Tms.Services.Tasks;

namespace Tms.WebApi.DI;

public static class SetupServices
{
    public static IServiceCollection AddServices(this IServiceCollection services)
    {
        services.AddScoped<ITaskService, TaskService>();

        return services;
    }
}