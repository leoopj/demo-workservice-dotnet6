using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.DependencyInjection;


namespace NewGO.Integration.Infra
{
    public static class ProviderFactory
    {
        private static IServiceProvider _serviceProvider;
        private static IHostEnvironment _environment;

        public static void SetEnvironment(IHostEnvironment environment)
        {
            _environment = environment;
        }

        public static void SetProvider(IServiceProvider serviceProvider)
        {
            _serviceProvider = serviceProvider;
        }

        public static IHostEnvironment GetEnvironment()
        {
            return _environment;
        }

        public static T GetService<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
