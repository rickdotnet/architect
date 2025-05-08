namespace Library.Shell.Wrappers;

public class Paru
{
    public static Task<ShellCommandResult> UpdateAll()
        => Shell.RunAsync("paru", "-Syu --noconfirm");

}
