namespace TaskList5;

using Microsoft.EntityFrameworkCore;

public class AppContext : DbContext
{
    public DbSet<User> Users { get; set; } = null!;
    public DbSet<Task> Tasks { get; set; } = null!;

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.EnableSensitiveDataLogging();
        optionsBuilder.UseNpgsql(
            "Host=localhost;Port=5432;Database=testDb;username=postgres;Password=Qwary123;Include Error Detail=True");
        base.OnConfiguring(optionsBuilder);
    }
}