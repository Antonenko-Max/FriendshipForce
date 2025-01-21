using CommandLine;

namespace Sd.Crm.Backend.Misc.CommandLine
{
    public class CommandLineOptions
    {
        [Option("update-database", Default = false, Required = false, HelpText = "Updates the database schema based on the last migration snapshot.")]
        public bool UpdateDatabase { get; set; }
    }
}
