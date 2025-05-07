using Architect.Cli;
using Architect.Cli.Commands;
using Microsoft.Extensions.Hosting;
using ConsoleAppFramework;
using Library.Shell;
using Microsoft.Extensions.DependencyInjection;
using Serilog;
using Serilog.Events;

var result = ShellCommand.Run("echo", "\"Hello World\"");
var sudoResult = ShellCommand.Run("sudo", "-v"); // cache the password

var command = ShellCommand.Build("echo", "\"Hello World\"")
    .WithOutputSink(Console.WriteLine)
    .WithSudo() // password will be cached
    .Create();

var result2 = await command.ExecuteAsync();

return;

using var host = Host.CreateDefaultBuilder().Build();
using var scope = host.Services.CreateScope();
ConsoleApp.ServiceProvider = scope.ServiceProvider;

var app = ConsoleApp.Create();
app.Add<Application>(); // base application
app.Add<Install>("install"); // tools

app
    .ConfigureLogging(Startup.Logging)
    .ConfigureDefaultConfiguration(Startup.Configuration)
    .ConfigureServices(Startup.Services)
    .Run(args);
