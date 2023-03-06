using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Animal;

[Table("types_animals", Schema = "info")]
public class TypesAnimals
{
    [Column("animal_id")]
    public long AnimalId { get; set; }
    
    [Column("type_id")]
    public long TypeId { get; set; }
}