using System;
using System.Collections.Generic;
using System.Text;

namespace boggleApp.Utils
{
    public static class Consts
    {
        public const string c_paramNameOpts = "opts";
        public const string c_paramNameSideLength = "width";

        public const string c_argExceptionDescSideLengthZero = "Width must be greater than zero";
        public const string c_argExceptionDescSideLengthLessThanMax = "Width must be less than   ";

        public const string c_exceptionDictNotFound = "Dictionary File not found anywhere in solution path [ {0} ]";

        public const string c_logTimingInfo = "Total Words [ {0} ] -- Prefixes Checked [ {1} ] -- Avg Time per Check [ {2} ns ]";
        public const string c_logWordDictSinglethreadedLoad = "Loading Word Dictionary :: [ SINGLETHREADED ]";
        public const string c_logWordDictAsyncLoad = "Loading Word Dictionary :: [ ASYNC ]";
        public const string c_logWordDictLoadComplete = "Loading Word Dictionary :: [ COMPLETE ]";
        public const string c_logWordDictLoaded = "Loaded [ {0} ] words";
        public const string c_logAppRuntime = "App Runtime :: [ {0} ms ]";
        public const string c_logAsyncCreatingTasks = "Creating Tasks...";
        public const string c_logAsyncTasksCreated = "All Tasks Created [ {0} ], Starting Execution...";
        public const string c_logAsyncWaitingToComplete = "Waiting for tasks to complete...";

        public const int c_ExitCodeSuccess = 0;
        public const int c_ExitCodeFailure = 1;

        public const int c_CopyrightYear = 2020;
        public const string c_CopyrightFormat = "copylinkedlist - Copyright © {0}";

        public const string c_dictFilename = "twl06.txt";
        public const string c_localDir = "./";
    }
}
