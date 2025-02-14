using System;
using System.ComponentModel.Design;
using System.Data;
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
            string? stringInput;
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
            string[] categoryNames =
            {
                "Dog Nicknames", "Colors", "Poisonous Flowers", "Things In My Room", "Things To Eat",
                "Fabric Types", "Managa Names", "Fonts", "DND Monsters", "Periodic Table Elements"
            };


            WriteLine("Welcome to the Amazing Word Search");
            WriteLine("You have ten categories to choose from:");
            DisplayCategories(categoryNames);
            WriteLine("--------------------------");
            

            while(isPlaying)
            {
                stringInput = InputCheck(categoryNames);
                string input = stringInput.ToLower();
                
                if (input == categoryNames[0].ToLower() || input == 1.ToString())
                {
                    
                    WordSearchDefault("01", out string row1);
                }
                else if (input == "quit" || input == 11.ToString())
                {
                    isPlaying = false;
                }

            }


        }

        static void WordSearchDefault(string rowNum, out string row)
        {
            string filler = ".";
            string rowOutput = "";
            string[] row1 = new string[20];
            Array.Fill(row1, " " + filler + " ");

            Array[] wordSearch = { row1 };
            WriteLine("Word Search Puzzle: ");
            WriteLine("  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20");

            foreach(string word in row1)
            {
                rowOutput += word;
            }
            row = rowNum + rowOutput;
            Console.Write(row);
            //Write(num++ + row1.ToString());
           
        }
        static void WriteLine(string sentence)
        {
            Console.WriteLine(sentence);
        }
        static void DisplayCategories(string[] categories)
        {
            int num = 1;
            foreach (string category in categories)
            {
                WriteLine(num + ") " + category);
                num++;
            }
            WriteLine(num + ") Quit");
        }
        static string InputCheck(string[] categories)
        {
            bool invalidInput = true;
            while (invalidInput)
            {
                string? input = Console.ReadLine();

                if (input != null)
                {
                    foreach (string category in categories)
                    {
                        if (input.ToLower() == category.ToLower())
                        {
                            invalidInput = false;
                            return input;
                        }
                    }
                    for (int i = 0; i <= categories.Count() + 1; i++)
                    {
                        if (input == i.ToString())
                        {
                            invalidInput = true;
                            return input;
                        }
                    }
                }
                else
                {
                    invalidInput = true;
                    WriteLine("Invalid input.");
                }
            }
            return "Invalid input. Try again";

        }
    }
}
