using BLL.Service.Implements;
using BLL.Service.Interfaces;
using DAL.Interfaces;
using DAL.Repositories;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Service;

public class ServiceModule : IModule
{
    public void Registry(IServiceCollection services)
    {
        // сервисы пользователя
        services.AddTransient<IUserRepo, UserRepo>();
        services.AddTransient<IUserService, UserService>();

        // сервисы аутентификации
        services.AddTransient<IAuthService, AuthService>();

        // сервисы сессий
        services.AddTransient<ISessionRepo, SessionRepo>();
        services.AddTransient<ISessionService, SessionService>();
        
        // сервисы локаций
        services.AddTransient<ILocationRepo, LocationRepo>();
        services.AddTransient<ILocationService, LocationService>();
        
        // сервисы типов животных
        services.AddTransient<ITypeRepo, TypeRepo>();
        services.AddTransient<ITypeService, TypeService>();
        
        // сервисы животных
        services.AddTransient<IAnimalRepo, AnimalRepo>();
        services.AddTransient<IAnimalService, AnimalService>();
    }
}