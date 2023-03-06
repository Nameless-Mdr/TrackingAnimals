using Domain.Entity.Animal;
using Domain.Entity.Location;
using Domain.Entity.User;
using Microsoft.EntityFrameworkCore;
using Type = Domain.Entity.Animal.Type;

namespace DAL;

public class DataContext : DbContext
{
    public DataContext(DbContextOptions<DataContext> optionts) : base(optionts)
    {
        
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        // creating users
        modelBuilder.Entity<User>()
            .HasIndex(f => f.Email)
            .IsUnique();
        modelBuilder.Entity<User>().HasMany(a => a.Animals).WithOne(u => u.User).OnDelete(DeleteBehavior.Restrict);

        // creating locations
        modelBuilder.Entity<Location>().HasIndex(l => new { l.Latitude, l.Longitude }).IsUnique();
        modelBuilder.Entity<Location>()
            .ToTable(t => t.HasCheckConstraint("CH_latitude", "latitude >= -90 AND latitude <= 90"))
            .ToTable(t => t.HasCheckConstraint("CH_longitude", "longitude >= -180 AND longitude <= 180"));
        modelBuilder.Entity<Location>().HasMany(a => a.Animals).WithOne(l => l.Location)
            .OnDelete(DeleteBehavior.Restrict);
        
        // creating animals
        modelBuilder.Entity<Animal>()
            .ToTable(t => t.HasCheckConstraint("CH_gender", "gender IN ('MALE', 'FEMALE', 'OTHER')"))
            .ToTable(t => t.HasCheckConstraint("CH_life_status", "life_status IN ('ALIVE', 'DEAD')"))
            .ToTable(t => t.HasCheckConstraint("CH_weight", "weight > 0"))
            .ToTable(t => t.HasCheckConstraint("CH_length", "length > 0"))
            .ToTable(t => t.HasCheckConstraint("CH_height", "height > 0"));
        modelBuilder.Entity<Animal>().Property(l => l.LifeStatus).HasDefaultValue("ALIVE");
        
        // creating type
        modelBuilder.Entity<Type>().HasIndex(t => t.NameType).IsUnique();

        // creating types_animals
        modelBuilder.Entity<Animal>().HasMany(t => t.Types).WithMany(a => a.Animals)
            .UsingEntity<Dictionary<string, object>>(
                x => x.HasOne<Type>().WithMany().OnDelete(DeleteBehavior.Restrict),
                x => x.HasOne<Animal>().WithMany().OnDelete(DeleteBehavior.Cascade),
                p => p.ToTable("types_animals", "info"));
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseNpgsql(b => b.MigrationsAssembly("Tracking"));
    }
    
    public DbSet<User> Users => Set<User>();
    
    public DbSet<UserSession> UserSessions => Set<UserSession>();
    
    public DbSet<Location> Locations => Set<Location>();

    public DbSet<Animal> Animals => Set<Animal>();

    public DbSet<Type> Types => Set<Type>();

    public DbSet<VisitLocation> VisitLocations => Set<VisitLocation>();
}