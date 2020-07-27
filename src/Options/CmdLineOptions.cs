using CommandLine;

namespace Boggle.Options
{
    /// <summary>
    /// Defines the valid commandline options that the CommandLineParser assembly will accept.
    /// </summary>
    [Verb("game", HelpText = "Generate a new round of the Boggle game")]
    public class CmdLineOptions
    {
        [Option('s', "singlethreaded", Required = true, HelpText = "Enable or disable singlethreaded execution.  Default = true", Group = "Threading")]
        public bool Singlethreaded { get; set; }

        [Option('a', "async", Required = true, HelpText = "Enable or disable async processing.  Default = false", Group = "Threading")]
        public bool Async { get; set; }

        [Option('l', "sidelength", Required = false, HelpText = "Board must be square, this is the length of a single side.  Default is 8, which will result in an 8x8 board.", Default = 8)]
        public int SideLength { get; set; }

        [Option('f', "forcewritefoundwords", Required = false, HelpText = "If more than 64 words are found, individual words are not written.  This forces all words written at the end of a run.  Default = false", Default = false)]
        public bool ForceWriteFoundWords { get; set; }
    }
}
