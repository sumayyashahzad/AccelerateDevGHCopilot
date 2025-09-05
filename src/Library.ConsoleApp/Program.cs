using Library.Infrastructure.Data;

var builder = Host.CreateDefaultBuilder(args)
    .ConfigureServices((hostContext, services) =>
    {
        services.AddSingleton<JsonData>();
        services.AddTransient<ConsoleApp>();
    });