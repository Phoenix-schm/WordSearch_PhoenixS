namespace WordSearch_PhoenixS
{
    public class SearchType
    {
        /// <summary>
        /// Outputs a random number between minNumber(inclusive) and maxNumber(exclusive)
        /// </summary>
        /// <param name="minNumber"></param>
        /// <param name="maxNumber"></param>
        /// <returns></returns>
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
        /// <param name="chosenWord"> The char array holding the word being inputed into the current word search </param>
        /// <param name="orderType"> Whether the word is being placed in order(0) or in reverse(1) </param>
        /// <returns> returns the index of valid a valid row </returns>
        public static int[] ReturnValidIndex(char[,] wordSearch_version, string[] wordList, char[] chosenWord, int orderType)
        {
            int[] randomRowsList = WordSearch.ReturnRandomNumberList(20, 20);                           // Creates a list of random rows

            int xIndex = 0;

            for (int i = 0; i < randomRowsList.Length; i++)                                             // going through every row in a random assortment
            {
                int[] randomLettersList = WordSearch.ReturnRandomNumberList(20, 20);
                int chosenRow = randomRowsList[i];

                bool validRow = true;
                int validXIndex = 0;
                int canFitHere = 0;

                for (int letter = 0; letter < wordSearch_version.GetLength(1); letter++)                // checking every letter in row
                {
                    if (wordSearch_version[chosenRow,letter] == ' ' && canFitHere < chosenWord.Length)       // if there's blank spaces
                    {
                        canFitHere++;
                    }
                    else if (canFitHere >= chosenWord.Length)                                            // if the word can fit inside those blank spaces
                    {
                        validRow = true;
                        validXIndex = letter - chosenWord.Length;
                        break;
                    }
                    else                                                                                 // if there's a letter
                    {
                        canFitHere = 0;
                        validRow = false;
                    }

                }

   
                if (validRow == true)
                {
                    switch (orderType)
                    {
                        case 0:                 // in order
                            xIndex = validXIndex;
                            break;
                        case 1:                 // in reverse
                            xIndex = validXIndex + chosenWord.Length - 1;
                            break;
                    }
                    int[] validIndex = { chosenRow, xIndex };
                    return validIndex;
                }
            }

            Console.WriteLine("Didn't get a valid row");
            int[] invalidIndex = { -1, -1 };
            return invalidIndex;
        }
        public static char[,] RotateWordSearch(char[,] currentWordSearch)
        {
            char[,] verticalWordSearch = new char[20, 20];

            for (int x_axis = 0; x_axis < currentWordSearch.GetLength(1); x_axis++)
            {
                for (int y_axis = 0; y_axis < currentWordSearch.GetLength(0); y_axis++)
                {
                    verticalWordSearch[x_axis, y_axis] = currentWordSearch[y_axis, x_axis];
                }
            }
            return verticalWordSearch;
        }
    }
}
