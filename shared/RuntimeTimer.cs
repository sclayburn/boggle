using System;

namespace BoggleShared
{
    public static class RuntimeTimer
    {
        private static DateTime m_appStart;
        private static DateTime m_appEnd;

        /// <summary>
        /// Records the DateTime this application started on.
        /// </summary>
        public static void RegisterAppStart()
        {
            m_appStart = DateTime.UtcNow;
        }

        /// <summary>
        /// Records the DateTime this application finished processing.
        /// </summary>
        public static void RegisterAppEnd()
        {
            m_appEnd = DateTime.UtcNow;
        }

        /// <summary>
        /// Calculates the total number of microseconds that the application took to execute.
        /// </summary>
        /// <returns>Total number of microseconds.</returns>
        public static double CalcAppRuntimeUs()
        {
            TimeSpan span = new TimeSpan(m_appEnd.Ticks - m_appStart.Ticks);
            return Math.Round(span.TotalMilliseconds * 1000, 3);
        }

        /// <summary>
        /// Calculates the total number of milliseconds that the application took to execute.
        /// </summary>
        /// <returns>Total number of milliseconds.</returns>
        public static double CalcAppRuntimeMs()
        {
            TimeSpan span = new TimeSpan(m_appEnd.Ticks - m_appStart.Ticks);
            return Math.Round(span.TotalMilliseconds, 3);
        }
    }
}
