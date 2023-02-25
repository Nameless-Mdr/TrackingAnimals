using AutoMapper;
using Common;
using Domain.DTO.Location;
using Domain.DTO.User;
using Domain.Entity.Location;
using Domain.Entity.User;

namespace Tracking.Mapper;

public class MapperProfile : Profile
{
    public MapperProfile()
    {
        // Мапинг модели пользователя
        CreateMap<CreateUserModel, User>()
            .ForMember(d => d.PasswordHash, m
                => m.MapFrom(s => HashHelper.GetHash(s.Password)));

        CreateMap<User, GetUserModel>();
        
        // Мапинг модели локации
        CreateMap<CreateLocationModel, Location>();
    }
}