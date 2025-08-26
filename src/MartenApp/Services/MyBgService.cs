using Marten;

namespace CasCap.Services;

public class MyBgService : BackgroundService
{
    private readonly ILogger _logger;
    private readonly IDocumentStore _store;

    public MyBgService(ILogger<MyBgService> logger, IDocumentStore store)
    {
        _logger = logger;
        _store = store;
    }

    protected async override Task ExecuteAsync(CancellationToken stoppingToken)
    {
        _logger.LogInformation("starting...");
        await Task.Delay(0, stoppingToken);

        await using (var session = _store.OpenSession(new Marten.Services.SessionOptions() { }))
        {
            // Start a brand new stream and commit the new events as part of a transaction
            //session.Events.StartStream<Quest>(questId, started, joined1);

            // Start a brand new stream
            var stream_id_location = Guid.NewGuid();
            while (!stoppingToken.IsCancellationRequested)
            {
                _logger.LogInformation("iterating...");
                await Task.Delay(1_000, stoppingToken);

                for (var i = 1; i < 10_000; i++)
                {
                    // Append more events to the same stream

                    session.Events.Append(stream_id_location, new TemperatureLogged(DateTime.Now, i));
                    // Save the pending changes to db
                    await session.SaveChangesAsync();
                    _logger.LogInformation("added event to stream {StreamIdLocation}", stream_id_location);

                    await Task.Delay(1_000, stoppingToken);

                }
            }
        }
    }
}

public record TemperatureLogged(DateTime DateTimeUtc, double temperature);
