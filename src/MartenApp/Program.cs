using CasCap.Services;
using Marten;
using Serilog;
//using Weasel.Core;
using System.Runtime.InteropServices;
using Serilog.Sinks.SystemConsole.Themes;

var builder = WebApplication.CreateBuilder(args);



builder.Services.AddMarten(options =>
{
    var connString = builder.Configuration.GetConnectionString("marten");
    options.Connection(connString);

    // If we're running in development mode, let Marten just take care
    // of all necessary schema building and patching behind the scenes
    //if (Environment.IsDevelopment())
    //{
    //    options.AutoCreateSchemaObjects = AutoCreate.All;
    //}
});


builder.Services.AddHostedService<MyBgService>();

builder.Host.UseSerilog((ctx, lc) => lc
    .WriteTo.Console(theme: RuntimeInformation.IsOSPlatform(OSPlatform.Windows) ? SystemConsoleTheme.Literate : AnsiConsoleTheme.Code, applyThemeToRedirectedOutput: true)
    );

var app = builder.Build();
app.MapGet("/", () => "hello event sourcing");
app.Run();