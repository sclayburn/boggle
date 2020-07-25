using System;
using System.Threading.Tasks;
using CommandLine;
using Serilog;
using boggleApp.Utils;
using boggleApp.Options;
using boggleApp.Managers;
using boggleShared;

namespace boggleApp
{
    class Program
    {
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            WriteCopyright();

            int exitCode = await Parser.Default.ParseArguments<CmdLineOptions, object>(args)
            .MapResult(
                (CmdLineOptions opts) => GameManager.RunAndReturnExitCode(opts),
                errs => Task.FromResult(Utils.Consts.c_ExitCodeFailure));

            Log.Information(Utils.Consts.c_logAppRuntime, new object[] { RuntimeTimer.CalcAppRuntimeMs() });

            Log.CloseAndFlush();

            Environment.Exit(exitCode);
        }

        private static void WriteCopyright()
        {
            string copyrightYear = Utils.Consts.c_CopyrightYear.ToString();
            if (DateTime.Now.Year > Utils.Consts.c_CopyrightYear)
            {
                copyrightYear += $"-{DateTime.Now.Year}";
            }
            Log.Information(Utils.Consts.c_CopyrightFormat, new object[] {copyrightYear});

        }
    }
}
