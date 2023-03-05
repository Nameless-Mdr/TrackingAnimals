using System.ComponentModel.DataAnnotations;

namespace Domain.DTO.TypeAnimal;

public class CreateTypeModel
{
    [MinLength(3)]
    public string Type { get; set; } = null!;
}