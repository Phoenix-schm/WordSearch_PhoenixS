using System;
using System.IO;

namespace WordSearch_PhoenixS
{
    internal class WordsList
    {
        //static void Main(string[] args)
        //{
        //    //StreamReader wordsFile = new StreamReader("words.txt");
        //    //string[] wordsList = File.ReadAllLines("words.txt");
        //    //wordsFile.Close();
        //}

        /// <summary>
        ///  Creates an array holding just the list of items for each word search category
        /// </summary>
        /// <param name="request"> the name of the category </param>
        /// <param name="list"> the array it's accessing (should be an array made from words.txt file)</param>
        /// <returns></returns>
        public static string[] ReturnWordArray(string request, string[] list)
        {
            string[] returnedList = new string[15];

            if (list.Contains(request))
            {
                int position = Array.IndexOf(list, request);        // returns the index position of 'request' in list[]
                int j = 0;
                for (int i = position + 1; i <= position + 15; i++)
                {
                    returnedList[j] = list[i].ToString();           // fills in each element of 'returnedList' with the corresponding list[]
                    j++;
                }
                return returnedList;
            }
            return returnedList;                            // will return a blank array of 15 lines
        }
    }
}
