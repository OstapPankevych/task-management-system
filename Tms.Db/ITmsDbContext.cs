using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tms.Db.Entities;

namespace Tms.Db;

public interface ITmsDbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }
    
    public Task SaveChangesAsync(CancellationToken ct = default);
    public Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus, CancellationToken ct = default);
}