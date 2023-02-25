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
        // 
        services.AddTransient<IUserRepo, UserRepo>();

        services.AddTransient<IUserService, UserService>();

        // 
        services.AddTransient<IAuthService, AuthService>();

        //
        services.AddTransient<ISessionRepo, SessionRepo>();

        services.AddTransient<ISessionService, SessionService>();
        
        //
        services.AddTransient<ILocationRepo, LocationRepo>();

        services.AddTransient<ILocationService, LocationService>();
    }
}