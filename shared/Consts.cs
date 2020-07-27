namespace BoggleShared
{
    /// <summary>
    /// Allows for the centralization of all const char strings in the project.  
    /// This is a preference thing, but I like having all my const char strings in a 
    /// central place rather than scattered around at the top of each file.
    /// </summary>
    public static class Consts
    {
        public const string c_argExceptionDescBoard = "Board array length must equal sideLength squared";
        public const string c_argExceptionDescSideLength = "SideLength must be between 1 and 1024";

        public const string c_logBoardStart = "BOARD START";
        public const string c_logBoardEnd = "BOARD END";

        public const char c_firstLetterOfAlphabet = 'a';

        public const int c_asciiCharCodeOfA = 97;
        public const int c_asciiCharCodeOfZ = 122;

        public const int c_alphabetSize = 26;
    }
}
