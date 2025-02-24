namespace WordSearch_PhoenixS
{
    class Diagonal : SearchType
    {
        /// <summary>
        /// Outputs the chosenWord into the wordsearch diagonally upwards or downwards.
        /// </summary>
        /// <param name="chosenWord"> Word being placed into the word search.</param>
        /// <param name="currentWordSearch"> The current word search being modified. </param>
        /// <param name="orderType"> Whether word will be placed in order or in reverse.</param>
        /// <param name="diagonalType"> Whether word will be placed upwards or downwards. </param>
        /// <param name="wasWordPlaced"> Checks if the word was placed at all.</param>
        /// <returns>Returns the new modified word search.</returns>
        public static char[,] PlaceWordInWordSearch(string chosenWord, char[,] currentWordSearch, int orderType, int diagonalType, ref bool wasWordPlaced)
        {
            char[,] diagonalVersion = TransformToDiagonalWordSearch(currentWordSearch, diagonalType);
            diagonalVersion = SearchType_PlaceWordInWordSearch(chosenWord, diagonalVersion, orderType, ref wasWordPlaced);

            currentWordSearch = RevertDiagonalWordSearchToNormal(diagonalVersion, diagonalType);
            return currentWordSearch;
        }
        /// <summary>
        /// Checks the inputed user coordinates to see if they hold the first letter of the chosenWord diagonally.
        /// </summary>
        /// <param name="userY"> User input y-coordinate. </param>
        /// <param name="userX"> User input x-Coordinate. </param>
        /// <param name="wordSearch"> The current WordSearch being checked. Is modified to if the chosenWord is found. Replaces the chosenWord with '@'.</param>
        /// <param name="chosenWord"> The current word being checked for.</param>
        /// <returns>Returns true if the chosenWord was found and false if it wasn't.</returns>
        public static bool CheckUserCoordinates(int userY, int userX, ref char[,] wordSearch, string chosenWord)
        {
            //Create alternate versions of the wordSearch as its diagonals
            char[,] upSlopeDiagonalWordSearch = TransformToDiagonalWordSearch(wordSearch, 4);
            char[,] downSlopeDiagonalWordSearch = TransformToDiagonalWordSearch(wordSearch, 6);

            bool isValidCoordinates = SearchType_CheckUserCoordinates(userY, userX, ref upSlopeDiagonalWordSearch, chosenWord, true);
            if (!isValidCoordinates)        // If upSlope doesn't return true with the user coordinates
            {
                userX = wordSearch.GetLength(1) - userX - 1;    // Modify userX to account for downSlope diagonal
                isValidCoordinates = SearchType_CheckUserCoordinates(userY, userX, ref downSlopeDiagonalWordSearch, chosenWord, true);
                wordSearch = RevertDiagonalWordSearchToNormal(downSlopeDiagonalWordSearch, 6);
            }
            else
            {
                wordSearch = RevertDiagonalWordSearchToNormal(upSlopeDiagonalWordSearch, 4);
            }

            return isValidCoordinates;    
        }
    }
}
