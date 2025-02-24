namespace WordSearch_PhoenixS
{
    public class Horizontal : SearchType // horizontal basically runs the default version of their called functions. Keeping it in to help keep away confusion.
    {
        /// <summary>
        /// Outputs words into the word search horizontally (on the x-axis).
        /// </summary>
        /// <param name="chosenWord"> Chosen word being placed into word search. </param>
        /// <param name="currentWordSearch"> The current word search being modified. </param>
        /// <param name="orderType"> Whether the word will be placed in order(0) or in reverse(1).</param>
        /// <returns>Returns the new modified word search.</returns>
        public static char[,] PlaceWordInWordSearch(string chosenWord, char[,] currentWordSearch, int orderType, ref bool wasWordPlaced)
        {
            currentWordSearch = SearchType_PlaceWordInWordSearch(chosenWord, currentWordSearch, orderType, ref wasWordPlaced);
            return currentWordSearch;
        }
        /// <summary>
        /// Checks the inputed user coordinates to see if they hold the first letter of the chosenWord.
        /// </summary>
        /// <param name="userY"> User input y-coordinate. </param>
        /// <param name="userX"> User input x-Coordinate. </param>
        /// <param name="wordSearch"> The current WordSearch being checked. Is modified to if the chosenWord is found. Replaces the chosenWord with '@'.</param>
        /// <param name="chosenWord"> The current word being checked for.</param>
        /// <returns>Returns true if the chosenWord was found and false if it wasn't.</returns>
        public static bool CheckUserCoordinates(int userY, int userX, ref char[,] wordSearch, string chosenWord)
        {
            bool isValidCoordinates = SearchType_CheckUserCoordinates(userY, userX, ref wordSearch, chosenWord, false);
            return isValidCoordinates;
        }
    }
}
