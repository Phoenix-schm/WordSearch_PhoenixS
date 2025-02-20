
using System.Runtime.ExceptionServices;

namespace WordSearch_PhoenixS
{
    public class SearchType
    {
        public static int PlaceWordAtIndex_InOrder(int row, int word)               // Outputs an index number that can accomodate placing 'word'
        {
            int index = row - word;
            int canFitHere = RandomNumber(0, index);
            return canFitHere;
        }
        public static int PlaceWordAtIndex_InReverse(int word)                      // Outputs an index number that can accomodate placing 'word' in reverse
        {
            int canFitRange = RandomNumber(word, 20);
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
        public static int[] ReturnValidIndex(char[,] wordSearch_version, string[] wordList, char[] chosenWord, int orderType)
        {
            int[] randomRowsList = WordSearch.ReturnRandomNumberList(8, 9);                           // Creates a list of random rows
            int xIndex = 0;

            for (int i = 0; i < randomRowsList.Length; i++)                                             // going through every row in a random assortment
            {
                int chosenRow = randomRowsList[i];
                bool validRow = true;
                int otherWord_Length = 0;
                int otherWord_positionMax = 0;
                bool rowIsFull = false;

                for (int letter = 0; letter < wordSearch_version.GetLength(1); letter++)                // checking every letter in row
                {
                    if (Char.IsLetter(wordSearch_version[chosenRow, letter]))                           // if row contains a letter
                    {
                        validRow = false;
                        otherWord_positionMax = letter;                                                 // will return the maxIndex position of the other word
                        otherWord_Length++;                                                             // each iteration adds to the other word length
                    }
                    if (validRow == false && letter == ' ')
                    {
                        for (int j = letter; letter < wordSearch_version.GetLength(1); letter++)
                        {
                            if (Char.IsLetter(wordSearch_version[chosenRow, letter]))
                            {
                                rowIsFull = true;
                            }
                        }
                    }
                }

                if (validRow == false && rowIsFull == false)
                {
                    int otherWord_positionMin = otherWord_positionMax - otherWord_Length;
                    int possibleXIndex = CheckForValidXindex(chosenRow, chosenWord, otherWord_positionMin, otherWord_positionMax, wordSearch_version, orderType);

                    if (possibleXIndex < 0)                                                             // if there is no place for the word to fit
                    {
                        validRow = false;
                    }
                    else
                    {
                        xIndex = possibleXIndex;
                        int[] validIndex = {chosenRow, xIndex};
                        return validIndex;
                    }
                }
   
                if (validRow == true)
                {
                    //Console.WriteLine("There was no other word in this row");
                    switch(orderType)
                    {
                        case 0:
                            int minRange = 0;
                            int maxRange = wordSearch_version.GetLength(1) - chosenWord.Length -  1;
                            xIndex = RandomNumber(minRange, maxRange);
                            break;
                        case 1:
                            minRange = chosenWord.Length;
                            maxRange = wordSearch_version.GetLength(1);
                            xIndex = RandomNumber(minRange, maxRange);
                            break;
                    }
                    int[] validIndex = {chosenRow, xIndex};
                    return validIndex;
                }
            }

            Console.WriteLine("Didn't get to the point to get a valid row");
            int[] invalidIndex = { -1, -1 };
            return invalidIndex;
        }

        static int CheckForValidXindex(int chosenRow, char[] chosenWord, int positionofOtherWord_min, int positionOfOtherWord_max, char[,] wordSearch_version, int orderType)
        {
            int xIndex = -1;                                                    // Defaults at -1
            int chosenWordLength = chosenWord.Length;

            // Checks if the chosenWord can fit before the other word
            for (int maxIndex = positionofOtherWord_min - 1; maxIndex > wordSearch_version.GetLength(1); maxIndex--)
            {
                chosenWordLength--;
                if (chosenWordLength <= 0)
                {
                    //Console.WriteLine("The word can fit before the other word");
                    switch(orderType)
                    {
                        case 0:     // in order
                            int maxRange = positionofOtherWord_min - chosenWord.Length;
                            int minRange = 0;
                            xIndex = RandomNumber(minRange, maxRange + 1);
                            return xIndex;
                        case 1:    // in reverse
                            maxRange = positionofOtherWord_min - 1;
                            minRange = chosenWord.Length;
                            xIndex = RandomNumber(minRange, maxRange + 1);
                            return xIndex;
                    }
                }
            }
            chosenWordLength = chosenWord.Length;
            // Checks if the chosenWord can fit after the other word
            for (int minIndex = positionOfOtherWord_max + 1; minIndex < wordSearch_version.GetLength(1); minIndex++)
            {
                chosenWordLength--;
                if (chosenWordLength <= 0)
                {

                    //Console.WriteLine("The word can fit after the other word");
                    switch(orderType)
                    {
                        case 0:         // in order
                            int maxRange = wordSearch_version.GetLength(1) - chosenWord.Length - 1;
                            int minRange = positionOfOtherWord_max + 1;
                            xIndex = RandomNumber(minRange, maxRange + 1);
                            return xIndex;
                        case 1:         // reverse
                            maxRange = wordSearch_version.GetLength(1) - 1;
                            minRange = positionOfOtherWord_max + chosenWord.Length;
                            xIndex = RandomNumber(minRange, maxRange + 1);
                            return xIndex;
                    }
                }
            }
            //Console.WriteLine("There wasn't a valid x coordinate");

            return xIndex;                              // if it can't fit, then return a -1
        }

    }
}
