namespace CasCap.Services;

public class MyBgService : BackgroundService
{
    readonly ILogger _logger;
    //readonly IDocumentSession _session;

    public MyBgService(ILogger<MyBgService> logger/*, IDocumentSession session*/)
    {
        _logger = logger;
        //_session = session;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("starting...");
        await Task.Delay(0, stoppingToken);


        while (true)
        {
            _logger.LogInformation("iterating...");
            await Task.Delay(1_000, stoppingToken);
        }


    }
}