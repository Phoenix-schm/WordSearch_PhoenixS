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

        public static bool CheckUserCoordinates(int userY, int userX, char[,] wordSearch, string chosenWord)
        {
            char[,] upSlopeDiagonalWordSearch = TransformToDiagonalWordSearch(wordSearch, 4);
            char[,] downSlopDiagonalWordSearch = TransformToDiagonalWordSearch(wordSearch, 6);

            for (int y = 0; y < downSlopDiagonalWordSearch.GetLength(0); y++)
            {
                for (int x = 0; x < downSlopDiagonalWordSearch.GetLength(1); x++)
                {
                    if (downSlopDiagonalWordSearch[y, x] == ' ')
                    {
                        Console.Write('0');
                    }
                    else
                    {
                        Console.Write(downSlopDiagonalWordSearch[y, x]);
                    }
                }
                Console.WriteLine();
            }

            bool isValidCoordinates = CheckCoordinates(userY, userX, upSlopeDiagonalWordSearch, chosenWord, true);
            if (!isValidCoordinates)
            {
                userX = wordSearch.GetLength(1) - userX - 1;
                isValidCoordinates = CheckCoordinates(userY, userX, downSlopDiagonalWordSearch, chosenWord, true);
            }

            return isValidCoordinates;    
        }
    }
}
