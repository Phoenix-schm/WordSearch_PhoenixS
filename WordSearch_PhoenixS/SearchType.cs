namespace WordSearch_PhoenixS
{
    public class SearchType
    {
        /// <summary>
        /// Outputs a random number between minNumber(inclusive) and maxNumber(exclusive).
        /// </summary>
        /// <param name="minNumber">The minimum number (inclusive).</param>
        /// <param name="maxNumber">The maximum number (exclusive).</param>
        /// <returns>A random number betwen minNumber and maxNumber.</returns>
        public static int RandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            return random.Next(minNumber, maxNumber);
        }

        /// <summary>
        /// Places the word in the modifiedd word search based on SearchType.
        /// </summary>
        /// <param name="chosenWord">Word being placed into the word search.</param>
        /// <param name="currentWordSearch">The current modified word search.</param>
        /// <param name="wasSuccessfullyPlaced">If the word could be successfully placed.</param>
        /// <returns>Returns the modified word search, now with the chosenWord in it.</returns>
        public static char[,] SearchType_PlaceWordInWordSearch(string chosenWord, char[,] currentWordSearch, ref bool wasSuccessfullyPlaced)
        {
            int[] validIndex = ReturnValidIndex(currentWordSearch, chosenWord);
            int validY = validIndex[0];
            int validX = validIndex[1];

            if (validY == -1 || validX == -1)
            {
                // Do nothing and continue to returning the unmodified word search
            }
            else
            {
                wasSuccessfullyPlaced = true;
                int chosenWordIndex = 0;
                for (int xAxis = validX; chosenWordIndex < chosenWord.Length; xAxis++, chosenWordIndex++)
                {
                    currentWordSearch[validY, xAxis] = chosenWord[chosenWordIndex];
                }
            }
            return currentWordSearch;
        }

        /// <summary>
        /// Checks a random assortment of rows and spaces in that row to see if the chosenWord can fit inside.
        /// </summary>
        /// <param name="currentWordSearch">The current word search being checked. Modified based on SearchType.</param>
        /// <param name="chosenWord">The chosenWord as a char[].</param>
        /// <returns> returns the index of valid a valid y coordinate and x coordinate.</returns>
        static int[] ReturnValidIndex(char[,] currentWordSearch, string chosenWord)
        {
            int amountOfSpaces = currentWordSearch.GetLength(1);                                        // Helpful with redundancy.
            int amountOfRows = currentWordSearch.GetLength(0);                                          // Insure's this can be used for diagonal wordSearch placement
            int[] random_RowList = WordSearch.ReturnRandomNumberList(amountOfRows, amountOfRows);       // Creates a list of random rows, no duplicates

            for (int random_rowIndex = 0; random_rowIndex < random_RowList.Length; random_rowIndex++)   // Going through every row in a random assortment
            {
                int chosenRow = random_RowList[random_rowIndex];

                // The minimum and maximum range that the word can be placed in
                int minRange_NewWordPosition = -1;
                int maxRange_NewWordPosition = -1;

                int[] randomPositionList = WordSearch.ReturnRandomNumberList(amountOfSpaces, amountOfSpaces);               // Creates a random list of positions in the row
                for (int randomPositionIndex = 0; randomPositionIndex < randomPositionList.Length; randomPositionIndex++)   // Going through every space in a random assortment
                {
                    int chosenSpace = randomPositionList[randomPositionIndex];
                    CheckRowCanFitWord(chosenRow, chosenWord, currentWordSearch, chosenSpace, ref minRange_NewWordPosition, ref maxRange_NewWordPosition);

                    if (maxRange_NewWordPosition > -1 && minRange_NewWordPosition > -1)                 // If the row can fit the word at a random position
                    {
                        chosenSpace = RandomNumber(minRange_NewWordPosition, maxRange_NewWordPosition);
                        int[] validIndex = { chosenRow, chosenSpace };
                        return validIndex;
                    }
                }
            }
            int[] invalidIndex = { -1, -1 };                            // If there was nowhere for the word to fit in this SearchType
            return invalidIndex;
        }

        /// <summary>
        /// Checks the chosenRow for if it can hold the chosenWord
        /// Note: Uppercase letters are blank spaces that can be filled in and Lowercase letters are other words from the category
        /// </summary>
        /// <param name="chosenRow">Row being checked</param>
        /// <param name="chosenWord">Word that the method is trying to fit into chosenRow.</param>
        /// <param name="wordSearch">The current word search being checked</param>
        /// <param name="chosenSpace">A random position for the word to be placed at.</param>
        /// <param name="minRange">Referenced minimum range that the chosenWord could possibly fit into.</param>
        /// <param name="maxRange">Referenced maximum range the chosenWord could possibly fit into.</param>
        static void CheckRowCanFitWord(int chosenRow, string chosenWord, char[,] wordSearch, int chosenSpace, ref int minRange, ref int maxRange)
        {
            int canWordFitHere = 0;
            int additionalRange = 0;
            for (int space = chosenSpace; space < wordSearch.GetLength(1); space++)                     // Checking every space from that randomPosition
            {
                while (space != wordSearch.GetLength(1) && Char.IsUpper(wordSearch[chosenRow, space]))  // Exits once it runs into a lowercase letter or the end of the row
                {
                    if (canWordFitHere < chosenWord.Length)                                             // If there's still letters in the word
                    {
                        canWordFitHere++;
                    }
                    else if (canWordFitHere == chosenWord.Length)    // If there's still room in the row and the word can already fit
                    {
                        additionalRange++;
                    }
                    space++;
                }

                if (canWordFitHere == chosenWord.Length)             // If there is a Lowercase/end of row, but the word can fit beforehand
                {
                    additionalRange += canWordFitHere;
                    // Defaults to "in order" index
                    minRange = space - additionalRange;
                    maxRange = space - chosenWord.Length;
                    break;
                }
                else                                                // Else, there was an Lowercase before the word could fit
                {
                    canWordFitHere = 0;
                    additionalRange = 0;
                }
            }
        }


        /// <summary>
        /// Checks the [y,x] coordinates of the modified wordSearch. Based on SearchType.
        /// </summary>
        /// <param name="userY">User input y-coordinate.</param>
        /// <param name="userX">User input x-coordinate.</param>
        /// <param name="wordSearch">The current word search.</param>
        /// <param name="chosenWord">The chosenWord being placed into the word search.</param>
        /// <param name="isDiagonal">If the modified word search is diagonal.</param>
        /// <returns></returns>
        public static bool SearchType_CheckUserCoordinates(int userY, int userX, ref char[,] wordSearch, string chosenWord, bool isDiagonal)
        {
            int y_axis = userY;

            if (isDiagonal)
            {
                y_axis = userY + userX;
            }

            int chosenWordIndex = 0;
            int canWordFitHere = 0;
            for (int x_axis = userX; canWordFitHere < chosenWord.Length - 1; x_axis++, canWordFitHere++)    // Checks for word in order
            {
                if (x_axis > wordSearch.GetUpperBound(1))
                {
                    break;
                }
                else if (wordSearch[y_axis, x_axis] == chosenWord[chosenWordIndex])            // If each successive x-coordinate contains the successive letter in chosenWord 
                {
                    chosenWordIndex++;
                }
                if (chosenWordIndex == chosenWord.Length - 1)                                  // If the word was correctly found
                {
                    chosenWordIndex = 0;
                    for (int chosenSpace = x_axis + 1; chosenWordIndex < chosenWord.Length; chosenSpace--, chosenWordIndex++)   // Replace the chosenWord with '@'
                    {
                        wordSearch[y_axis, chosenSpace] = '@';
                    }
                    return true;
                }
            }
            // Resets variable checkers
            chosenWordIndex = 0;
            canWordFitHere = 0;
            for (int x_axis = userX; canWordFitHere < chosenWord.Length - 1; x_axis--, canWordFitHere++)    // Checks for word in reverse
            {
                if (x_axis < wordSearch.GetLowerBound(1))
                {
                    break;
                }
                else if (wordSearch[y_axis, x_axis] == chosenWord[chosenWordIndex])             // If each successive x-coordinate contains the successive letter in chosenWord 
                {
                    chosenWordIndex++;
                }
                if (chosenWordIndex == chosenWord.Length - 1)                                   // If the word was correctly found
                {
                    chosenWordIndex = 0;
                    for (int chosenSpace = x_axis - 1; chosenWordIndex < chosenWord.Length; chosenSpace++, chosenWordIndex++)   // Replace the chosenWord with '@'
                    {
                        wordSearch[y_axis, chosenSpace] = '@';
                    }
                    return true;
                }
            }
            return false;
        }
    }
}
