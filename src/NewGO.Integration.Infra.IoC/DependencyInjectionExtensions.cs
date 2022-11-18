using Microsoft.Extensions.DependencyInjection;

namespace NewGO.Integration.Infra.IoC
{
    public static class DependencyInjectionExtensions
    {
        public static void AddDependencyInjection(this IServiceCollection services)
        {
            NativeInjectorBootStrapper.RegisterServices(services);
        }
    }
}