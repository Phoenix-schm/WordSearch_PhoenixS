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
            string[] dogNicknames = WordList.ReturnWordArray("DogNames", wordList);
            string[] colorsList = WordList.ReturnWordArray("Colors", wordList);
            string[] poisonFlowers = WordList.ReturnWordArray("PoisonousPlants", wordList);
            string[] thingsInRoom = WordList.ReturnWordArray("ThingsInMyRoom", wordList);
            string[] thingsToEat = WordList.ReturnWordArray("ThingsToEat", wordList);
            string[] fabricTypes = WordList.ReturnWordArray("FabricTypes", wordList);
            string[] mangaNames = WordList.ReturnWordArray("MangaList", wordList);
            string[] fonts = WordList.ReturnWordArray("Fonts", wordList);
            string[] dndMonsters = WordList.ReturnWordArray("DNDmonsters", wordList);
            string[] periodicElements = WordList.ReturnWordArray("PeriodicElements", wordList);

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
            string[] alphabet =
            {
                "A", "B", "C", "D", "E", "F", "G", "H", "I", "J", "K", "L", "M", "N", "O", "P", "Q",
                "R", "S", "T", "U", "V", "W", "X", "Y", "Z"
            };


            Console.WriteLine("Welcome to the Amazing Word Search");
            Console.WriteLine("You have ten categories to choose from:");
            DisplayCategories(categoryNames);
            Console.WriteLine("--------------------------");
            

            while(isPlaying)
            {
                stringInput = UserInput.InputCheck(categoryNames);
                string input = stringInput.ToLower();

                PlayerChoice(input, ref isPlaying, dogNicknames);
            }
        }

        /// <summary>
        /// Holds all the choices the player can make
        /// </summary>
        /// <param name="input"> player input</param>
        static void PlayerChoice(string input, ref bool boolean, string[] category)
        {
            switch (input)
            {
                case "dog nicknames":           case "1":
                    WordSearchDefault();
                    break;
                case "colors":                  case "2":
                    WordSearchDefault();
                    break;
                case "poisonous flowers":       case "3":
                    break;
                case "things in my room":       case "4":
                    WordSearchDefault();
                    break;
                case "things to eat":           case "5":
                    WordSearchDefault();
                    break;
                case "fabric types":            case "6":
                    WordSearchDefault();
                    break;
                case "manga names":             case "7":
                    WordSearchDefault();
                    break;
                case "fonts":                   case "8":
                    WordSearchDefault();
                    break;
                case "dnd monsters":            case "9":
                    WordSearchDefault();
                    break;
                case "periodic table elements":     case "10":
                    WordSearchDefault();
                    break;
                case "quit":                    case "11":
                    boolean = false;
                    break;
            }
        }

        static void DisplayCategories(string[] categories)
        {
            int num = 1;
            foreach (string category in categories)
            {
                Console.WriteLine(num + ") " + category);
                num++;
            }
            Console.WriteLine(num + ") Quit");
        }

        /// <summary>
        /// Creates the default version of the word search, filled with "."
        /// </summary>
        static Array[] WordSearchDefault()
        {

            OutputRow("01", out char[] row1);
            OutputRow("02", out char[] row2);
            OutputRow("03", out char[] row3);
            OutputRow("04", out char[] row4);
            OutputRow("05", out char[] row5);
            OutputRow("06", out char[] row6);
            OutputRow("07", out char[] row7);
            OutputRow("08", out char[] row8);
            OutputRow("09", out char[] row9);
            OutputRow("10", out char[] row10);
            OutputRow("11", out char[] row11);
            OutputRow("12", out char[] row12);
            OutputRow("13", out char[] row13);
            OutputRow("14", out char[] row14);
            OutputRow("15", out char[] row15);
            OutputRow("16", out char[] row16);
            OutputRow("17", out char[] row17);
            OutputRow("18", out char[] row18);
            OutputRow("19", out char[] row19);
            OutputRow("20", out char[] row20);


            Array[] defaultWordSearch = { row1, row2, row3, row4, row5, row6, row7, row8,row9, row10,
                                   row11, row12, row13, row14, row15, row16, row17, row18, row19, row20};
            
            Console.WriteLine("Word Search Puzzle: ");
            string row0 = "  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20";
            char[] charRow0 = row0.ToCharArray();

            Console.WriteLine(charRow0);

            foreach(char[] arrayRow in defaultWordSearch)
            {
                foreach(char word in arrayRow)
                {
                    Console.Write(word);
                }
                Console.WriteLine();
            }
            return defaultWordSearch;
        }

        /// <summary>
        /// Creates a new array filled with '.' and [0] to be rowNum
        /// </summary>
        /// <param name="rowNum"> the label for each row </param>
        /// <param name="row"> the array holding </param>
        /// <returns> a string array </returns>
        static char[] OutputRow(string rowNum, out char[] charRow)
        {
            string filler = ".";
            int charCount = 0;

            string[] stringRow = new string[21];                      // Create a string[] that's filled with filler and rowNum
            charRow = new char[62];

            Array.Fill(stringRow, " " + filler + " ");
            stringRow[0] = rowNum;

            // fills charRow[] with all elements of stringRow[]
            for (int i = 0; i < stringRow.Length; i++)               //going through each element of row
            {
                char[] word = stringRow[i].ToCharArray();
                for (int j = 0; j < word.Length; j++)        //going through each letter[j] of each word of row
                {
                    charRow[charCount] = word[j];
                    charCount++;
                }
            }
            return charRow;
        }
        
        static string[] RandomWordsArray(string[] wordList) // Adds 8 random words from 'wordList' into randomWords[]
        {
            Random rand = new Random();
            string[] randomWords = new string[8];

            for (int i = 0; i < 8; i++)
            {
                randomWords.Append(wordList[rand.Next(0, 15)]);
            }
            return randomWords;
        }
        static char[] ConvertWordToCharArray(string word)
        {
            char[] charOfWord = word.ToCharArray();
            return charOfWord;
        }
        static void OutputWordSearch(char[] row, string[] category)
        {
            string[] randomWords = RandomWordsArray(category);          // creates a string[] of 8 words from a category
            

            foreach (char letter in row)
            {
                if (letter == '.')
                {
                    for(int i = 0; i < randomWords.Length; i++)
                    {
                        char[] word = ConvertWordToCharArray(randomWords[i]);
                        for (int j = 0; j < word.Length; j++)
                        {
                            row[letter] = word[j];

                        }

                    }

                }
            }
            Console.WriteLine(row);
        }

        #region Misc

        #endregion
    }
    internal class UserInput
    {
        /// <summary>
        /// Checks user input for string and int's for valid inputs
        /// </summary>
        /// <param name="categories"> the list of categories the player can choose from</param>
        /// <returns></returns>
        public static string InputCheck(string[] categories)
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
                    Console.WriteLine("Invalid input.");
                }
            }
            return "Invalid input. Try again";

        }
    }
    internal class WordList
    {
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
