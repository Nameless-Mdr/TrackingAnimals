using AutoMapper;
using Common;
using Domain.DTO.Animal;
using Domain.DTO.Location;
using Domain.DTO.TypeAnimal;
using Domain.DTO.User;
using Domain.DTO.VisitLocation;
using Domain.Entity.Animal;
using Domain.Entity.Location;
using Domain.Entity.User;
using Type = Domain.Entity.Animal.Type;

namespace Tracking.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Маппинг модели пользователя
        CreateMap<CreateUserModel, User>()
            .ForMember(d => d.PasswordHash, m
                => m.MapFrom(s => HashHelper.GetHash(s.Password)));
        CreateMap<User, GetUserModel>();
        
        // Маппинг модели локации
        CreateMap<CreateLocationModel, Location>();
        CreateMap<Location, GetLocationModel>();
        
        // Маппинг модели типы животного
        CreateMap<CreateTypeModel, Type>();
        CreateMap<Type, GetTypeModel>();

        // Маппинг модели животного
        CreateMap<CreateAnimalModel, Animal>()
            .ForMember(d => d.ChippingDateTime, m
                => m.MapFrom(s => DateTimeOffset.UtcNow));
        CreateMap<Animal, GetAnimalModel>();
        CreateMap<UpdateAnimalModel, Animal>()
            .ForMember(d => d.Gender, m
                => m.MapFrom(s => s.Gender.ToUpper()))
            .ForMember(d => d.LifeStatus, m
                => m.MapFrom(s => s.LifeStatus.ToUpper()))
            .ForMember(d => d.DeathDateTime, m
                => m.MapFrom(s => s.LifeStatus.ToUpper() == "DEAD" ? DateTimeOffset.UtcNow : (DateTimeOffset?)null));
        
        // Маппинг модели локации посещенной животным
        CreateMap<CreateVisitLocationModel, VisitLocation>()
            .ForMember(d => d.DateTimeOfVisitLocationPoint, m
                => m.MapFrom(s => DateTimeOffset.UtcNow));
        CreateMap<VisitLocation, GetVisitLocationModel>();
        CreateMap<UpdateVisitLocationModel, VisitLocation>();
    }
}