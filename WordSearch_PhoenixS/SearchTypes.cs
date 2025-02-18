
namespace WordSearch_PhoenixS
{
    internal class ReturnValue
    {


        public static Array[] SearchTypeVertical(char[] word, Array[] currentArray)
        {
            return currentArray;
        }

        public static Array[] SearchTypeDownwardsDiagonal(char[] word, Array[] currentArray)
        {
            return currentArray;
        }

        public static Array[] SearchTypeUpwardsDiagonal(char[] word, Array[] currentArray) 
        { 
            return currentArray; 
        }

        public static int PlaceWordAtIndex_InOrder(int row, int word)               // Outputs an index number that can accomodate placing 'word'
        {
            int index = row - word;
            int canFitHere = RandomNumber(2, index);
            return canFitHere;
        }
        public static int PlaceWordAtIndex_InReverse(int word)                      // Outputs an index number that can accomodate placing 'word' in reverse
        {
            int canFitRange = RandomNumber(word + 2, 22);
            return canFitRange;
        }
        public static int ChooseRandomRow()
        {
            int randomRow = RandomNumber(0, 20);
            return randomRow;
        }

        public static bool SkipThesePartsOfRow(char row, int i)
        {
            if (i == 0 || i == 1 || row == ' ')
            {
                return true;
            }
            else
            {
                return false;
            }
        }
        public static int RandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            int index = random.Next(minNumber, maxNumber);
            return index;
        }
        /// <summary>
        /// returns a row that doesn't have a word in it yet. Can be hopefully be reused for diagonals and columns
        /// </summary>
        /// <param name="wordSearch"> should take in only the letter parts of the word search </param>
        /// <param name="randomWord"> the list of eight random words it will check for</param>
        /// <returns> returns the index of valid a valid row </returns>
        public static int ReturnValidRow(char[,] wordSearch, string[] randomWord)
        {
            bool validRow = false;
            while (!validRow)                                                                             // while the row is invalid
            {
                int randomRow = RandomNumber(0, 20);                           // generate a randomRow
                
                // for insuring it won't go on forever if there is no valid rows
                int invalidRows = 0;
                int reverse_invalidRows = 0;

                for (int chosenRow = randomRow; chosenRow < wordSearch.GetLength(0); chosenRow++)  // chosenRow equals randomRow.
                {                                                                                       // If that row isn't valid then go to the next one
                    validRow = true;                                                                    // default is that the row is valid
                    char[] rowLetters = new char[22];   // create string from chosenRow
                    for (int x_axis = 0; x_axis < wordSearch.GetLength(1); x_axis++)
                    {
                        rowLetters[x_axis] = (wordSearch[chosenRow, x_axis]);
                    }

                    string stringRowLetters = string.Join("", rowLetters);
                    Array.Reverse(rowLetters);
                    string r_stringRowLetters = string.Join("", rowLetters);

                    //Array.Reverse(wordSearch[chosenRow]);                                        // Puts the chosenRow in reverse
                    //string r_RowLetters = string.Join("", (char[])wordSearch[chosenRow]);   // Create a string from the reversed chosenRow

                    for (int i = 0; i < randomWord.Length; i++)                                     // Going through each random word
                    {
                        string r_RandomWord = string.Empty;
                        for (int r_index = randomWord[i].Length -1; r_index >=0; r_index--)         // create a reverse version of the word
                        {
                            r_RandomWord += randomWord[i][r_index];
                        }

                        if (stringRowLetters.Contains(randomWord[i]) || stringRowLetters.Contains(r_RandomWord))        // if the row contains one of the randomWords
                        {
                            validRow = false;
                            invalidRows++;
                        }
                        if (r_stringRowLetters.Contains(randomWord[i]) || r_stringRowLetters.Contains(r_RandomWord))    // if the reverse row contains randomWords
                        {
                            validRow = false; 
                            reverse_invalidRows++;
                        }
                    }

                    if (validRow == true)
                    {
                        return chosenRow;
                    }
                }
                if (reverse_invalidRows == 19 && invalidRows == 19)                    // if there are no valid rows
                {
                    return -1;
                }
            }
            return -1;       // no valid row
        }

    }
    internal class Horizontal           // Hosts functions related to horizontal SearchTypes
    {
        /// <summary>
        /// Outputs a word in the word search from left to right
        /// </summary>
        /// <param name="chosenWord"> one of the random words </param>
        /// <param name="currentArray"> the current word search, modified each time function is used </param>
        /// <param name="categoryArray"> the list of 8 random words </param>
        /// <returns></returns>
        public static char[,] OutputWord_InOrder(char[] chosenWord, char[,] currentArray, string[] categoryArray)
        {
            int wordIndex = 0;                                                                       // chosenWord index that will be used
            int validRow = ReturnValue.ReturnValidRow(currentArray, categoryArray);            // Checks if the row already has a word in it.
                                                                                               // (should be changed later to instead see if chosenWord can fit alongside other word)
            if (validRow != -1)                                                                // in case shit went wrong
            {
                // chosenWord will begin to be placed at a location that will accomodate its size
                for (int x_axis = ReturnValue.PlaceWordAtIndex_InOrder(20, chosenWord.Length); wordIndex < chosenWord.Length; x_axis++)
                {
                    currentArray[validRow, x_axis] = chosenWord[wordIndex];                         // row at 'i' will now be changed to each letter of chosenWord
                    wordIndex++;
                }

            }
            else
            {
                Console.WriteLine("Row was null.");
            }
            return currentArray;
        }
        public static char[,] OutputWord_Reverse(char[] chosenWord, char[,] currentArray, string[] categoryArray)
        {
            int wordIndex = 0;                                                                       // chosenWord index that will be used
            int validRow = ReturnValue.ReturnValidRow(currentArray, categoryArray);            // Checks if the row already has a word in it.
                                                                                               // (should be changed later to instead see if chosenWord can fit alongside other word)
            if (validRow != -1)                                                                // in case shit went wrong
            {
                // chosenWord will begin to be placed at a location that will accomodate its size
                for (int x_axis = ReturnValue.PlaceWordAtIndex_InReverse(chosenWord.Length); wordIndex < chosenWord.Length; x_axis--)
                {
                    currentArray[validRow, x_axis] = chosenWord[wordIndex];                         // row at 'i' will now be changed to each letter of chosenWord
                    wordIndex++;
                }
            }
            else
            {
                Console.WriteLine("Row was null.");
            }
            return currentArray;
        }
    }
}
