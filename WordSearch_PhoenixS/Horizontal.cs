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
        /// <returns></returns>
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType, ref bool wasWordPlaced)
        {
            currentWordSearch = PlaceChosenWordInWordSearch(chosenWord, currentWordSearch, orderType, ref wasWordPlaced);

            return currentWordSearch;
        }
    }
}
