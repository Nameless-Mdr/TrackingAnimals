using BLL.Service.Implements;
using BLL.Service.Interfaces;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace BLL.Service;

public class ServiceModule : IModule
{
    public void Registry(IServiceCollection services)
    {
        services.AddTransient<IUserService, UserService>();

        services.AddTransient<ISessionService, SessionService>();
        
        services.AddTransient<ILocationService, LocationService>();

        services.AddTransient<ITypeService, TypeService>();
        
        services.AddTransient<IAnimalService, AnimalService>();
        
        services.AddTransient<IVisitLocationService, VisitLocationService>();
    }
}