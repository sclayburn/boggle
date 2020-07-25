using System;
using System.Collections.Generic;
using System.Text;
using CommandLine;

namespace boggleApp.Options
{
    [Verb("game", HelpText = "Generate a new round of the Boggle game")]
    public class CmdLineOptions
    {
        [Option('l', "sidelength", Required = false, HelpText = "Board must be square, this is the length of a single side.  Default is 8, which will result in an 8x8 board.", Default = 8)]
        public int SideLength { get; set; }

        [Option('s', "singlethreaded", Required = true, HelpText = "Enable or disable singlethreaded execution.  Default = true", Group = "Threading")]
        public bool Singlethreaded { get; set; }

        [Option('a', "async", Required = true, HelpText = "Enable or disable async processing.  Default = false", Group = "Threading")]
        public bool Async { get; set; }
    }
}
