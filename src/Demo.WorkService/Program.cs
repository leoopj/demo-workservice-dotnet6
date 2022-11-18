using NewGO.Integration.Senior;
using AutoMapper;
using NewGO.Integration.Model.Map;
using NewGO.Integration.Infra.IoC;
using NLog.Extensions.Logging;
using System.Reflection;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureAppConfiguration((hostContext, configApp) =>
    {
        configApp.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
        configApp.AddEnvironmentVariables(prefix: "PREFIX_");
        configApp.AddCommandLine(args);

        NewGO.Integration.Infra.ProviderFactory.SetEnvironment(hostContext.HostingEnvironment);
    })
    .ConfigureServices((hostContext, services) =>
    {
        services.AddHostedService<Worker>();
        services.AddSingleton(new MapperConfiguration(cfg => { cfg.AddProfile(new ConfigMap()); }).CreateMapper());
        services.AddDependencyInjection();

        IServiceProvider _serviceProvider = services
                   .AddLogging(builder =>
                   {
                       builder.AddConfiguration(hostContext.Configuration.GetSection("Logging"));
                       builder.AddConsole();
                       builder.AddDebug();
                       builder.AddEventSourceLogger();
                       builder.AddNLog();
                   })
                   .AddAutoMapper(Assembly.GetAssembly(typeof(ConfigMap)))
                   .BuildServiceProvider();

        NewGO.Integration.Infra.ProviderFactory.SetProvider(_serviceProvider);

    })
    .Build();

await host.RunAsync();
