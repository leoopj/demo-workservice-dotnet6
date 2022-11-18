using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using NewGO.Integration.Model.Map;
using NLog.Extensions.Logging;

namespace NewGO.Integration.Test.Helper
{
    public class HelperTest
    {
        public readonly IConfiguration? Configuration = null;
        public readonly ILogger? Log = null;
        public readonly HttpContextAccessor HttpContext;
        public ServiceProvider? Services = null;

        public HelperTest()
        {
            Configuration = InitConfiguration();
            Log = InitLog();

            HttpContext = new HttpContextAccessor();
            HttpContext.HttpContext = new DefaultHttpContext();
            var user = new System.Security.Claims.ClaimsPrincipal();
            var identity = new System.Security.Claims.ClaimsIdentity();

            identity.AddClaim(new System.Security.Claims.Claim("Username", "test.sco2"));
            identity.AddClaim(new System.Security.Claims.Claim("Id", "1"));
            user.AddIdentity(identity);
            HttpContext.HttpContext.User = new System.Security.Claims.ClaimsPrincipal(user);
        }

        private IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("appsettings.test.json")
                .Build();
            return config;
        }

        private ILogger InitLog()
        {
            Services = new ServiceCollection()
                .AddLogging(builder =>
                {
                    builder.AddConfiguration(Configuration.GetSection("Logging"));
                    builder.AddConsole();
                    builder.AddEventSourceLogger();
                    builder.AddNLog();
                })
                .AddOptions()
                .AddSingleton(new MapperConfiguration(cfg => { cfg.AddProfile(new ConfigMap()); }).CreateMapper())
                .BuildServiceProvider();

            return Services.GetService<ILoggerFactory>().CreateLogger(typeof(HelperTest));
        }
    }
}
