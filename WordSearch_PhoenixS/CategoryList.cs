namespace WordSearch_PhoenixS
{
    public class CategoryList
    {
        /// <summary>
        ///  Creates an array holding just the list of items of a category
        /// </summary>
        /// <param name="categoryName"> the name of the category </param>
        /// <returns>A list of 15 strings.</returns>
        public static string[] CreateCategoryList(string categoryName)
        {
            string filePath = "words.txt";
            StreamReader file = new StreamReader(filePath);                     // Creates the file path if it doesn't exist
            string wordsFromFile = file.ReadToEnd();
            file.Close();

            string[] AllCategoriesList = wordsFromFile.Split("\r\n");
            string[] returnedWordList = new string[15];

            for (int i = 0; i < returnedWordList.Length; i++)
            {
                if (AllCategoriesList.Contains(categoryName))
                {
                    int position = Array.IndexOf(AllCategoriesList, categoryName) + 1;    // returns the index position of the first word in 'request' in wordList[]
                    returnedWordList[i] = AllCategoriesList[position + i];
                }
                else
                {
                    Console.WriteLine("Incorrect categoryName.");
                }
            }
            return returnedWordList;                                                 // Should return a valid a list full of the category words
        }
    }
}
