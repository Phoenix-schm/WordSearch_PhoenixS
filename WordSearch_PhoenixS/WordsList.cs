using System;
using System.IO;

namespace WordSearch_PhoenixS
{
    internal class WordsList
    {
        static void Main(string[] args)
        {
            using StreamReader wordsFile = new StreamReader("words.txt");
            string[] wordsList = File.ReadAllLines("words.txt");
            //words.Close();
        }
    }
}
