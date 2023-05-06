using Authentication.Implements;
using Authentication.Interfaces;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace Authentication;

public class ServiceAuth : IModule
{
    public void Registry(IServiceCollection services)
    {
        services.AddTransient<IAuthService, AuthService>();
    }
}