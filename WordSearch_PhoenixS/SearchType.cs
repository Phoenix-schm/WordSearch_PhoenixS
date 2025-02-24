using System.Reflection.Metadata.Ecma335;

namespace WordSearch_PhoenixS
{
    public class SearchType
    {
        /// <summary>
        /// Outputs a random number between minNumber(inclusive) and maxNumber(exclusive).
        /// Used with Vertical search type
        /// </summary>
        /// <param name="minNumber">The minimum number (inclusive).</param>
        /// <param name="maxNumber">The maximum number (exclusive).</param>
        /// <returns>A random number betwen minNumber and maxNumber.</returns>
        public static int RandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            int randomNumber = random.Next(minNumber, maxNumber);
            return randomNumber;
        }

        /// <summary>
        /// Flips the x and the y coordinates of the word search.
        /// </summary>
        /// <param name="currentWordSearch">The current word search beinng modified.</param>
        /// <returns>currentWordSearch but flipped.</returns>
        public static char[,] FlipWordSearch(char[,] currentWordSearch)
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

        /// <summary>
        /// Reverses the x axis of the word search such that the element at the minimum x coordinate becomes the element at the max coordiate and vice versa.
        /// Used with TransformToDiagonalWordSearch() for the down slope diagonals
        /// </summary>
        /// <param name="currentWordSearch">The current word search being modified</param>
        /// <returns></returns>
        public static char[,] ReverseWordSearch(char[,] currentWordSearch)
        {
            char[,] newWordsearch = new char[20, 20];

            for (int y_axis = 0; y_axis < currentWordSearch.GetLength(0); y_axis++ )
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
        /// Takes apart the word search into its diagonals.
        /// </summary>
        /// <param name="currentWordSearch">The current word search being taken apart.</param>
        /// <param name="diagonalType">If the diagonal type is an up slope(less than 6) or a down slope (6 or 7). 
        /// Based on the SearchTypeList[index] in WordSearch.NewWordSearch()</param>
        /// <returns>The list of diagonals in the current word search.</returns>
        public static char[,] TransformToDiagonalWordSearch(char[,] currentWordSearch, int diagonalType)
        {
            if (diagonalType == 6 || diagonalType == 7)
            {
                currentWordSearch = ReverseWordSearch(currentWordSearch);
            }

            char[,] diagonalWordSearch = new char[40, 20];                                                      //[y,x] y is double the length of
                                                                                                                //the y_axis of currentWordSearch
            for (int diagonalY_axis = 0; diagonalY_axis < diagonalWordSearch.GetLength(0); diagonalY_axis++)           
            {
                for (int y_axis = 0; y_axis < currentWordSearch.GetLength(0); y_axis++)
                {
                    for (int x_axis = 0; x_axis < currentWordSearch.GetLength(1); x_axis++)
                    {
                        if (y_axis + x_axis == diagonalY_axis)                                                  // Diagonals are made when the y_axis + x_axis are all the same number
                        {
                            diagonalWordSearch[diagonalY_axis, x_axis] = currentWordSearch[y_axis, x_axis];
                        }
                    }
                }
            }
            return diagonalWordSearch;
        }

        /// <summary>
        /// Reverses the char[40,20] diagonal word search back into the char[20,20] word search.
        /// </summary>
        /// <param name="diagonalWordSearch">The word search listed as its diagonals.</param>
        /// <param name="diagonalType">If the diagonal type is an up slope(less than 6) or a down slope (6 or 7). 
        /// <returns>The word search as 20 by 20.</returns>
        public static char[,] RevertDiagonalWordSearchToNormal(char[,] diagonalWordSearch, int diagonalType)
        {
            char[,] normalWordSearch = new char[20,20];

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
                normalWordSearch = ReverseWordSearch(normalWordSearch);
            }

            return normalWordSearch;
        }

        /// <summary>
        /// Checks a random assortment of rows and spaces in that row to see if the chosenWord can fit inside the row.
        /// </summary>
        /// <param name="wordSearch">The current word search being checked. Modified based on SearchType.</param>
        /// <param name="chosenWord">The chosenWord as a char[].</param>
        /// <param name="orderType"> Whether the word is being placed in order(0) or in reverse(1).</param>
        /// <returns> returns the index of valid a valid y coordinate and x coordinate.</returns>
        static int[] ReturnValidIndex(char[,] wordSearch, char[] chosenWord, int orderType)
        {
            int amountOfSpaces = wordSearch.GetLength(1);                                               // Helpful with redundancy.
            int amountOfRows = wordSearch.GetLength(0);                                                 // Insure's this can be used for diagonal wordSearch placement
            int[] random_RowList = WordSearch.ReturnRandomNumberList(amountOfRows, amountOfRows);       // Creates a list of random rows, no duplicates

            for (int random_rowIndex = 0; random_rowIndex < random_RowList.Length; random_rowIndex++)               // Going through every row in a random assortment
            {
                int chosenRow = random_RowList[random_rowIndex];

                int[] randomPositionList = WordSearch.ReturnRandomNumberList(amountOfSpaces, amountOfSpaces);       // Creates a random list of positions in the row
                
                // The minimum and maximum range that the word can be placed in
                int minRange_NewWordPosition = 0;
                int maxRange_NewWordPosition = 0;

                bool validRow = false;
                maxRange_NewWordPosition = CheckRowIsEntirelyBlank(chosenRow, wordSearch, ref validRow, maxRange_NewWordPosition, chosenWord);

                for (int randomPosition = randomPositionList[0];  randomPosition < randomPositionList.Length; randomPosition++)     // Picks a random place in the row to start at
                {
                    if (validRow == true)                                                                       // If the CheckRowIsEntirelyBlank() returned true
                    {
                        break;
                    }
                    int additionalRange = 0;
                    int canFitHere = 0;

                    for (int space = randomPosition; space < amountOfSpaces; space++)                        // Checking every space from that randomPosition
                    {
                        if (wordSearch[chosenRow, space] == ' ' && canFitHere < chosenWord.Length)             // If there's blank spaces
                        {
                            canFitHere++;
                        }
                        else if (wordSearch[chosenRow, space] == ' ' && space < amountOfSpaces && canFitHere >= chosenWord.Length) // If there's still room in the row and the word can already fit
                        {
                            additionalRange++;
                        }
                        else if (wordSearch[chosenRow, space] != ' ' && canFitHere >= chosenWord.Length)       // If there is a letter, but the word can fit beforehand
                        {
                            additionalRange += canFitHere;
                            // defaults to in order index
                            minRange_NewWordPosition = space - additionalRange;
                            maxRange_NewWordPosition = space - chosenWord.Length;
                            validRow = true;
                            break;
                        }
                        else if (canFitHere >= chosenWord.Length)                                          // There's no more space, but the word can fit
                        {                                                                                  // (also procks for the space after a word was already found int the row)
                            additionalRange += canFitHere;
                            // defaults to in order index
                            minRange_NewWordPosition = space - additionalRange;
                            maxRange_NewWordPosition = space - chosenWord.Length;
                            validRow = true;
                            break;
                        }
                        else                                                                              // if there's no room for the new word
                        {
                            canFitHere = 0;
                            additionalRange = 0;
                            validRow = false;
                            break;
                        }
                    }
                    if (validRow == true)
                    {
                        break;
                    }
                }

                if (validRow == true)
                {
                    int chosenSpace = 0;
                    switch (orderType)
                    {
                        case 0:                 // in order
                            chosenSpace = RandomNumber(minRange_NewWordPosition, maxRange_NewWordPosition);
                            break;
                        case 1:                 // in reverse
                            minRange_NewWordPosition += chosenWord.Length - 1;
                            maxRange_NewWordPosition += chosenWord.Length;
                            chosenSpace = RandomNumber(minRange_NewWordPosition, maxRange_NewWordPosition);
                            break;
                    }
                    int[] validIndex = { chosenRow, chosenSpace };
                    return validIndex;
                }
            }
            int[] invalidIndex = { -1, -1 };                            // If there was nowhere for the word to fit in this SearchType
            return invalidIndex;
        }
        
        /// <summary>
        /// Checks a chosenRow to see if its entirely blank
        /// </summary>
        /// <param name="_chosenRow">Row being checked.</param>
        /// <param name="_wordSearch">The current word search being used.</param>
        /// <param name="rowValidity">Can the row ontain the word. Is true if the row is completely blank.</param>
        /// <param name="maxRange">The max possible index the word can start to be placed at.</param>
        /// <param name="_chosenWord">The word being placed in the row.</param>
        /// <returns>Returns the modified maxRange to be either 0 (if there's already a word in the row) or the length of the row minus the chosenWord length</returns>
        static int CheckRowIsEntirelyBlank(int _chosenRow, char[,] _wordSearch, ref bool rowValidity, int maxRange, char[] _chosenWord)
        {
            int isFilledWithBlanks = 0;
            int rowLength = _wordSearch.GetLength(1) - 1;

            for (int space = 0; space <= rowLength; space++)                                // Checks if row only contains blank spaces
            {
                if (_wordSearch[_chosenRow, space] == ' ')
                {
                    isFilledWithBlanks++;
                }
                if (isFilledWithBlanks > rowLength)                                         // If the whole row contains blanks
                {
                    rowValidity = true;
                    maxRange = rowLength - _chosenWord.Length;
                    break;
                }                                                                           // else, maxRange doesn't change
            }
            return maxRange;
        }

       /// <summary>
       /// Places the word in the modifiedd word search based on SearchType.
       /// </summary>
       /// <param name="chosenWord">Word being placed into the word search.</param>
       /// <param name="currentWordSearch">The current modified word search.</param>
       /// <param name="orderType">Whether the word will be placed in order(0) or in reverse(1)</param>
       /// <param name="wasSuccessfullyPlaced">If the word could be successfully placed.</param>
       /// <returns>Returns the modified word search, now with the chosenWord in it.</returns>
        public static char[,] PlaceChosenWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType, ref bool wasSuccessfullyPlaced)
        {
            int[] validIndex = ReturnValidIndex(currentWordSearch, chosenWord, orderType);
            int validY = validIndex[0];
            int validX = validIndex[1];

            if (validY == -1 || validX == -1)
            {
                //Console.WriteLine("No valid rows to choose from");
                // Do nothing and continue to returning the unmodified word search
            }
            else
            {
                wasSuccessfullyPlaced = true;
                int chosenWord_index = 0;                                                                       // chosenWord index that will be used
                switch (orderType)
                {
                    case 0: // Outputs the chosenWord in order into the chosen row starting at ValidX
                        for (int xAxis = validX; chosenWord_index < chosenWord.Length; xAxis++, chosenWord_index++)
                        {
                            currentWordSearch[validY, xAxis] = chosenWord[chosenWord_index];
                        }
                        break;
                    case 1: // Outputs the chosenWord in reverse into the chosen row starting at validX
                        for (int xAxis = validX; chosenWord_index < chosenWord.Length; xAxis--, chosenWord_index++)
                        {
                            currentWordSearch[validY, xAxis] = chosenWord[chosenWord_index];
                        }
                        break;
                }
            }
            return currentWordSearch;
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
        public static bool CheckCoordinates(int userY, int userX, ref char[,] wordSearch, string chosenWord, bool isDiagonal)
        {
            int chosenWordIndex = 0;
            int y_axis = userY;

            if (isDiagonal == true)
            {
                y_axis = userY + userX ;
            }

            for (int x_axis = userX; x_axis < wordSearch.GetLength(1); x_axis++)                // Checks for word in order
            {
                if (wordSearch[y_axis, x_axis] == chosenWord[chosenWordIndex])                  // If each successive x-coordinate contains the successive letter in chosenWord 
                {
                    chosenWordIndex++;
                }
                if (chosenWordIndex == chosenWord.Length - 1)                                   // If the word was correctly found
                {
                    chosenWordIndex = 0;
                    for (int chosenSpace = x_axis + 1; chosenWordIndex < chosenWord.Length; chosenSpace--, chosenWordIndex++)   // Replace the chosenWord with '@'
                    {
                        wordSearch[y_axis, chosenSpace] = '@';
                    }
                    return true;
                }
            }
            // reset variable checker
            chosenWordIndex = 0;
            for (int x_axis = userX; x_axis >= wordSearch.GetLowerBound(1); x_axis--)           // Checks for word in reverse
            {
                if (wordSearch[y_axis, x_axis] == chosenWord[chosenWordIndex])                  // If each successive x-coordinate contains the successive letter in chosenWord 
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
