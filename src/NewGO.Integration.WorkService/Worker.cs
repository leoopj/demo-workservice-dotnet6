using Microsoft.Extensions.Options;
using NewGO.Integration.Infra;

namespace NewGO.Integration.Senior
{
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private static readonly Timer? _timer = null;
        private readonly ServiceConfiguration _serviceConfiguration;

        public Worker(ILogger<Worker> logger, IConfiguration configuration)
        {
            _logger = logger;

            _serviceConfiguration = new ServiceConfiguration();
            new ConfigureFromConfigurationOptions<ServiceConfiguration>(configuration.GetSection("ServiceConfigurations"))
                .Configure(_serviceConfiguration);
        }

        public override Task StartAsync(CancellationToken cancellationToken)
        {
            _logger.LogInformation(" =========================================== SERVICE STARTED ===========================================");
            return base.StartAsync(cancellationToken);
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("Worker running at: {time}", DateTimeOffset.Now);
                await Task.Delay(1000, stoppingToken);
            }
        }

        public override Task StopAsync(CancellationToken cancellationToken)
        {
            _timer?.Change(Timeout.Infinite, 0);
            _logger.LogInformation("============================================ SERVICE STOPPED =============================================");

            return base.StopAsync(cancellationToken);
        }
    }
}