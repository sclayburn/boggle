using Boggle.Options;
using Boggle.Utils;
using System;

namespace Boggle.Validation
{
    /// <summary>
    /// Ensures that the commandline arguments meet our range specifications.
    /// </summary>
    public class ValidateCmdLine
    {
        /// <summary>
        /// Ensure that the command line options are valid and within bounds.
        /// </summary>
        /// <param name="opts">CmdLineOptions object that contains the parsed command line args.</param>
        public void IsCmdLineValid(CmdLineOptions opts)
        {
            if (opts == null)
            {
                throw new ArgumentNullException(nameof(opts));
            }

            IsSideLengthValid(opts.SideLength);
        }

        private void IsSideLengthValid(int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(nameof(length), Consts.c_argExceptionDescSideLengthZero);
            }

            if (length > 1024)
            {
                throw new ArgumentOutOfRangeException(nameof(length), Consts.c_argExceptionDescSideLengthLessThanMax);
            }
        }
    }
}
