using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Text;
using System.Threading.Tasks;

namespace WordSearch_PhoenixS
{
    public class Horizontal : SearchType         // Hosts functions related to horizontal SearchTypes
    {
        /// <summary>
        /// Outputs a word in the word search from left to right
        /// </summary>
        /// <param name="chosenWord"> one of the random words </param>
        /// <param name="currentWordSearch"> the current word search, modified each time function is used </param>
        /// <param name="eightCategoryWords"> the list of 8 random words </param>
        /// <returns></returns>
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, string[] eightCategoryWords, int orderType)
        {
            int chosenWord_index = 0;                                                                       // chosenWord index that will be used

            int[] validIndex = ReturnValidIndex(currentWordSearch, eightCategoryWords, chosenWord, orderType);
            int validY = validIndex[0];
            int validX = validIndex[1];

            if (validY == -1 || validX == -1)
            {
                Console.WriteLine("No valid rows to choose from");
            }
            else
            {
                switch(orderType)
                {
                    case 0: // outputs the random word in order into the chosen row starting at ValidX
                        for (int xAxis = validX; chosenWord_index < chosenWord.Length; xAxis++, chosenWord_index++)
                        {
                            currentWordSearch[validY, xAxis] = chosenWord[chosenWord_index];
                        }
                        break;
                    case 1: // outputs the random word in reverse into the chosen row starting at validX
                        for (int xAxis = validX; chosenWord_index < chosenWord.Length; xAxis--, chosenWord_index++)
                        {
                            currentWordSearch[validY, xAxis] = chosenWord[chosenWord_index];
                        }
                        break;
                }
            }

            return currentWordSearch;
        }
    }
}
