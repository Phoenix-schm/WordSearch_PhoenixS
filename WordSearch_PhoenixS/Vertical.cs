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
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType, ref bool wasWordPlaced)
        {
            currentWordSearch = RotateWordSearch(currentWordSearch);            // rotate currentWordSearch so that it can output vertically

            currentWordSearch = PlaceChosenWordInWordSearch(chosenWord, currentWordSearch, orderType, ref wasWordPlaced);

            currentWordSearch = RotateWordSearch(currentWordSearch);           // rotates the wordsearch back to its orginal postion

            return currentWordSearch;
        }

        public static bool CheckUserCoordinates(int userY, int userX, char[,] wordSearch, string chosenWord)
        {
            wordSearch = RotateWordSearch(wordSearch);
            bool isValidCoordinates = CheckCoordinates(userX, userY, wordSearch, chosenWord, false);        // reverse x and y to accomodate rotation 
            return isValidCoordinates;
        }
    }
}
