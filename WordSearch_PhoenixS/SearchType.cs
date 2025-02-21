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
        /// <param name="wordSearch"> should take in only the letter parts of the word search </param>
        /// <param name="wordList"> the list of eight random words it will check for</param>
        /// <param name="chosenWord"> The char array holding the word being inputed into the current word search </param>
        /// <param name="orderType"> Whether the word is being placed in order(0) or in reverse(1) </param>
        /// <returns> returns the index of valid a valid row </returns>
        public static int[] ReturnValidIndex(char[,] wordSearch, string[] wordList, char[] chosenWord, int orderType)
        {
            int amountOfRows = wordSearch.GetLength(0);
            int amountofLetters = wordSearch.GetLength(1);

            int[] randomRowsList = WordSearch.ReturnRandomNumberList(amountOfRows, amountOfRows);                           // Creates a list of random rows

            int xIndex = 0;

            for (int i = 0; i < randomRowsList.Length; i++)                                             // going through every row in a random assortment
            {
                int[] randomLettersList = WordSearch.ReturnRandomNumberList(amountofLetters, amountofLetters);
                int chosenRow = randomRowsList[i];

                bool validRow = false;

                int minimumRange = 0;
                int maximumRange = 0;
                int isFilledWithBlanks = 0;

                for (int letter = 0; letter < amountofLetters; letter++)                                    // checks row for if it only contains blank spaces
                {
                    if (wordSearch[chosenRow, letter] == ' ')
                    {
                        isFilledWithBlanks++;
                    }
                    if (isFilledWithBlanks >= 19)
                    {
                        validRow = true;
                        maximumRange = 19 - chosenWord.Length;
                        break;
                    }
                }
                

                for (int j = randomLettersList[0];  j < randomLettersList.Length; j++)                      // Picks a random place in the row to start at
                {
                    if (validRow == true)
                    {
                        break;
                    }
                    int additionalRange = 0;
                    int canFitHere = 0;
                    for (int letter = j; letter < wordSearch.GetLength(1); letter++)                        // checking every letter from that starting point
                    {
                        if (wordSearch[chosenRow, letter] == ' ' && canFitHere < chosenWord.Length)          // if there's blank spaces
                        {
                            canFitHere++;
                        }
                        else if (wordSearch[chosenRow, letter] != ' ' && canFitHere >= chosenWord.Length)    // if the there is a letter, but the word can fit inside
                        {
                            additionalRange += canFitHere;
                            // defaults to in order index
                            minimumRange = letter - additionalRange;
                            maximumRange = letter - chosenWord.Length;
                            validRow = true;
                            break;
                        }
                        else if (wordSearch[chosenRow, letter] == ' ' && letter < wordSearch.GetLength(1) && canFitHere >= chosenWord.Length)
                        {
                            additionalRange++;
                        }
                        else if (wordSearch[chosenRow, letter] == ' ' && canFitHere >= chosenWord.Length)
                        {
                            additionalRange += canFitHere;
                            // defaults to in order index
                            minimumRange = letter - additionalRange;
                            maximumRange = letter - chosenWord.Length;
                            validRow = true;
                            break;
                        }
                        else                                                                                // if there's a letter
                        {
                            canFitHere = 0;
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
                    switch (orderType)
                    {
                        case 0:                 // in order
                            xIndex = RandomNumber(minimumRange, maximumRange);
                            break;
                        case 1:                 // in reverse
                            minimumRange += chosenWord.Length - 1;
                            maximumRange += chosenWord.Length;
                            xIndex = RandomNumber(minimumRange, maximumRange);
                            //xIndex = validXIndex + chosenWord.Length - 1;
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
