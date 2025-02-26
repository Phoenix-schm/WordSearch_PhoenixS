namespace WordSearch_PhoenixS
{
    public class Vertical : SearchType
    {
        /// <summary>
        /// Outputs words into the word search vertically (on y axis)
        /// </summary>
        /// <param name="chosenWord"> Chosen word being placed into word search. </param>
        /// <param name="currentWordSearch"> The current word search being modified. </param>
        /// <param name="orderType"> Whether the word will be placed in order(0) or in reverse(1).</param>
        /// <param name="wasWordPlaced">Boolean for if the word was actually placed into the row.</param>
        /// <returns>Returns the new modified word search.</returns>
        public static char[,] PlaceWordInWordSearch(string chosenWord, char[,] currentWordSearch, ref bool wasWordPlaced)
        {
            currentWordSearch = ModifyWordSearch_FlipXYaxis(currentWordSearch);            // Flip currentWordSearch so that it can output vertically
            currentWordSearch = SearchType_PlaceWordInWordSearch(chosenWord, currentWordSearch, ref wasWordPlaced);
            currentWordSearch = ModifyWordSearch_FlipXYaxis(currentWordSearch);            // Flip the wordsearch back to its orginal postion
            return currentWordSearch;
        }
        /// <summary>
        /// Checks the inputed user coordinates to see if they hold the first letter of the chosenWord via the y axis
        /// </summary>
        /// <param name="userY"> User input y-coordinate. </param>
        /// <param name="userX"> User input x-Coordinate. </param>
        /// <param name="wordSearch"> The current WordSearch being checked. Is modified to if the chosenWord is found. Replaces the chosenWord with '@'.</param>
        /// <param name="chosenWord"> The current word being checked for.</param>
        /// <returns></returns>
        public static bool CheckUserCoordinates(int userY, int userX, ref char[,] currentWordSearch, string chosenWord)
        {
            currentWordSearch = ModifyWordSearch_FlipXYaxis(currentWordSearch);
            bool isValidCoordinates = SearchType_CheckUserCoordinates(userX, userY, ref currentWordSearch, chosenWord, false);        // Reverse x and y to accomodate rotation 
            currentWordSearch = ModifyWordSearch_FlipXYaxis(currentWordSearch);
            return isValidCoordinates;
        }
    }
}
