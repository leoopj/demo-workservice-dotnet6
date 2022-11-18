using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NewGO.Integration.Application;
using NewGO.Integration.Model;

namespace NewGO.Integration.Infra.IoC
{
    internal class NativeInjectorBootStrapper
    {
        public static void RegisterServices(IServiceCollection services)
        {
            try
            {
                services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();
                services.AddScoped<IHelloWordSvc, HelloWordSvc>();
            }
            catch (Exception)
            {

                throw;
            }
            
        }
    }
}
