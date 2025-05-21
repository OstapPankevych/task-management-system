using Microsoft.EntityFrameworkCore;
using Tms.Db;
using Tms.Services.Tasks.Events.Handlers;

namespace Tms.WebApi.DI;

public static class SetupInfrastructure
{
    public static IServiceCollection AddDataBase(this IServiceCollection services, string dbConnectionString)
    {
        services.AddDbContext<TmsDbContext>(options =>
            options.UseNpgsql(dbConnectionString));
        
        services.AddScoped<ITmsDbContext, TmsDbContext>();
        
        return services;
    }
    
    public static IServiceCollection AddCap(this IServiceCollection services, bool isDevelopment, string dbConnectionString, 
        RabbitMqOptions rabbitMqOptions)
    {
        services.AddTransient<TaskEventHandler>();
        
        services.AddCap(options =>
        {
            options.UsePostgreSql(dbConnectionString);
            
            options.UseRabbitMQ(rabbit =>
            {
                rabbit.HostName = rabbitMqOptions.HostName;
                rabbit.Port = rabbitMqOptions.Port;
                rabbit.UserName = rabbitMqOptions.UserName;
                rabbit.Password = rabbitMqOptions.Password;
            });

            options.UseDashboard();
        });
        
        return services;
    }
}