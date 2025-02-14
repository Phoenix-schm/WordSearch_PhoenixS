using System;
using System.IO;

namespace WordSearch_PhoenixS
{
    internal class WordSearch
    {
        static void Main(string[] args)
        {
            // variable declarations
            string filePath = "words.txt";
            string[] wordList = File.ReadAllLines(filePath);

            bool isPlaying = true;

            // each word search category
            string[] dogNicknames = WordsList.ReturnWordArray("DogNames", wordList);
            string[] colorsList = WordsList.ReturnWordArray("Colors", wordList);
            string[] poisonFlowers = WordsList.ReturnWordArray("PoisonousPlants", wordList);
            string[] thingsInRoom = WordsList.ReturnWordArray("ThingsInMyRoom", wordList);
            string[] thingsToEat = WordsList.ReturnWordArray("ThingsToEat", wordList);
            string[] fabricTypes = WordsList.ReturnWordArray("FabricTypes", wordList);
            string[] mangaNames = WordsList.ReturnWordArray("MangaList", wordList);
            string[] fonts = WordsList.ReturnWordArray("Fonts", wordList);
            string[] dndMonsters = WordsList.ReturnWordArray("DNDmonsters", wordList);
            string[] periodicElements = WordsList.ReturnWordArray("PeriodicElements", wordList);

            // array of the arrays
            Array[] wordSearchArrays =
            {
                dogNicknames, colorsList, poisonFlowers, thingsInRoom, thingsToEat, 
                fabricTypes, mangaNames, fonts, dndMonsters, periodicElements
            };


            Write("Welcome to the Amazing Word Search");
            Write("You have ten categories to choose from");

            while(isPlaying)
            {

            }


        }

        static void WordSearchDefault()
        {
            int num = 00;

            Write("Word Search Puzzle: ");
            Write("  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20");
           
        }
        static void Write(string sentence)
        {
            Console.WriteLine(sentence);
        }

    }
}
