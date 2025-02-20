using System.Security;

namespace WordSearch_PhoenixS
{
    public class Vertical : SearchType
    {
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, string[] eightCategoryWords, int orderType)
        {
            int chosenWord_index = 0;                                                                       // chosenWord index that will be used
            currentWordSearch = RotateWordSearch(currentWordSearch);


            int[] validIndex = ReturnValidIndex(currentWordSearch, eightCategoryWords, chosenWord, orderType);
            int validY = validIndex[0];
            int validX = validIndex[1];

            if (validY == -1 || validX == -1)
            {
                Console.WriteLine("No valid rows to choose from");
            }
            else
            {
                switch (orderType)
                {
                    case 2: // outputs the random word in order into the chosen row starting at ValidX
                        for (int xAxis = validX; chosenWord_index < chosenWord.Length; xAxis++, chosenWord_index++)
                        {
                            currentWordSearch[validY, xAxis] = chosenWord[chosenWord_index];
                        }
                        break;
                    case 3: // outputs the random word in reverse into the chosen row starting at validX
                        for (int xAxis = validX; chosenWord_index < chosenWord.Length; xAxis--, chosenWord_index++)
                        {
                            currentWordSearch[validY, xAxis] = chosenWord[chosenWord_index];
                        }
                        break;
                }
            }
            WordSearch.DisplayWordSearch(currentWordSearch);

            return currentWordSearch;
        }
    }
}
