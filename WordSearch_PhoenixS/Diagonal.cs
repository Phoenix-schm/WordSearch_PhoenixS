namespace WordSearch_PhoenixS
{
    class Diagonal : SearchType
    {
        public static char[,] OutputWordInWordSearch(char[] chosenWord, char[,] currentWordSearch, int orderType, int diagonalType)
        {
            char[,] diagonalVersion = TransformToDiagonalWordSearch(currentWordSearch, diagonalType);
            diagonalVersion = PlaceChosenWordInWordSearch(chosenWord, diagonalVersion, orderType);

            currentWordSearch = RevertDiagonalWordSearchToNormal(diagonalVersion, diagonalType);

            return currentWordSearch;
        }
    }
}
