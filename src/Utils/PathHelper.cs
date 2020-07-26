using System;
using System.IO;

namespace Boggle.Utils
{
    public static class PathHelper
    {
        /// <summary>
        /// Search for and find the first path that contains the dictionary file.
        /// </summary>
        /// <returns>Fully qualified local path to the dictionary file.</returns>
        public static string GetWordDictionaryPath()
        {
            string[] files = Directory.GetFiles(Consts.c_localDir, Consts.c_dictFilename, SearchOption.AllDirectories);
            if (files.Length == 0)
            {
                throw new Exception(string.Format(Consts.c_exceptionDictNotFound, Consts.c_dictFilename));
            }
            return files[0];
        }
    }
}
