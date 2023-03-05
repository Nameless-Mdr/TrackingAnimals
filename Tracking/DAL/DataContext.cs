using Domain.Entity.Animal;
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
        // creating users
        modelBuilder.Entity<User>()
            .HasIndex(f => f.Email)
            .IsUnique();
        modelBuilder.Entity<User>().HasMany(a => a.Animals).WithOne(u => u.User).OnDelete(DeleteBehavior.Restrict);

        // creating locations
        modelBuilder.Entity<Location>().HasIndex(l => new { l.Latitude, l.Longitude }).IsUnique();
        modelBuilder.Entity<Location>()
            .HasCheckConstraint(nameof(Location.Latitude),
                "latitude >= -90 AND latitude <= 90",
                n => n.HasName("CH_latitude"))
            .HasCheckConstraint(nameof(Location.Longitude),
                "longitude >= -180 AND longitude <= 180",
                n => n.HasName("CH_longitude"));
        modelBuilder.Entity<Location>().HasMany(a => a.Animals).WithOne(l => l.Location)
            .OnDelete(DeleteBehavior.Restrict);
        
        // creating animals
        modelBuilder.Entity<Animal>()
            .HasCheckConstraint(nameof(Animal.Gender),
                "gender IN ('MALE', 'FEMALE', 'OTHER')", n => n.HasName("CH_gender"))
            .HasCheckConstraint(nameof(Animal.LifeStatus),
                "life_status IN ('ALIVE', 'DEAD')", n => n.HasName("CH_life_status"))
            .HasCheckConstraint(nameof(Animal.Weight),
                "weight > 0", n => n.HasName("CH_weight"))
            .HasCheckConstraint(nameof(Animal.Length),
                "length > 0", n => n.HasName("CH_length"))
            .HasCheckConstraint(nameof(Animal.Height),
                "height > 0", n => n.HasName("CH_height"));
        modelBuilder.Entity<Animal>().Property(l => l.LifeStatus).HasDefaultValue("ALIVE");
        
        // creating type
        modelBuilder.Entity<TypeAnimal>().HasIndex(t => t.Type).IsUnique();

        // creating types_animals
        modelBuilder.Entity<Animal>().HasMany(t => t.Types).WithMany(a => a.Animals)
            .UsingEntity<Dictionary<string, object>>(
                x => x.HasOne<TypeAnimal>().WithMany().OnDelete(DeleteBehavior.Restrict),
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

    public DbSet<TypeAnimal> Types => Set<TypeAnimal>();
}