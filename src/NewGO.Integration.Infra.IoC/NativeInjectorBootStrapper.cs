using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;

namespace NewGO.Integration.Infra.IoC
{
    internal class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
        }
    }
}
