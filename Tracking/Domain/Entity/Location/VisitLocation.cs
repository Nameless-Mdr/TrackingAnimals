using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Location;

[Table("visit_locations", Schema = "info")]
public class VisitLocation
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    
    [Column("date_time_of_visit_location_point")]
    public DateTimeOffset DateTimeOfVisitLocationPoint { get; set; }
    
    [Column("animal_id")]
    public long AnimalId { get; set; }
    
    [Column("location_point_id")]
    public long LocationPointId { get; set; }


    [ForeignKey($"{nameof(AnimalId)}")]
    public virtual Animal.Animal Animal { get; set; } = null!;
    
    [ForeignKey($"{nameof(LocationPointId)}")]
    public virtual Location Location { get; set; } = null!;
}