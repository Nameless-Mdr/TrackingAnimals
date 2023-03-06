namespace Domain.DTO.Animal;

public class UpdateAnimalModel
{
    public long Id { get; set; }
    
    public double Weight { get; set; }
    
    public double Length { get; set; }
    
    public double Height { get; set; }

    public string Gender { get; set; } = null!;

    public string LifeStatus { get; set; } = null!;
    
    public int ChipperId { get; set; }
    
    public long ChippingLocationId { get; set; }
}