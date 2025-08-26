using CasCap.Services;
using Marten;
using Serilog;
using Serilog.Sinks.SystemConsole.Themes;
using System.Runtime.InteropServices;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMarten(options =>
{
    var connString = builder.Configuration.GetConnectionString("Marten");
    options.Connection(connString);

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    //if (Environment.IsDevelopment())
    {
        options.AutoCreateSchemaObjects = JasperFx.AutoCreate.All;
    }
});

builder.Services.AddHostedService<MyBgService>();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(theme: RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? SystemConsoleTheme.Literate : AnsiConsoleTheme.Code, applyThemeToRedirectedOutput: true)
    );

var app = builder.Build();
app.MapGet("/", () => "hello event sourcing");
await app.RunAsync();