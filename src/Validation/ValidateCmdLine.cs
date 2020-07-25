using boggleApp.Options;
using boggleApp.Utils;
using System;
using System.Collections.Generic;
using System.Text;

namespace boggleApp.Validation
{
    public class ValidateCmdLine
    {
        /// <summary>
        /// Ensure that the command line options are valid and within bounds
        /// </summary>
        /// <param name="opts">CmdLineOptions object that contains the parsed command line args</param>
        public void IsCmdLineValid(CmdLineOptions opts)
        {
            if (opts == null)
            {
                throw new ArgumentNullException(Consts.c_paramNameOpts);
            }

            IsSideLengthValid(opts.SideLength);
        }

        private void IsSideLengthValid(int length)
        {
            if (length < 1)
            {
                throw new ArgumentOutOfRangeException(Consts.c_paramNameSideLength, Consts.c_argExceptionDescSideLengthZero);
            }

            if (length > 1024)
            {
                throw new ArgumentOutOfRangeException(Consts.c_paramNameSideLength, Consts.c_argExceptionDescSideLengthLessThanMax);
            }
        }
    }
}
