
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
            int trueWordLength = word * 3;
            int index = row - trueWordLength;
            int canFitHere = RandomNumber(3, index);
            return canFitHere;
        }
        public static int PlaceWordAtIndex_InReverse(int word)                      // Outputs an index number that can accomodate placing 'word' in reverse
        {
            int trueWordLength = word * 3;
            int canFitRange = RandomNumber(trueWordLength, 62);
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
        /// <param name="justLettersWordSearch"> should take in only the letter parts of the word search </param>
        /// <param name="randomWord"> the list of eight random words it will check for</param>
        /// <returns> returns the index of valid a valid row </returns>
        public static int ReturnValidRow(Array[] justLettersWordSearch, string[] randomWord)
        {
            bool validRow = false;
            while (!validRow)                                                                             // while the row is invalid
            {
                int randomRow = RandomNumber(0, justLettersWordSearch.Length);                           // generate a randomRow
                
                // for insuring it won't go on forever if there is no valid rows
                int invalidRows = 0;
                int reverse_invalidRows = 0;

                for (int chosenRow = randomRow; chosenRow < justLettersWordSearch.Length; chosenRow++)  // chosenRow equals randomRow.
                {                                                                                       // If that row isn't valid then go to the next one
                    validRow = true;                                                                    // default is that the row is valid
                    string rowLetters = string.Join("", (char[])justLettersWordSearch[chosenRow]);      // create string from chosenRow

                    Array.Reverse(justLettersWordSearch[chosenRow]);                                        // Puts the chosenRow in reverse
                    string r_RowLetters = string.Join("", (char[])justLettersWordSearch[chosenRow]);   // Create a string from the reversed chosenRow

                    for (int i = 0; i < randomWord.Length; i++)                                     // Going through each random word
                    {
                        string r_RandomWord = string.Empty;
                        for (int r_index = randomWord[i].Length -1; r_index >=0; r_index--)         // create a reverse version of the word
                        {
                            r_RandomWord += randomWord[i][r_index];
                        }

                        if (rowLetters.Contains(randomWord[i]) || rowLetters.Contains(r_RandomWord))        // if the row contains one of the randomWords
                        {
                            validRow = false;
                            invalidRows++;
                        }
                        if (r_RowLetters.Contains(randomWord[i]) || r_RowLetters.Contains(r_RandomWord))    // if the reverse row contains randomWords
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
        public static Array[] OutputWord_InOrder(char[] chosenWord, Array[] currentArray, string[] categoryArray)
        {
            int wordIndex = 0;                                                                       // chosenWord index that will be used

            Array[] justLettersWordSearch = JustTheLetters_Horizontal(currentArray);                 // only the letters in the word search. For hosting comparisons
            int validRow = ReturnValue.ReturnValidRow(justLettersWordSearch, categoryArray);            // Checks if the row already has a word in it.
                                                                                                     // (should be changed later to instead see if chosenWord can fit alongside other word)
            char[] row = (char[])currentArray[validRow];                                             // the current row being changed

            if (row != null)                                    // in case shit went wrong
            {
                // chosenWord will begin to be placed at a location that will accomodate its size
                for (int i = ReturnValue.PlaceWordAtIndex_InOrder(62, chosenWord.Length); wordIndex < chosenWord.Length; i++)
                {
                    if (ReturnValue.SkipThesePartsOfRow(row[i], i) == true)        // skips the numbers and spaces
                    {
                        continue;
                    }
                    else
                    {
                        row[i] = chosenWord[wordIndex];                         // row at 'i' will now be changed to each letter of chosenWord
                        wordIndex++;
                    }
                }

            }
            else
            {
                Console.WriteLine("Row was null.");
            }
            return currentArray;
        }
        /// <summary>
        /// Outputs chosenWord in reverse order from right to left
        /// </summary>
        /// <param name="chosenWord"></param>
        /// <param name="currentArray"></param>
        /// <param name="categoryArray"></param>
        /// <returns></returns>
        public static Array[] OutputWord_Reverse(char[] chosenWord, Array[] currentArray, string[] categoryArray)
        {
            int wordIndex = 0;

            Array[] justLettersWordSearch = JustTheLetters_Horizontal(currentArray);
            int validRow = ReturnValue.ReturnValidRow(justLettersWordSearch, categoryArray);
            char[] row = (char[])currentArray[validRow];

            if (row != null)
            {
                for (int i = ReturnValue.PlaceWordAtIndex_InReverse(chosenWord.Length); wordIndex < chosenWord.Length; i--)
                {
                    if (ReturnValue.SkipThesePartsOfRow(row[i], i) == true)
                    {
                        continue;
                    }
                    else
                    {
                        row[i] = chosenWord[wordIndex];
                        wordIndex++;
                    }
                }

            }
            else
            {
                Console.WriteLine("Row was null.");
            }
            return currentArray;
        }
        public static Array[] JustTheLetters_Horizontal(Array[] currentWordSearch)
        {
            Array[] lettersWordSearch = new Array[currentWordSearch.Length];
            int newLetterIndex = 0;
            int newArrayIndex = 0;

            foreach (char[] row in currentWordSearch)
            {
                newLetterIndex = 0;
                char[] justTheLetters = new char[20];
                for (int i = 0; i < row.Length; i++)
                {
                    if (ReturnValue.SkipThesePartsOfRow(row[i], i) == true)
                    {
                        continue;
                    }
                    else
                    {
                        if (newLetterIndex < justTheLetters.Length)
                        {
                            justTheLetters[newLetterIndex] = row[i];
                            newLetterIndex++;
                        }
                        else
                        {
                            break;
                        }
                    }
                }
                lettersWordSearch[newArrayIndex] = justTheLetters;
                newArrayIndex++;
            }
            return lettersWordSearch;
        }

    }
}
