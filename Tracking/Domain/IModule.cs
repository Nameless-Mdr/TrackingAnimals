using Microsoft.Extensions.DependencyInjection;

namespace Domain;

public interface IModule
{
    public void Registry(IServiceCollection services);
}