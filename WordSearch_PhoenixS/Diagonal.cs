namespace WordSearch_PhoenixS
{
    class Diagonal : SearchType
    {
        /// <summary>
        /// Outputs the chosenWord into the wordsearch diagonally upwards or downwards.
        /// </summary>
        /// <param name="chosenWord"> Word being placed into the word search.</param>
        /// <param name="currentWordSearch"> The current word search being modified. </param>
        /// <param name="diagonalType"> Whether word will be placed upwards or downwards. </param>
        /// <param name="wasWordPlaced"> Checks if the word was placed at all.</param>
        /// <returns>Returns the new modified word search.</returns>
        public static char[,] PlaceWordInWordSearch(string chosenWord, char[,] currentWordSearch, int diagonalType, ref bool wasWordPlaced)
        {
            char[,] diagonalVersion = TransformWordSearch_ToDiagonal(currentWordSearch, diagonalType);
            diagonalVersion = SearchType_PlaceWordInWordSearch(chosenWord, diagonalVersion, ref wasWordPlaced);
            currentWordSearch = TransformWordSearch_RevertDiagonal(diagonalVersion, diagonalType);

            return currentWordSearch;
        }

        /// <summary>
        /// Checks the inputed user coordinates to see if they hold the first letter of the chosenWord diagonally.
        /// </summary>
        /// <param name="userY"> User input y-coordinate. </param>
        /// <param name="userX"> User input x-Coordinate. </param>
        /// <param name="currentWordSearch"> The current WordSearch being checked. Is modified to if the chosenWord is found. Replaces the chosenWord with '@'.</param>
        /// <param name="chosenWord"> The current word being checked for.</param>
        /// <returns>Returns true if the chosenWord was found and false if it wasn't.</returns>
        public static bool CheckUserCoordinates(int userY, int userX, ref char[,] currentWordSearch, string chosenWord)
        {
            //Create alternate versions of the wordSearch as its diagonals
            char[,] upSlopeDiagonalWordSearch = TransformWordSearch_ToDiagonal(currentWordSearch, 4);
            char[,] downSlopeDiagonalWordSearch = TransformWordSearch_ToDiagonal(currentWordSearch, 6);

            bool isValidCoordinates = SearchType_CheckUserCoordinates(userY, userX, ref upSlopeDiagonalWordSearch, chosenWord, true);
            if (!isValidCoordinates)        // If upSlope doesn't return true with the user coordinates
            {
                int specialX = currentWordSearch.GetLength(1) - userX - 1;    // Modify userX to account for downSlope diagonal
                isValidCoordinates = SearchType_CheckUserCoordinates(userY, specialX, ref downSlopeDiagonalWordSearch, chosenWord, true);
                currentWordSearch = TransformWordSearch_RevertDiagonal(downSlopeDiagonalWordSearch, 6);
            }
            else
            {
                currentWordSearch = TransformWordSearch_RevertDiagonal(upSlopeDiagonalWordSearch, 4);
            }
            return isValidCoordinates;
        }

        /// <summary>
        /// Takes apart the word search and outputs its diagonals.
        /// </summary>
        /// <param name="currentWordSearch">The current word search being taken apart.</param>
        /// <param name="diagonalType">If the diagonal type is an up slope(less than 6) or a down slope (6 or 7). 
        /// Based on the SearchTypeList[index] in WordSearch.NewWordSearch()</param>
        /// <returns>The list of diagonals in the current word search.</returns>
        static char[,] TransformWordSearch_ToDiagonal(char[,] currentWordSearch, int diagonalType)
        {
            if (diagonalType == 6 || diagonalType == 7)                                                         // For '\' downslope diagonals
            {
                currentWordSearch = TransformWordSearch_ReverseXaxis(currentWordSearch);
            }

            char[,] diagonalWordSearch = new char[40, 20];                                                      //[y,x] y is double the length of
                                                                                                                //  the y_axis of currentWordSearch
            for (int diagonalY_axis = 0; diagonalY_axis < diagonalWordSearch.GetLength(0); diagonalY_axis++)
            {
                for (int y_axis = 0; y_axis < currentWordSearch.GetLength(0); y_axis++)
                {
                    for (int x_axis = 0; x_axis < currentWordSearch.GetLength(1); x_axis++)
                    {
                        if (y_axis + x_axis == diagonalY_axis)                                                  // Diagonals are made when the y_axis + x_axis are all the same number
                        {                                                                                       // Only applies to '/' upslope diagonals
                            diagonalWordSearch[diagonalY_axis, x_axis] = currentWordSearch[y_axis, x_axis];
                        }
                    }
                }
            }
            return diagonalWordSearch;
        }

        /// <summary>
        /// Reverses the x axis of the word search such that the element at the minimum x coordinate becomes the element at the max coordiate and vice versa.
        /// </summary>
        /// <param name="currentWordSearch">The current word search being modified</param>
        /// <returns></returns>
        public static char[,] TransformWordSearch_ReverseXaxis(char[,] currentWordSearch)
        {
            char[,] newWordsearch = new char[currentWordSearch.GetLength(0), currentWordSearch.GetLength(1)];

            for (int y_axis = 0; y_axis < currentWordSearch.GetLength(0); y_axis++)
            {
                int reversalX_axis = currentWordSearch.GetUpperBound(1);
                for (int x_axis = 0; x_axis < currentWordSearch.GetLength(1); x_axis++)
                {
                    newWordsearch[y_axis, x_axis] = currentWordSearch[y_axis, reversalX_axis--];
                }
            }
            return newWordsearch;
        }
        /// <summary>
        /// Reverses the char[40,20] diagonal word search back into the char[20,20] word search.
        /// </summary>
        /// <param name="diagonalWordSearch">The word search listed as its diagonals.</param>
        /// <param name="diagonalType">If the diagonal type is an up slope(less than 6) or a down slope (6 or 7). 
        /// <returns>The word search as 20 by 20.</returns>
        static char[,] TransformWordSearch_RevertDiagonal(char[,] diagonalWordSearch, int diagonalType)
        {
            char[,] normalWordSearch = new char[20, 20];

            for (int y_axis = 0; y_axis < normalWordSearch.GetLength(0); y_axis++)
            {
                for (int x_axis = 0; x_axis < normalWordSearch.GetLength(1); x_axis++)
                {
                    for (int diagonalY_axis = 0; diagonalY_axis < diagonalWordSearch.GetLength(0); diagonalY_axis++)
                    {
                        if (y_axis == diagonalY_axis - x_axis)                                                   // Diagonals are made with y_axis + x_axis, so
                        {                                                                                        //      reversing it is taking the diagonalY_Axis - x_axis
                            normalWordSearch[y_axis, x_axis] = diagonalWordSearch[diagonalY_axis, x_axis];
                        }
                    }
                }
            }
            if (diagonalType == 6 || diagonalType == 7)                                              // Reverses the reverse 
            {
                normalWordSearch = TransformWordSearch_ReverseXaxis(normalWordSearch);
            }

            return normalWordSearch;
        }
    }
}
