namespace WorkerService1
{
    using Microsoft.Extensions.Options;
    using System.Net;
    using TestService;
    public class Worker : BackgroundService
    {
        private readonly ILogger<Worker> _logger;
        private readonly IOptions<SenderConfiguration> _options;
        private DataSender dataSender;

        public Worker(ILogger<Worker> logger, IOptions<SenderConfiguration> options)
        {
            _logger = logger;
            _options = options;
            dataSender = new DataSender(new IPEndPoint(IPAddress.Parse(_options.Value.IPAddress), Int32.Parse(_options.Value.Port)));
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                await dataSender.Start();
            }
        }
    }
}