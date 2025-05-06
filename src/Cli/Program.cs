using Architect.Cli;
using Architect.Cli.Commands;
using Microsoft.Extensions.Hosting;
using ConsoleAppFramework;
using SimpleExec;

// will launch into the CLI using various flags
// there are a few options, looking at Terminal.Gui
// first, but then also looking at the one from nuecc
// and team.

// only some of them will run the host, and at that,
// we'll wrap it and make sure we're smart about how
// hosts are created
var app = ConsoleApp.Create();
app.Add<Application>(); // base application
app.Add<Install>("install"); // tools
app.Run(args);



// var host = Host.CreateApplicationBuilder(args).ConfigureHost();
// host.Run();