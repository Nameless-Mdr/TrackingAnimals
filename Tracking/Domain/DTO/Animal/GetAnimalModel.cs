using Domain.DTO.TypeAnimal;

namespace Domain.DTO.Animal;

public class GetAnimalModel
{
    public long Id { get; set; }

    public ICollection<GetTypeModel> Types { get; set; } = null!;
    
    public double Weight { get; set; }
    
    public double Length { get; set; }
    
    public double Height { get; set; }

    public string Gender { get; set; } = null!;

    public string LifeStatus { get; set; } = null!;
    
    public DateTimeOffset ChippingDateTime { get; set; }
    
    public int ChipperId { get; set; }
    
    public long ChippingLocationId { get; set; }
    
    public DateTimeOffset? DeathDateTime { get; set; }
}