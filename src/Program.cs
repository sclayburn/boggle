using Boggle.Managers;
using Boggle.Options;
using BoggleShared;
using CommandLine;
using Serilog;
using System;
using System.Threading.Tasks;

namespace Boggle
{
    class Program
    {
        /// <summary>
        /// Async entrypoint for the application.  
        /// </summary>
        /// <param name="args">Commandline arguments</param>
        /// <returns>0 = Success; 1 = Error.</returns>
        static async Task Main(string[] args)
        {
            Log.Logger = new LoggerConfiguration()
                .WriteTo.Console()
                .CreateLogger();

            WriteCopyright();

            int exitCode = await Parser.Default.ParseArguments<CmdLineOptions, object>(args)
            .MapResult(
                (CmdLineOptions opts) => GameManager.RunAndReturnExitCodeAsync(opts),
                errs => Task.FromResult(Utils.Consts.c_exitCodeFailure));

            Log.Information(Utils.Consts.c_logAppRuntime, RuntimeTimer.CalcAppRuntimeMs());

            Log.CloseAndFlush();

            Environment.Exit(exitCode);
        }

        private static void WriteCopyright()
        {
            string copyrightYear = Utils.Consts.c_copyrightYear.ToString();
            if (DateTime.Now.Year > Utils.Consts.c_copyrightYear)
            {
                copyrightYear += $"-{DateTime.Now.Year}";
            }
            Log.Information(Utils.Consts.c_copyrightFormat, copyrightYear);

        }
    }
}
