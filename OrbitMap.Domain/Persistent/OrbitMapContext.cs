using System.Reflection;
using Contracts.Domains.Interface;
using Microsoft.EntityFrameworkCore;
using OrbitMap.Domain.Entities;

namespace OrbitMap.Domain.Persistent;

public class OrbitMapContext : DbContext
{
    public OrbitMapContext()
    {
    }
    
    public OrbitMapContext(DbContextOptions<OrbitMapContext> options) : base(options)
    {
    }
    
    public DbSet<User> User { get; set; }
    public DbSet<Friendship> Friendship { get; set; }
    public DbSet<Role> Role { get; set; }
    public DbSet<Message> Message { get; set; }
    public DbSet<LastMessageChat> LastMessageChat { get; set; }
    public DbSet<Group> Group { get; set; }
    public DbSet<Connection> Connection { get; set; }
    public DbSet<Story> Story { get; set; }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
        
        base.OnModelCreating(modelBuilder);
    }
    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        var modified = ChangeTracker.Entries()
            .Where(e => e.State == EntityState.Modified ||
                        e.State == EntityState.Added ||
                        e.State == EntityState.Deleted);

        foreach (var item in modified)
        {
            switch (item.State)
            {
                case EntityState.Added:
                    if(item.Entity is IDateTracking addedEntity)
                    {
                        addedEntity.CreatedDate = DateTime.Now;
                        item.State = EntityState.Added;
                    }
                    break;
                case EntityState.Modified:
                    if(item.Entity is IDateTracking modifiedEntity)
                    {
                        Entry(item.Entity).Property("Id").IsModified = false;
                        modifiedEntity.LastModifiedDate = DateTime.Now;
                        item.State = EntityState.Modified;
                    }
                    break;
            }
        }
        var result = await base.SaveChangesAsync(cancellationToken);
        return result;
    }
}