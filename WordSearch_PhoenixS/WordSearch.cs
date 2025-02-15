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
            char[] num1 = { '0', '1', '2' };
            char[] num2 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0'};
            int iterator = 1;
            //char[] row1 = OutPutCharRow('0', '1');
            //char[] row2 = OutPutCharRow('0', '2');
            //char[] row3 = OutPutCharRow('0', '3');
            //char[] row4 = OutPutCharRow('0', '4');
            //char[] row5 = OutPutCharRow('0', '5');
            //char[] row6 = OutPutCharRow('0', '6');
            //char[] row7 = OutPutCharRow('0', '7');
            //char[] row8 = OutPutCharRow('0', '8');
            //char[] row9 = OutPutCharRow('0', '9');
            //char[] row10 = OutPutCharRow('1', '0');
            //char[] row11 = OutPutCharRow('1', '1');
            //char[] row12 = OutPutCharRow('1', '2');
            //char[] row13 = OutPutCharRow('1', '3');
            //char[] row14 = OutPutCharRow('1', '4');
            //char[] row15 = OutPutCharRow('1', '5');
            //char[] row16 = OutPutCharRow('1', '6');
            //char[] row17 = OutPutCharRow('1', '7');
            //char[] row18 = OutPutCharRow('1', '8');
            //char[] row19 = OutPutCharRow('1', '9');
            //char[] row20 = OutPutCharRow('2', '0');


            Array[] defaultWordSearch = new Array[20];
            for (int i = 0; i <= defaultWordSearch.Length; i++)
            {
                if (i < 9)
                {                    
                    defaultWordSearch[i] = OutPutCharRow(num1[0], num2[iterator]);
                    iterator++;
                }
                else if (i >= 9 && i < 19)
                {
                    if (i == 9)
                    {
                        iterator = 0;
                    }

                    defaultWordSearch[i] = OutPutCharRow(num1[1], num2[iterator]);
                    iterator++;
                }
                else if (i == 19)
                {
                    defaultWordSearch[i] = OutPutCharRow(num1[2], num2[0]);
                }
            }

                //Array[] defaultWordSearch = { row1, row2, row3, row4, row5, row6, row7, row8,row9, row10,
                //                   row11, row12, row13, row14, row15, row16, row17, row18, row19, row20};
            
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

        static char[] OutPutCharRow(char num1, char num2)
        {
            char[] charRow = new char[62];

            Array.Fill(charRow, ' ');

            for (int i = 0; i < charRow.Length; i++)
            {
                if (i == 0)
                {
                    charRow[0] = num1;
                }
                else if (i == 1)
                {
                    charRow[1] = num2;
                }
                else if (i % 3 == 0)
                {
                    charRow[i] = RandomLetter();
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
        static char RandomLetter()
        {
            char[] alphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };
            Random random = new Random();
            int index = random.Next(0, alphabet.Length);
            return alphabet[index];
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
