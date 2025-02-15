using System;
using System.IO;
using System.Net.Quic;

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

                PlayerChoice(input, ref isPlaying);
            }
        }

        /// <summary>
        /// Holds all the choices the player can make
        /// </summary>
        /// <param name="input"> player input</param>
        static void PlayerChoice(string input, ref bool boolean)
        {
            if (input == "quit" || input == "11")
            {
                boolean = false;
            }
            else
            {
                WordSearchDefault();
            }
            //switch (input)
            //{
            //    case "dog nicknames":       case "1":
            //        WordSearchDefault();
            //        break;
            //    case "colors":              case "2":
            //        WordSearchDefault();
            //        break;
            //    case "poisonous flowers":   case "3":
            //        break;
            //    case "things in my room":   case "4":
            //        WordSearchDefault();
            //        break;
            //    case "things to eat":       case "5":
            //        WordSearchDefault();
            //        break;
            //    case "fabric types":        case "6":
            //        WordSearchDefault();
            //        break;
            //    case "manga names":         case "7":
            //        WordSearchDefault();
            //        break;
            //    case "fonts":               case "8":
            //        WordSearchDefault();
            //        break;
            //    case "dnd monsters":        case "9":
            //        WordSearchDefault();
            //        break;
            //    case "periodic table elements": case "10":
            //        WordSearchDefault();
            //        break;
            //    case "quit":                case "11":
            //        boolean = false;
            //        break;
            //}
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

        /// <summary>
        /// Creates the default version of the word search, filled with "."
        /// </summary>
        static void WordSearchDefault()
        {

            OutputRow("01", out string[] row1);
            OutputRow("02", out string[] row2);
            OutputRow("03", out string[] row3);
            OutputRow("04", out string[] row4);
            OutputRow("05", out string[] row5);
            OutputRow("06", out string[] row6);
            OutputRow("07", out string[] row7);
            OutputRow("08", out string[] row8);
            OutputRow("09", out string[] row9);
            OutputRow("10", out string[] row10);
            OutputRow("11", out string[] row11);
            OutputRow("12", out string[] row12);
            OutputRow("13", out string[] row13);
            OutputRow("14", out string[] row14);
            OutputRow("15", out string[] row15);
            OutputRow("16", out string[] row16);
            OutputRow("17", out string[] row17);
            OutputRow("18", out string[] row18);
            OutputRow("19", out string[] row19);
            OutputRow("20", out string[] row20);


            Array[] wordSearch = { row1, row2, row3, row4, row5, row6, row7, row8,row9, row10,
                                   row11, row12, row13, row14, row15, row16, row17, row18, row19, row20};
            
            WriteLine("Word Search Puzzle: ");
            WriteLine("  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20");

            foreach(Array row in wordSearch)
            {
                foreach(string word in row)
                {
                    Console.Write(word);
                }
                Console.WriteLine();
            }
           
        }

        /// <summary>
        /// Creates a new array filled with '.' and [0] to be rowNum
        /// </summary>
        /// <param name="rowNum"> the label for each row </param>
        /// <param name="row"> the array holding </param>
        /// <returns> a string array </returns>
        static string[] OutputRow(string rowNum, out string[] row)
        {
            string filler = ".";

            row = new string[21];
            Array.Fill(row, " " + filler + " ");

            row[0] = rowNum;
            return row;
        }

        /// <summary>
        /// Checks user input for string and int's for valid inputs
        /// </summary>
        /// <param name="categories"> the list of categories the player can choose from</param>
        /// <returns></returns>
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
#region Misc
        static void WriteLine(string sentence)
        {
            Console.WriteLine(sentence);
        }
#endregion
    }
}
