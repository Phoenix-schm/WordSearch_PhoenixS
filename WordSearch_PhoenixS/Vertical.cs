using System.Security;

namespace WordSearch_PhoenixS
{
    public class Vertical : SearchType
    {
        /// <summary>
        /// Outputs words into the word search vertically (on y axis)
        /// </summary>
        /// <param name="chosenWord"></param>
        /// <param name="currentWordSearch"></param>
        /// <param name="orderType"></param>
        /// <returns></returns>
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType)
        {
            currentWordSearch = RotateWordSearch(currentWordSearch);            // rotate currentWordSearch so that it can output vertically

            currentWordSearch = PlaceChosenWordInWordSearch(chosenWord, currentWordSearch, orderType);

            currentWordSearch = RotateWordSearch(currentWordSearch);           // rotates the wordsearch back to its orginal postion

            return currentWordSearch;
        }
    }
}
