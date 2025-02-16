using System;
using System.ComponentModel.Design;
using System.IO;
using System.Net.Quic;

namespace WordSearch_PhoenixS
{
    internal class WordSearch
    {
        static void Main(string[] args)
        {
            // variable declarations
            bool isPlaying = true;
            string? stringInput;

            // each word search category
            string[] dogNicknames = WordList.ReturnWordArray("DogNames");
            string[] colorsList = WordList.ReturnWordArray("Colors");
            string[] poisonFlowers = WordList.ReturnWordArray("PoisonousPlants");
            string[] thingsInRoom = WordList.ReturnWordArray("ThingsInMyRoom");
            string[] thingsToEat = WordList.ReturnWordArray("ThingsToEat");
            string[] fabricTypes = WordList.ReturnWordArray("FabricTypes");
            string[] mangaNames = WordList.ReturnWordArray("MangaList");
            string[] fonts = WordList.ReturnWordArray("Fonts");
            string[] dndMonsters = WordList.ReturnWordArray("DNDmonsters");
            string[] periodicElements = WordList.ReturnWordArray("PeriodicElements");

            // array of the arrays
            Array[] wordSearchArrays =
            {
                dogNicknames, colorsList, poisonFlowers, thingsInRoom, thingsToEat,
                fabricTypes, mangaNames, fonts, dndMonsters, periodicElements
            };

            string[] categoryNamesDisplay =
            {
                "Dog Nicknames", "Colors", "Poisonous Flowers", "Things In My Room", "Things To Eat",
                "Fabric Types", "Managa Names", "Fonts", "DND Monsters", "Periodic Table Elements"
            };

            Console.WriteLine("Welcome to the Amazing Word Search");
            Console.WriteLine("You have ten categories to choose from:");
            DisplayCategories(categoryNamesDisplay);
            Console.WriteLine("--------------------------");
            

            while(isPlaying)
            {
                stringInput = UserInput.InputCheck(categoryNamesDisplay);
                string input = stringInput.ToLower();

                PlayerChoice(input, ref isPlaying, wordSearchArrays);
            }
        }

        /// <summary>
        /// Holds all the choices the player can make
        /// </summary>
        /// <param name="input"> player input</param>
        static void PlayerChoice(string input, ref bool boolean, Array[] categoryList)
        {
            switch (input)
            {
                case "dog nicknames":           case "1":
                    WordSearchDefault((string[])categoryList[0]);
                    break;
                case "colors":                  case "2":
                    //WordSearchDefault((string[])categoryList[1]);
                    break;
                case "poisonous flowers":       case "3":
                    //WordSearchDefault((string[])categoryList[2]);
                    break;
                case "things in my room":       case "4":
                    //WordSearchDefault((string[])categoryList[3]);
                    break;
                case "things to eat":           case "5":
                    //WordSearchDefault((string[])categoryList[4]);
                    break;
                case "fabric types":            case "6":
                    //WordSearchDefault((string[])categoryList[5]);
                    break;
                case "manga names":             case "7":
                    //WordSearchDefault((string[])categoryList[6]);
                    break;
                case "fonts":                   case "8":
                    //WordSearchDefault((string[])categoryList[7]);
                    break;
                case "dnd monsters":            case "9":
                    //WordSearchDefault((string[])categoryList[8]);
                    break;
                case "periodic table elements":     case "10":
                    //WordSearchDefault((string[])categoryList[9]);
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

        static void WordSearchDefault(string[] categories)
        {
            Array[] array = WordSearchCreation();
            int useOnce = 0;

            for(int i = 0; i < categories.Length; i++)                          // for each word in catergories          
            {
                char[] word = ConvertWordToCharArray(categories[i]);

                foreach (char[] row in array)                                   // going through each row in array
                {
                    int k = 0;
                    while (k < word.Length)            
                    {
                        for (int j = 0; j < row.Length; j++)                        // going through each letter in row
                        {
                            if (j == 0 || j == 1 || j % 3 != 0)                     // skip these parts
                            {
                                continue;
                            }
                            else if (k < word.Length)
                            {
                                row[j] = word[k];
                                k++;
                            }
                        }

                    }
                }
            }

            string row0 = "  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20";
            char[] charRow0 = row0.ToCharArray();

            Console.WriteLine("Word Search Puzzle: ");
            Console.WriteLine(charRow0);


            foreach (char[] row in array)
            {
                foreach (char word in row)
                {
                    Console.Write(word);
                }
                Console.WriteLine();
            }
        }
        /*
        /// <summary>
        /// Creates the default version of the word search, filled with "."
        /// </summary>
        static Array[] WordSearchDefault(string[] inputCategory)
        {
            string[] categoryArray = RandomWordsArray(inputCategory);           // creates the random eigth letters that'll be in the word search
            Array[] defaultWordSearch = WordSearchCreation(categoryArray);    
            Random random = new Random();
            int x = 0;


            Console.WriteLine("Word Search Puzzle: ");
            string row0 = "  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20";
            char[] charRow0 = row0.ToCharArray();

            Console.WriteLine(charRow0);

            while (x < categoryArray.Length)                //going through each word in categoryArray
            {
                char[] word = ConvertWordToCharArray(categoryArray[x]);     //each letter in a word of categoryArray
                int choice = 2;   //ChooseSearchType();
                int useOnce = 0;
                int fullLength = word.Length;
                int j = 0;
                int h = 0;

                foreach (char[] row in defaultWordSearch)
                {
                    int canFit = random.Next(3, row.Length - word.Length);
                    if (choice == 0 || choice == 1)         // up or down
                    {

                    }
                    else if (choice == 2 || choice == 3)   // left or right
                    {
                        while(j <= row.Length)                         // while there are still letters in row
                        {
                            while (h < word.Length)
                            {
                                if (j == 0 || j == 1 && j % 3 != 0)         // skip these parts of row
                                {
                                    continue;
                                }
                                else
                                {
                                    for (int i = canFit; i < row.Length; i++)
                                    {
                                        row[i] = word[h];
                                        h++;
                                    }
                                }

                            }
                            j++;
                        }
                    }
                    else if (choice == 4 || choice == 5)
                    {
                    
                    }
                    else if (choice == 6 || choice == 7)
                    {

                    }
                }
                x++;
            }

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
        */
        static int ChooseSearchType()
        {
            Random random = new Random();
            int randomNum = random.Next(0, 8);

            return randomNum;       // will return a number choosing how the word will be hidden in the word search
        }

        static Array[] WordSearchCreation()         //Outputs a wordSearch[] array with each index being a char[], numbered
        {
            char[] num1 = { '0', '1', '2' };
            char[] num2 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int iterator = 1;

            Array[] wordSearch = new Array[20];
            for (int i = 0; i <= wordSearch.Length; i++)
            {
                if (i < 9)                                          // from row 0 to 9
                {
                    wordSearch[i] = OutputWordSearchRow(num1[0], num2[iterator]);
                    iterator++;
                }
                else if (i >= 9 && i < 19)                          // from row 10 to 19
                {
                    if (i == 9)
                    {
                        iterator = 0;
                    }
                    wordSearch[i] = OutputWordSearchRow(num1[1], num2[iterator]);
                    iterator++;
                }
                else if (i == 19)                                   // row 20
                {
                    wordSearch[i] = OutputWordSearchRow(num1[2], num2[0]);
                }
            }
            return wordSearch;
        }

        static char[] OutputWordSearchRow(char num1, char num2)
        {
            char[] charRow = new char[62];                  // creates the row


            Array.Fill(charRow, ' ');                       // fills it with blank spaces

            for (int i = 0; i < charRow.Length; i++)        // fills each index of charRow[], first two indexes are numbers
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
        
        static string[] RandomWordsArray(string[] wordList) // Creates 8 random words from 'wordList' into randomWords[]
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
            //converts 'word' into a char[]
            char[] charOfWord = word.ToCharArray();
            return charOfWord;
        }
        static char RandomLetter()
        {
            // Outputs a random letter from alphabet[]
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
        /// <param name="wordList"> the array it's accessing (should be an array made from words.txt file)</param>
        /// <returns></returns>
        public static string[] ReturnWordArray(string request)
        {
            string filePath = "words.txt";
            string[] wordList = File.ReadAllLines(filePath);
            string[] returnedList = new string[15];

            if (wordList.Contains(request))
            {
                int position = Array.IndexOf(wordList, request);        // returns the index position of 'request' in list[]
                int j = 0;
                for (int i = position + 1; i <= position + 15; i++)
                {
                    returnedList[j] = wordList[i].ToString();           // fills in each element of 'returnedList' with the corresponding list[]
                    j++;
                }
                return returnedList;
            }
            return returnedList;                            // will return a blank array of 15 lines
        }
    }
}
