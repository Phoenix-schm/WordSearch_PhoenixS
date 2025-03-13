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
            int[]? validIndex = ReturnValidIndex(currentWordSearch, chosenWord);

            if (validIndex != null)
            {
                wasSuccessfullyPlaced = true;

                int validY = validIndex[0];
                int validX = validIndex[1];
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
        static int[]? ReturnValidIndex(char[,] currentWordSearch, string chosenWord)
        {
            int amountOfSpaces = currentWordSearch.GetLength(1);                                        // Helpful with redundancy.
            int amountOfRows = currentWordSearch.GetLength(0);                                          // Insure's this can be used for diagonal wordSearch placement
            int[] random_RowList = WordSearch.ReturnRandomNumberList(amountOfRows, amountOfRows);       // Creates a list of random rows, no duplicates

            int[]? validIndex = null;
            bool canWordFit = false;
            for (int random_rowIndex = 0; random_rowIndex < random_RowList.Length; random_rowIndex++)   // Going through every row in a random assortment
            {
                int chosenRow = random_RowList[random_rowIndex];

                int[] randomPositionList = WordSearch.ReturnRandomNumberList(amountOfSpaces, amountOfSpaces);               // Creates a random list of positions in the row
                for (int randomPositionIndex = 0; randomPositionIndex < randomPositionList.Length; randomPositionIndex++)   // Going through every space in a random assortment
                {
                    int chosenSpace = randomPositionList[randomPositionIndex];
                    canWordFit = CheckRowCanFitWord(chosenRow, chosenSpace, chosenWord, currentWordSearch);

                    if (canWordFit)
                    {
                        validIndex = [chosenRow, chosenSpace];
                        break;
                    }
                }
                if (canWordFit)
                {
                    break;
                }
            }
            return validIndex;
        }

        /// <summary>
        /// Checks the chosenRow for if it can hold the chosenWord
        /// Note: Uppercase letters are blank spaces that can be filled in and Lowercase letters are other words from the category
        /// </summary>
        /// <param name="chosenRow">Row being checked</param>
        /// <param name="chosenSpace">A random position for the word to be placed at.</param>
        /// <param name="chosenWord">Word that the method is trying to fit into chosenRow.</param>
        /// <param name="wordSearch">The current word search being checked</param>
        static bool CheckRowCanFitWord(int chosenRow, int chosenSpace, string chosenWord, char[,] wordSearch)
        {
            int canWordFitHere = 0;
            bool isValidIndex = false;

            while (chosenSpace != wordSearch.GetLength(1) && Char.IsUpper(wordSearch[chosenRow, chosenSpace]))  // Exits once it runs into a lowercase letter or the end of the row
            {
                if (canWordFitHere < chosenWord.Length)                                             // If there's still letters in the word
                {
                    canWordFitHere++;
                }

                if (canWordFitHere == chosenWord.Length)    // If there's still room in the row and the word can already fit
                {
                    isValidIndex = true;
                    break;
                }
                chosenSpace++;
            }
            
            return isValidIndex;
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

            bool isValidCoordinates = CheckCoordinates_InOrder(y_axis, userX, ref wordSearch, chosenWord);
            if (!isValidCoordinates)
            {
                wordSearch = Diagonal.TransformWordSearch_ReverseXaxis(wordSearch);     // for diagonal add one to y, minus 1 to x
                userX = wordSearch.GetLength(1) - userX - 1;                                    // inverse x to accomodate

                isValidCoordinates = CheckCoordinates_InOrder(y_axis, userX, ref wordSearch, chosenWord);
                wordSearch = Diagonal.TransformWordSearch_ReverseXaxis(wordSearch);
            }
            return isValidCoordinates;
        }

        static bool CheckCoordinates_InOrder(int y_axis, int userX, ref char[,] wordSearch, string chosenWord)
        {
            int chosenWordIndex = 0;
            int canWordFitHere = 0;
            bool isValid = false;
            for (int x_axis = userX; chosenWordIndex < chosenWord.Length; x_axis++, chosenWordIndex++)    // Checks for word in order
            {
                if (x_axis > wordSearch.GetUpperBound(1))
                {
                    break;
                }
                else if (wordSearch[y_axis, x_axis] == chosenWord[chosenWordIndex])            // If each successive x-coordinate contains the successive letter in chosenWord 
                {
                    canWordFitHere++;
                }


                if (canWordFitHere == chosenWord.Length - 1)                                  // If the word was correctly found
                {
                    isValid = true;
                    chosenWordIndex = 0;
                    for (int chosenSpace = userX; chosenWordIndex < chosenWord.Length; chosenSpace++, chosenWordIndex++)   // Replace the chosenWord with '@'
                    {
                        wordSearch[y_axis, chosenSpace] = '@';
                    }
                }
            }
            return isValid;
        }
    }
}
