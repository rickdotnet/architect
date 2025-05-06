using Architect.Cli;
using Architect.Cli.Commands;
using Microsoft.Extensions.Hosting;
using ConsoleAppFramework;
using Library;
using Library.Conf;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Serilog;
using SimpleExec;

using var host = Host.CreateDefaultBuilder().Build(); // use using for host lifetime
using var scope = host.Services.CreateScope(); // create execution scope
ConsoleApp.ServiceProvider = scope.ServiceProvider;

var app = ConsoleApp.Create();
// app.Add("", ([FromServices] AppConfig config) =>
// {
//     Console.WriteLine($"alias - {config?.BashAlias}");
//     Console.WriteLine("Do you even yolo, bro?");
// });

app.Add<Application>(); // base application
app.Add<Install>("install"); // tools

app
    .ConfigureLogging(Startup.Logging)
    .ConfigureDefaultConfiguration(Startup.Configuration)
    .ConfigureServices(Startup.Services)
    .Run(args);

