using System.ComponentModel.Design;

namespace WordSearch_PhoenixS
{
    class Diagonal : SearchType
    {
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType, int diagonalType, ref bool wasWordPlaced)
        {
            char[,] diagonalVersion = TransformToDiagonalWordSearch(currentWordSearch, diagonalType);
            diagonalVersion = PlaceChosenWordInWordSearch(chosenWord, diagonalVersion, orderType, ref wasWordPlaced);

            currentWordSearch = RevertDiagonalWordSearchToNormal(diagonalVersion, diagonalType);

            return currentWordSearch;
        }

        public static bool CheckUserCoordinates(int userY, int userX, ref char[,] wordSearch, string chosenWord)
        {
            char[,] upSlopeDiagonalWordSearch = TransformToDiagonalWordSearch(wordSearch, 4);
            char[,] downSlopeDiagonalWordSearch = TransformToDiagonalWordSearch(wordSearch, 6);


            bool isValidCoordinates = CheckCoordinates(userY, userX, ref upSlopeDiagonalWordSearch, chosenWord, true);
            if (!isValidCoordinates)
            {
                userX = wordSearch.GetLength(1) - userX - 1;
                isValidCoordinates = CheckCoordinates(userY, userX, ref downSlopeDiagonalWordSearch, chosenWord, true);
                wordSearch = RevertDiagonalWordSearchToNormal(downSlopeDiagonalWordSearch, 6);
            }
            else
            {
                wordSearch = RevertDiagonalWordSearchToNormal(upSlopeDiagonalWordSearch, 4);
            }

            return isValidCoordinates;    
        }
    }
}
