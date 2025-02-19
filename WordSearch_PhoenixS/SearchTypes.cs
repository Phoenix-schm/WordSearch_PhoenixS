
using System.Runtime.ExceptionServices;

namespace WordSearch_PhoenixS
{
    class ReturnValue
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
        public static int RandomNumber(int minNumber, int maxNumber)
        {
            Random random = new Random();
            int index = random.Next(minNumber, maxNumber);
            return index;
        }


        /// <summary>
        /// returns a row that doesn't have a word in it yet. Can be hopefully be reused for diagonals and columns
        /// </summary>
        /// <param name="wordSearch_version"> should take in only the letter parts of the word search </param>
        /// <param name="wordList"> the list of eight random words it will check for</param>
        /// <returns> returns the index of valid a valid row </returns>
        public static int ReturnValidRow(char[,] wordSearch_version, string[] wordList, char[] chosenWord)
        {
            bool validRow = false;
            int chosenWordLength = chosenWord.Length;

            while (!validRow)                                                                           // While the row is invalid
            {
                int randomRow = RandomNumber(0, 20);                                                    // Generate a randomRow
                int invalidRows = 0;                                                                    // variable insures it won't go on forever
                int wordLength = 0;
                int positionOfWord_max = 0;

                for (int chosenRow = randomRow; chosenRow < wordSearch_version.GetLength(0); chosenRow++)   // ChosenRow equals randomRow.
                {                                                                                                // if that row isn't valid then go to the next one
                    validRow = true;                                                                    // Default is that the row is valid

                    for (int letter = 0; letter < wordSearch_version.GetLength(1);  letter++)           // Going through each letter in the chosenRow
                    {
                        if (Char.IsLetter(wordSearch_version[chosenRow, letter]))                        // if chosenRow contains a lowercase letter
                        {
                            validRow = false;                                                           // then the chosenRow is invalid

                            positionOfWord_max = Array.IndexOf(wordList, letter);                             // gets the index position of the last letter
                            wordLength++;
                        }
                    }

                    int positionOfWord_min = positionOfWord_max - wordLength;
                    // checking if the word can fit before the other word
                    for (int letterInWord = positionOfWord_min; letterInWord > wordSearch_version.GetLength(1) - 2; letterInWord--)
                    {
                        chosenWordLength--;
                        if (chosenWordLength <= 0)
                        {
                            validRow = true;
                        }
                    }
                    // checking if the word can fit after the other word
                    for (int letterInWord = positionOfWord_max; letterInWord < wordSearch_version.GetLength(1); letterInWord++)
                    {
                        chosenWordLength--;
                        if (chosenWordLength <= 0)
                        {
                            validRow = true;
                        }
                    }

                    if (validRow == true)
                    {
                        return chosenRow;
                    }
                    else
                    {
                        invalidRows++;
                    }
                }
                if (invalidRows == 19)                    // if there are no valid rows
                {
                    return -1;
                }
            }
            return -1;       // no valid row
        }

    }
    public class Horizontal           // Hosts functions related to horizontal SearchTypes
    {
        /// <summary>
        /// Outputs a word in the word search from left to right
        /// </summary>
        /// <param name="chosenWord"> one of the random words </param>
        /// <param name="currentWordSearch"> the current word search, modified each time function is used </param>
        /// <param name="eightCategoryWords"> the list of 8 random words </param>
        /// <returns></returns>
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, string[] eightCategoryWords, int randomChoice)
        {
            int wordIndex = 0;                                                                       // chosenWord index that will be used
            int validRow = ReturnValue.ReturnValidRow(currentWordSearch, eightCategoryWords);            // Checks if the row already has a word in it.
                                                                                               // (should be changed later to instead see if chosenWord can fit alongside other word)
            if (validRow != -1)                                                                // in case shit went wrong
            {
                switch(randomChoice)
                {
                    case 0:         // output word in word search in order
                        // chosenWord will begin to be placed at a location that will accomodate its size
                        for (int x_axis = ReturnValue.PlaceWordAtIndex_InOrder(20, chosenWord.Length); wordIndex < chosenWord.Length; x_axis++, wordIndex++)
                        {
                            currentWordSearch[validRow, x_axis] = chosenWord[wordIndex];                         // row at 'i' will now be changed to each letter of chosenWord
                        }
                        break;
                    case 1:         // outputs word in word search in reverse
                        // chosenWord will begin to be placed at a location that will accomodate its size
                        for (int x_axis = ReturnValue.PlaceWordAtIndex_InReverse(chosenWord.Length); wordIndex < chosenWord.Length; x_axis--, wordIndex++)
                        {
                            currentWordSearch[validRow, x_axis] = chosenWord[wordIndex];                         // row at 'i' will now be changed to each letter of chosenWord
                        }
                        break;
                }
            }
            else
            {
                Console.WriteLine("No valid rows to choose from");
            }
            return currentWordSearch;
        }
    }
    public class Vertical
    {
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, string[] eightCategoryWords, int randomChoice)
        {

            return currentWordSearch;
        }
    }
}
