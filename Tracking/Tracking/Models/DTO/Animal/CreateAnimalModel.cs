namespace Domain.DTO.Animal;

public class CreateAnimalModel
{
    public long[] TypesId { get; set; } = null!;
    
    public double Weight { get; set; }
    
    public double Length { get; set; }
    
    public double Height { get; set; }

    public string Gender { get; set; } = null!;
    
    public int ChipperId { get; set; }
    
    public long ChippingLocationId { get; set; } 
}