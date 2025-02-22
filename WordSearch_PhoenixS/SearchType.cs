﻿namespace WordSearch_PhoenixS
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
        public static int[] ReturnValidIndex(char[,] wordSearch, char[] chosenWord, int orderType)
        {
            int amountOfSpaces = wordSearch.GetLength(1);                                             // insure's this can be used for diagonal wordSearch placement
            int amountOfRows = wordSearch.GetLength(0);
            int[] randomRowsList = WordSearch.ReturnRandomNumberList(amountOfRows, amountOfRows);                           // Creates a list of random rows

            int xIndex = 0;

            for (int i = 0; i < randomRowsList.Length; i++)                                                       // Going through every row in a random assortment
            {
                int chosenRow = randomRowsList[i];

                int[] randomPositionList = WordSearch.ReturnRandomNumberList(amountOfSpaces, amountOfSpaces);     // Creates a random list of positions
                
                bool validRow = false;

                int minRange_NewWordPosition = 0;
                int maxRange_NewWordPosition = 0;

                maxRange_NewWordPosition = CheckRowIsEntirelyBlank(chosenRow, wordSearch, ref validRow, maxRange_NewWordPosition, chosenWord);

                for (int randomPosition = randomPositionList[0];  randomPosition < randomPositionList.Length; randomPosition++)     // Picks a random place in the row to start at
                {
                    if (validRow == true)                                                                       // if the CheckRowIsEntirelyBlank() returned true
                    {
                        break;
                    }
                    int additionalRange = 0;
                    int canFitHere = 0;

                    for (int letter = randomPosition; letter < amountOfSpaces; letter++)                        // checking every letter from that randomPosition
                    {
                        if (wordSearch[chosenRow, letter] == ' ' && canFitHere < chosenWord.Length)             // if there's blank spaces
                        {
                            canFitHere++;
                        }
                        else if (wordSearch[chosenRow, letter] == ' ' && letter < amountOfSpaces && canFitHere >= chosenWord.Length) // if there's still room in the row and the word can already fit
                        {
                            additionalRange++;
                        }
                        else if (wordSearch[chosenRow, letter] != ' ' && canFitHere >= chosenWord.Length)       // if the there is a letter, but the word can fit beforehand
                        {
                            additionalRange += canFitHere;
                            // defaults to in order index
                            minRange_NewWordPosition = letter - additionalRange;
                            maxRange_NewWordPosition = letter - chosenWord.Length;
                            validRow = true;
                            break;
                        }
                        else if (canFitHere >= chosenWord.Length)                                               // There's no more space, but the word can fit          
                        {
                            additionalRange += canFitHere;
                            // defaults to in order index
                            minRange_NewWordPosition = letter - additionalRange;
                            maxRange_NewWordPosition = letter - chosenWord.Length;
                            validRow = true;
                            break;
                        }
                        else                                                                                     // if there's no room for the new word
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
                    switch (orderType)
                    {
                        case 0:                 // in order
                            xIndex = RandomNumber(minRange_NewWordPosition, maxRange_NewWordPosition);
                            break;
                        case 1:                 // in reverse
                            minRange_NewWordPosition += chosenWord.Length - 1;
                            maxRange_NewWordPosition += chosenWord.Length;
                            xIndex = RandomNumber(minRange_NewWordPosition, maxRange_NewWordPosition);
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
        static int CheckRowIsEntirelyBlank(int _chosenRow, char[,] _wordSearch, ref bool rowValidity, int maxRange, char[] _chosenWord)
        {
            int isFilledWithBlanks = 0;
            int rowLength = _wordSearch.GetLength(1);

            for (int letter = 0; letter < rowLength; letter++)                                  // checks row for only contains blank spaces
            {
                if (_wordSearch[_chosenRow, letter] == ' ')
                {
                    isFilledWithBlanks++;
                }
                if (isFilledWithBlanks > 19)                                                    // if the whole row contains blanks
                {
                    rowValidity = true;
                    maxRange = 19 - _chosenWord.Length;
                    break;
                }                                                                               // else, maxRange doesn't change
            }

            return maxRange;
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
        public static char[,] DiagonalWordSearch(char[,] currentWordSearch)
        {
            char[,] diagonalWordSearch = new char[40, 20];
            int iterator = 0;

            for (int i = 0; i < diagonalWordSearch.GetLength(0); i++)
            {
                if (i >= 20)
                {
                    iterator++;
                }

                for (int y_axis = 0; y_axis< currentWordSearch.GetLength(0); y_axis++)
                {
                    for (int x_axis = 0; x_axis< currentWordSearch.GetLength(1); x_axis++)
                    {
                        if (y_axis + x_axis == i && i < 20)
                        {
                            diagonalWordSearch[i, x_axis] = currentWordSearch[y_axis, x_axis];
                        }
                        else if (y_axis + x_axis == i && i >= 20)
                        {
                            diagonalWordSearch[i, x_axis - iterator] = currentWordSearch[y_axis, x_axis];
                        }

                    }
                }
                for (int x_axisDiagonal = 0; x_axisDiagonal < diagonalWordSearch.GetLength(1); x_axisDiagonal++)
                {
                    if (diagonalWordSearch[i, x_axisDiagonal] != ' ')
                    {
                        continue;
                    }
                    else
                    {
                        diagonalWordSearch[i, x_axisDiagonal] = '0';
                    }
                }
            }

            for (int y_axis = 0; y_axis < diagonalWordSearch.GetLength(0); y_axis++ )
            {
                for (int x_axis = 0; x_axis < diagonalWordSearch.GetLength(1); x_axis++)
                {
                    if (Char.IsLetter(diagonalWordSearch[y_axis, x_axis]))                               // if there's a letter, turn it green (for debugging purposes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + diagonalWordSearch[y_axis, x_axis] + " ");
                    }
                    else                                                                        // else, fill the word search with random letters
                    {
                        Console.ResetColor();
                        Console.Write(" " + diagonalWordSearch[y_axis, x_axis] + " ");
                    }
                }
                Console.WriteLine();
            }
            
            return diagonalWordSearch;
        }

    }
}
