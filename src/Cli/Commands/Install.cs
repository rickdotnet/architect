using ConsoleAppFramework;

namespace Architect.Cli.Commands;

public class Install
{
    /// <summary>
    /// Install a package. Checks pacman first, then paru.
    /// </summary>
    /// <param name="package">Name of the package to install</param>
    [Command("")]
    public void Run(string package)
    {
        Console.WriteLine("Installing package: " + package);
    }
}
