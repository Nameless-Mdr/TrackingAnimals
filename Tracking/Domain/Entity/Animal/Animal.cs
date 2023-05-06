using System.ComponentModel.DataAnnotations.Schema;
using Domain.Entity.Location;
using Domain.Enum;

namespace Domain.Entity.Animal;

[Table("animals", Schema = "info")]
public class Animal
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("weight")] 
    public double Weight { get; set; }

    [Column("length")]
    public double Length { get; set; }

    [Column("height")]
    public double Height { get; set; }

    [Column("gender")]
    public string Gender { get; set; } = null!;

    [Column("life_status")] 
    public string LifeStatus { get; set; } = null!;
    
    [Column("chipper_id")]
    public int ChipperId { get; set; }
    
    [Column("chipping_location_id")]
    public long ChippingLocationId { get; set; }
    
    [Column("chipping_date_time")]
    public DateTimeOffset ChippingDateTime { get; set; }
    
    [Column("death_date_time")]
    public DateTimeOffset? DeathDateTime { get; set; }
    
    public virtual ICollection<Type> Types { get; set; } = null!;

    public virtual ICollection<VisitLocation>? VisitLocations { get; set; }

    [ForeignKey($"{nameof(ChipperId)}")]
    public virtual User.User User { get; set; } = null!;

    [ForeignKey($"{nameof(ChippingLocationId)}")]
    public virtual Location.Location Location { get; set; } = null!;
}