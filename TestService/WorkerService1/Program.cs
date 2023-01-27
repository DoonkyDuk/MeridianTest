using WorkerService1;

IHost host = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        IConfiguration configuration = hostContext.Configuration;
        services.Configure<SenderConfiguration>(configuration.GetSection("SenderConfiguration"));
        services.AddHostedService<Worker>();
    })
    .UseWindowsService()
    .Build();

await host.RunAsync();
