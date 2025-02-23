namespace WordSearch_PhoenixS
{
    public class Horizontal : SearchType
    {
        /// <summary>
        /// Outputs a word into the word search horizontally ( in the x axis)
        /// </summary>
        /// <param name="chosenWord"> one of the random words </param>
        /// <param name="currentWordSearch"> the current word search, modified each time function is used </param>
        /// <param name="orderType"> the list of 8 random words </param>
        /// <param name="wasWordPlaced"> returs true or false whether word was actually placed into the Word Search</param>
        /// <returns></returns>
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType, ref bool wasWordPlaced)
        {
            currentWordSearch = PlaceChosenWordInWordSearch(chosenWord, currentWordSearch, orderType, ref wasWordPlaced);

            return currentWordSearch;
        }
        public static bool CheckUserCoordinates(int userY, int userX, char[,] wordSearch, string chosenWord)
        {
            bool isValidCoordinates = CheckCoordinates(userY, userX, wordSearch, chosenWord, false);
            return isValidCoordinates;
        }
    }
}
