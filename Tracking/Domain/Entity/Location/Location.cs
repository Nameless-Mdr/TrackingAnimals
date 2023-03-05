using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Location;

[Table("locations", Schema = "info")]
public class Location
{
    [Column("id")]
    public int Id { get; set; }

    [Column("latitude")] 
    public double Latitude { get; set; }

    [Column("longitude")] 
    public double Longitude { get; set; }
    
    
    public virtual ICollection<Animal.Animal>? Animals { get; set; }
}