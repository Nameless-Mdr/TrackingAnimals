using AutoMapper;
using Common;
using Domain.DTO.Location;
using Domain.DTO.TypeAnimal;
using Domain.DTO.User;
using Domain.Entity.Animal;
using Domain.Entity.Location;
using Domain.Entity.User;

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
        CreateMap<CreateTypeModel, TypeAnimal>();
        CreateMap<TypeAnimal, GetTypeModel>();
        
        // Маппинг модели животного
    }
}