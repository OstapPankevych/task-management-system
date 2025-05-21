using DotNetCore.CAP;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Tms.Db.Entities;

namespace Tms.Db;

public class TmsDbContext(DbContextOptions<TmsDbContext> options) 
    : DbContext(options), ITmsDbContext
{
    public DbSet<TaskEntity> Tasks { get; set; }

    public new async Task SaveChangesAsync(CancellationToken ct = default) =>
        await base.SaveChangesAsync(ct);

    public async Task<IDbContextTransaction> BeginTransactionAsync(ICapPublisher capBus, CancellationToken ct = default) => 
        await base.Database.BeginTransactionAsync(capBus, false, ct);
}