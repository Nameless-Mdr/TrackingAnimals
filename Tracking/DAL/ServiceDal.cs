using DAL.Interfaces;
using DAL.Repositories;
using Domain;
using Microsoft.Extensions.DependencyInjection;

namespace DAL;

public class ServiceDal : IModule
{
    public void Registry(IServiceCollection services)
    {
        services.AddTransient<IUserRepo, UserRepo>();
        
        services.AddTransient<ISessionRepo, SessionRepo>();
        
        services.AddTransient<ILocationRepo, LocationRepo>();
        
        services.AddTransient<ITypeRepo, TypeRepo>();
        
        services.AddTransient<IAnimalRepo, AnimalRepo>();
        
        services.AddTransient<IVisitLocationRepo, VisitLocationRepo>();
    }
}