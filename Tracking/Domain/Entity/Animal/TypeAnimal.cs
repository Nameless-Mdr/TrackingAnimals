using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Animal;

[Table("types", Schema = "info")]
public class TypeAnimal
{
    [Column("id")]
    public int Id { get; set; }

    [Column("type")] 
    public string Type { get; set; } = null!;

    public virtual ICollection<Animal> Animals { get; set; } = null!;
}