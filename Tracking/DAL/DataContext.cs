using Domain.Entity.Location;
using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;

namespace DAL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> optionts) : base(optionts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // creating user_sessions
        modelBuilder.Entity<UserSession>().ToTable(nameof(UserSessions).ToLower(), "auth");

        modelBuilder.Entity<UserSession>()
            .Property(s => s.UserId).IsRequired();
        
        // creating users
        modelBuilder.Entity<User>().ToTable(nameof(Users).ToLower(), "auth");
        
        modelBuilder.Entity<User>()
            .HasIndex(f => f.Email)
            .IsUnique();

        // creating locations
        modelBuilder.Entity<Location>().ToTable(nameof(Locations).ToLower(), "info");
        
        modelBuilder.Entity<Location>()
            .HasCheckConstraint(nameof(Location.Latitude),
                $"{nameof(Location.Latitude)} >= -90 AND {nameof(Location.Latitude)} <= 90")
            .HasCheckConstraint(nameof(Location.Longitude),
                $"{nameof(Location.Longitude)} >= -180 AND {nameof(Location.Longitude)} <= 180");
        
        modelBuilder.Entity<Location>()
            .HasIndex(l => new {l.Latitude, l.Longitude})
            .IsUnique();
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(b => b.MigrationsAssembly("Tracking"));
    }
    
    public DbSet<User> Users => Set<User>();
    
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    
    public DbSet<Location> Locations => Set<Location>();
}