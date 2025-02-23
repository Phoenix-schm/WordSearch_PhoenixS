using System.IO;

namespace WordSearch_PhoenixS
{
    public class CategoryList
    {
        public static string[] dogNicknames = CreateCategoryList("DogNames");              // 01
        public static string[] colors = CreateCategoryList("Colors");                      // 02
        public static string[] poisonPlants = CreateCategoryList("PoisonousPlants");       // 03
        public static string[] thingsToEat = CreateCategoryList("ThingsToEat");            // 04
        public static string[] thingsInMyRoom = CreateCategoryList("ThingsInMyRoom");      // 05
        public static string[] fabrictypes = CreateCategoryList("FabricTypes");            // 06
        public static string[] mangaList = CreateCategoryList("MangaList");                // 07
        public static string[] fonts = CreateCategoryList("Fonts");                        // 08
        public static string[] dndMonsters = CreateCategoryList("DNDmonsters");            // 09
        public static string[] periodicElements = CreateCategoryList("PeriodicElements");  // 10

        /// <summary>
        ///  Creates an array holding just the list of items of a category
        /// </summary>
        /// <param name="categoryName"> the name of the category </param>
        /// <param name="wordList"> the array it's accessing (should be an array made from words.txt file)</param>
        /// <returns></returns>
        public static string[] CreateCategoryList(string categoryName)
        {
            string filePath = "words.txt";
            StreamReader file = new StreamReader(filePath);                              // Creates the file path if it doesn't exist
            string wordsFromFile = file.ReadToEnd();
            file.Close();

            string[] wordList = wordsFromFile.Split("\r\n");
            string[] returnedList = new string[15];

            for (int i = 0; i < returnedList.Length; i++)
            {
                if (wordList.Contains(categoryName))
                {
                    int position = Array.IndexOf(wordList, categoryName) + 1;           // returns the index position of the first word in 'request' in wordList[]
                    returnedList[i] = wordList[position + i];
                }
                else
                {
                    Console.WriteLine("Incorrect categoryName");
                }
            }
            return returnedList;                                                        // Shouldn't happen if 'categoryName' is valid, Will return a blank array of 15 lines
        }
    }
}
