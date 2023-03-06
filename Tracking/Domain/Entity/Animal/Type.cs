using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entity.Animal;

[Table("types", Schema = "info")]
public class Type
{
    [Column("id")]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }

    [Column("name_type")] 
    public string NameType { get; set; } = null!;

    
    public virtual ICollection<Animal> Animals { get; set; } = null!;
}