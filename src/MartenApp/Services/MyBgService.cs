using Marten;
using Marten.Events;
using Marten.Events.Aggregation;
using static System.Formats.Asn1.AsnWriter;

namespace CasCap.Services;

public class MyBgService : BackgroundService
{
    readonly ILogger _logger;
    readonly IDocumentStore _store;

    public MyBgService(ILogger<MyBgService> logger, IDocumentStore store)
    {
        _logger = logger;
        _store = store;
    }

    protected async override Task ExecuteAsync(CancellationToken cancellationToken)
    {
        _logger.LogInformation("starting...");
        await Task.Delay(0, cancellationToken);


        await using (var session = _store.OpenSession())
        {
            // Start a brand new stream and commit the new events as part of a transaction
            //session.Events.StartStream<Quest>(questId, started, joined1);

            // Start a brand new stream
            //var stream_id_location = Guid.NewGuid();
            var stream_id_location = Guid.Parse("9f8ab4b5-7265-4b3d-85ae-22429337de9f");
            while (true)
            {
                _logger.LogInformation("iterating...");
                await Task.Delay(1_000, cancellationToken);

                for (var i = 1; i < 10_000; i++)
                {
                    // Append more events to the same stream

                    var e = new TemperatureLogged(DateTime.Now, i);
                    var a = session.Events.Append(stream_id_location, e);
                    
                    // Save the pending changes to db
                    await session.SaveChangesAsync(cancellationToken);
                    _logger.LogInformation($"added event {@e} to stream {stream_id_location}");

                    await Task.Delay(1_000, cancellationToken);

                    //this is a live projection
                    var l1 = await session.Events.AggregateStreamAsync<P1>(stream_id_location);
                    _logger.LogInformation($"here is projection RoomTemp1={l1.RoomTemp1}");

                    //this is a live projection
                    var l2 = await session.Events.AggregateStreamAsync<P2>(stream_id_location);
                    _logger.LogInformation($"here is MyProjector TotalReadings={l2.TotalReadings}");

                }
            }
        }
    }
}

public record TemperatureLogged(DateTime DateTimeUtc, double temperature);


//this is a projection
public class P1
{
    public Guid Id { get; set; }
    public double RoomTemp1 { get; set; }

    public void Apply(TemperatureLogged e)
    {
        RoomTemp1 = e.temperature;
    }
}

public class P2
{
    public Guid Id { get; set; }
    public long TotalReadings { get; set; }
}

public class MyProjector: SingleStreamAggregation<P2>
{
    public void Apply(P2 snapshot, TemperatureLogged e)
    {
        snapshot.TotalReadings++;
    }
}