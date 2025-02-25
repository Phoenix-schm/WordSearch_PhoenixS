using System.IO;

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

            string[] wordList = wordsFromFile.Split("\r\n");
            string[] returnedList = new string[15];

            for (int i = 0; i < returnedList.Length; i++)
            {
                if (wordList.Contains(categoryName))
                {
                    int position = Array.IndexOf(wordList, categoryName) + 1;    // returns the index position of the first word in 'request' in wordList[]
                    returnedList[i] = wordList[position + i];
                }
                else
                {
                    Console.WriteLine("Incorrect categoryName.");
                }
            }
            return returnedList;                                                 // Shouldn't happen if 'categoryName' is valid, Will return a blank array of 15 lines
        }
    }
}
