using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Location;

public class Location
{
    [Column("id")]
    public int Id { get; set; }

    [Column("latitude")] 
    public double Latitude { get; set; }

    [Column("longitude")] 
    public double Longitude { get; set; }
}