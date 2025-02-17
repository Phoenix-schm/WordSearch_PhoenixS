using System;
using System.IO;

namespace WordSearch_PhoenixS
{
    internal class SearchTypes
    {
        public static Array[] SearchTypeHorizontal(char[] chosenWord, Array[] currentArray, string[] categoryArray)
        {
            int wordIndex = 0;
            int index = 0;                                          // chooses a random row

            char[] row = ContainsWordInCategoryCheck_Horizontal(ref index, currentArray, categoryArray);

            for ( int i = PlaceWordAtIndex(62, chosenWord.Length); i < row.Length; i++ )
            {
                if (wordIndex < chosenWord.Length)
                {
                    if (SkipThesePartsOfRow(row[i], i) == true)
                    {
                        continue;
                    }

                    else if (SkipThesePartsOfRow(row[i], i) == false)
                    { 
                        row[i] = chosenWord[wordIndex];
                        wordIndex++;
                    }
                    else
                    {
                        break;
                    }

                }
            }
            currentArray[index] = row;
            return currentArray;

        }

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

        public static int PlaceWordAtIndex(int row, int word)
        {
            int trueWordLength = word * 2;
            int index = row - trueWordLength;
            int canFitRange = WordSearch.RandomNumber(3, index - 11);
            return canFitRange;
        }
        public static int ChooseRandomRow()
        {
            int randomRow = WordSearch.RandomNumber(0, 20);
            return randomRow;
        }
        public static char[] JustTheWordsInRow(char[] row)
        {
            char[] justTheWords = new char[20];
            int newIndex = 0;

            for (int i = 0; i < row.Length; i++)
            {

                if (SkipThesePartsOfRow(row[i], i) == true)
                {
                    continue;
                }
                else
                {
                    if (newIndex <  justTheWords.Length)
                    {
                        justTheWords[newIndex] = row[i];
                        newIndex++;
                    }
                    else
                    {
                        break;
                    }
                }
            }
            return justTheWords;
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
        static char[] ContainsWordInCategoryCheck_Horizontal(ref int index, Array[] currentArray, string[] category)
        {
            bool alreadyFilled = true;
            char[] shitWentWrong = null;
            while (alreadyFilled)
            {
                index = ChooseRandomRow();                                          // chooses a random row
                char[] row = (char[])currentArray[index];                               // makes a row[] from currentArray at index

                string rowLetters = string.Join(string.Empty, JustTheWordsInRow(row));  // converts row into a string (for comparisons)

                foreach (string word in category)
                {
                    if (rowLetters.Contains(word))                                    // if the chosen row already contains a word
                    {
                        alreadyFilled = true;
                    }
                    else
                    {
                        alreadyFilled = false;
                        return row;
                    }
                }
            }
            return shitWentWrong;
        }
    }
}
