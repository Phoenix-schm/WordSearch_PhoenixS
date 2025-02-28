namespace WordSearch_PhoenixS
{
    public class Vertical : SearchType
    {
        /// <summary>
        /// Outputs words into the word search vertically (on y axis)
        /// </summary>
        /// <param name="chosenWord"> Chosen word being placed into word search. </param>
        /// <param name="currentWordSearch"> The current word search being modified. </param>
        /// <param name="wasWordPlaced">Boolean for if the word was actually placed into the row.</param>
        /// <returns>Returns the new modified word search.</returns>
        public static char[,] PlaceWordInWordSearch(string chosenWord, char[,] currentWordSearch, ref bool wasWordPlaced)
        {
            currentWordSearch = TransformWordSearch_FlipXYaxis(currentWordSearch);            // Flip currentWordSearch so that it can output vertically
            currentWordSearch = SearchType_PlaceWordInWordSearch(chosenWord, currentWordSearch, ref wasWordPlaced);
            currentWordSearch = TransformWordSearch_FlipXYaxis(currentWordSearch);            // Flip the wordsearch back to its orginal postion

            return currentWordSearch;
        }
        /// <summary>
        /// Checks the inputed user coordinates to see if they hold the first letter of the chosenWord via the y axis
        /// </summary>
        /// <param name="userY"> User input y-coordinate. </param>
        /// <param name="userX"> User input x-Coordinate. </param>
        /// <param name="currentWordSearch"> The current WordSearch being checked. Is modified if the chosenWord is found. Replaces the chosenWord with '@'.</param>
        /// <param name="chosenWord"> The current word being checked for.</param>
        /// <returns></returns>
        public static bool CheckUserCoordinates(int userY, int userX, ref char[,] currentWordSearch, string chosenWord)
        {
            currentWordSearch = TransformWordSearch_FlipXYaxis(currentWordSearch);
            bool isValidCoordinates = SearchType_CheckUserCoordinates(userX, userY, ref currentWordSearch, chosenWord, false);        // Reverse x and y to accomodate rotation 
            currentWordSearch = TransformWordSearch_FlipXYaxis(currentWordSearch);

            return isValidCoordinates;
        }

        /// <summary>
        /// Flips the x and the y coordinates of the word search.
        /// </summary>
        /// <param name="currentWordSearch">The current word search beinng modified.</param>
        /// <returns>currentWordSearch but flipped.</returns>
        static char[,] TransformWordSearch_FlipXYaxis(char[,] currentWordSearch)
        {
            char[,] newWordSearch = new char[20, 20];

            for (int x_axis = 0; x_axis < currentWordSearch.GetLength(1); x_axis++)
            {
                for (int y_axis = 0; y_axis < currentWordSearch.GetLength(0); y_axis++)
                {
                    newWordSearch[x_axis, y_axis] = currentWordSearch[y_axis, x_axis];
                }
            }
            return newWordSearch;
        }
    }
}
