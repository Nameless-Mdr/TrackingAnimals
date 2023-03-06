namespace Domain.DTO.VisitLocation;

public class GetVisitLocationModel : UpdateVisitLocationModel
{
    public DateTimeOffset DateTimeOfVisitLocationPoint { get; set; }
}