using System;
using System.IO;
using System.Drawing;

namespace WordSearch_PhoenixS
{
    internal class WordSearch
    {
        static void Main(string[] args)
        {
            // variable declarations
            bool isPlaying = true;
            string? validInput;

            string[] validUserInputs =
            {
                "Dog Nicknames", "Colors", "Poisonous Flowers", "Things In My Room", "Things To Eat",
                "Fabric Types", "Managa Names", "Fonts", "DND Monsters", "Periodic Table Elements", 
                "Quit", "Show List"
            };

            Console.WriteLine("Welcome to the Amazing Word Search");
            Console.WriteLine("You have ten categories to choose from:");
            DisplayValidUserInputs(validUserInputs);
            Console.WriteLine("--------------------------");

            while(isPlaying)
            {
                Console.Write("Please input your choice of wordsearch: ");
                validInput = UserInput.InputCheck(validUserInputs);
                validInput = validInput.ToLower();

                PlayerChoice(validInput, ref isPlaying, validUserInputs);
                //Console.WriteLine("Type 'Show List' to show the categories again.");
            }
        }

        static void DisplayValidUserInputs(string[] categories)                  // displays each category/valid input and its associated number
        {
            for (int i = 1; i < categories.Length; i++)
            {
                Console.WriteLine(i + ") " +  categories[i - 1]);
            }
            Console.WriteLine();
        }

        /// <summary>
        /// Holds all the choices the player can make
        /// </summary>
        /// <param name="input"> player input</param>
        static void PlayerChoice(string input, ref bool boolean, string[] categoriesDisplay)
        {
            switch (input)
            {
                case "dog nicknames":           case "1":
                    CategoryWordSearchCreation(WordList.dogNicknames);
                    break;
                case "colors":                  case "2":
                    CategoryWordSearchCreation(WordList.colors);
                    break;
                case "poisonous flowers":       case "3":
                    CategoryWordSearchCreation(WordList.poisonPlants);
                    break;
                case "things in my room":       case "4":
                    CategoryWordSearchCreation(WordList.thingsInMyRoom);
                    break;
                case "things to eat":           case "5":
                    CategoryWordSearchCreation(WordList.thingsToEat);
                    break;
                case "fabric types":            case "6":
                    CategoryWordSearchCreation(WordList.fabrictypes);
                    break;
                case "manga names":             case "7":
                    CategoryWordSearchCreation(WordList.mangaList);
                    break;
                case "fonts":                   case "8":
                    CategoryWordSearchCreation(WordList.fonts);
                    break;
                case "dnd monsters":            case "9":
                    CategoryWordSearchCreation(WordList.dndMonsters);
                    break;
                case "periodic table elements":     case "10":
                    CategoryWordSearchCreation(WordList.periodicElements);
                    break;
                case "quit":                    case "11":
                    boolean = false;
                    break;
                case "show list":               case "12":
                    DisplayValidUserInputs(categoriesDisplay);
                    break;

            }
        }

        /// <summary>
        /// Creates a word search using the inputCategory
        /// </summary>
        /// <param name="inputCategory"> the category the user chose </param>
        static void CategoryWordSearchCreation(string[] inputCategory)
        {
            Array[] newWordSearch = DefaultWordSearchArray();                               // Creates a default version of the word search
            string[] eightRandomWords = RandomWordsFromCategoryArray(inputCategory);        // choose eight random words from inputCategory
            
            for(int i = 0; i < eightRandomWords.Length; i++)                                // passes in each random word
            {
                char[] randomWord = ConvertWordToCharArray(eightRandomWords[i]);
                newWordSearch = NewWordSearch(randomWord, newWordSearch, eightRandomWords); // each time a word is passed in it creates a new word search
            }

            string row0 = "  01 02 03 04 05 06 07 08 09 10 11 12 13 14 15 16 17 18 19 20";
            char[] charRow0 = row0.ToCharArray();

            Console.WriteLine("Word Search Puzzle: ");
            Console.WriteLine(charRow0);

            // outputs the whole word search
            foreach (char[] row in newWordSearch)
            {
                foreach (char letter in row)
                {
                    if (Char.IsLower(letter))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(letter);
                    }
                    else
                    {
                        Console.ResetColor();
                        Console.Write(letter);
                    }
                }
                Console.WriteLine();
            }

            Console.WriteLine();
            //Console.WriteLine("Search for these words:");
            foreach(string word in eightRandomWords)
            {
                Console.WriteLine(word);
            }
        }

        static Array[] NewWordSearch(char[] word, Array[] currentWordSearchArray, string[] category)
        {
            int randomNum = ValueReturn.RandomNumber(0, 2);

            switch(0)
            {
                case 0:
                    currentWordSearchArray = Horizontal.OutputWord_InOrder(word, currentWordSearchArray, category);
                    return currentWordSearchArray;
                case 1:         // left or right
                    currentWordSearchArray = Horizontal.OutputWord_Reverse(word, currentWordSearchArray, category);
                    return currentWordSearchArray;
                case 2:  case 3:        // up or down
                    return currentWordSearchArray;
                case 4:  case 5:        // '/' diagonal 
                    return currentWordSearchArray;                
                case 6:  case 7:        // '\' diagonal
                    return currentWordSearchArray;
                default:
                    return currentWordSearchArray;
            }
        }

        public static Array[] DefaultWordSearchArray()         //Outputs a wordSearch[] array with each index being a char[], numbered
        {
            // variables used only for numbering of each row in wordSearch
            char[] num1 = { '0', '1', '2' };
            char[] num2 = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9', '0' };
            int iterator = 1;
            //

            Array[] wordSearch = new Array[20];
            for (int i = 0; i <= wordSearch.Length; i++)
            {
                if (i < 9)                                          // from row 0 to 9
                {
                    wordSearch[i] = DefaultWordSearchRow(num1[0], num2[iterator]);
                    iterator++;
                }
                else if (i >= 9 && i < 19)                          // from row 10 to 19
                {
                    if (i == 9)
                    {
                        iterator = 0;
                    }
                    wordSearch[i] = DefaultWordSearchRow(num1[1], num2[iterator]);
                    iterator++;
                }
                else if (i == 19)                                   // row 20
                {
                    wordSearch[i] = DefaultWordSearchRow(num1[2], num2[0]);
                }
            }
            return wordSearch;
        }
        static char[] DefaultWordSearchRow(char num1, char num2)  // Outputs a row, numbered and filled with random letters
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

        static char[] ConvertWordToCharArray(string word)
        {
            //converts string 'word' into a char[]
            char[] charOfWord = word.ToCharArray();
            return charOfWord;
        }
        
        /// <summary>
        /// Creates a string[] of eight random words of user choice category
        /// </summary>
        /// <param name="categoryWordList"> The list of words that the eight words will be chosen from </param>
        /// <returns></returns>
        public static string[] RandomWordsFromCategoryArray(string[] categoryWordList)
        {
            string[] randomWords = new string[8];
            int[] randomIntList = new int[8];
            int index = 0;

            while (index < 8)                               // while there is still room in randomIntList
            {
                int randomInt = ValueReturn.RandomNumber(0, 15);        // Create a random number 0 - 14
                if (randomIntList.Contains(randomInt))      // if randomIntList already has that number
                {
                    continue;
                }
                else
                {
                    randomIntList[index++] = randomInt;     // add that number to randomIntList
                }
            }

            for (int i = 0; i < 8; i++)
            {
                randomWords[i] = categoryWordList[randomIntList[i]].ToLower();      // Add a random word from categoryWordList into randomWords[]
            }
            return randomWords;
        }

        static char RandomLetter()
        {
            // Outputs a random letter from alphabet[]
            char[] alphabet =
            {
                'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                'R', 'S', 'T', 'U', 'V', 'W', 'X', 'Y', 'Z'
            };

            int index = ValueReturn.RandomNumber(0, alphabet.Length);
            return alphabet[index];
        }

    }
    internal class UserInput
    {
        /// <summary>
        /// Checks user input for string and int's for valid inputs
        /// </summary>
        /// <param name="categoryList"> the list of categories the player can choose from</param>
        /// <returns></returns>
        public static string InputCheck(string[] categoryList)
        {
            bool invalidInput = true;

            while (invalidInput)
            {
                string? input = Console.ReadLine();

                if (input != null)
                {
                    foreach (string category in categoryList)
                    {
                        if (input.ToLower() == category.ToLower())
                        {
                            return input;
                        }
                    }
                    for (int i = 0; i <= categoryList.Length + 1; i++)           
                    {
                        if (input == i.ToString())
                        {
                            return input;
                        }
                    }
                }
                else
                {
                    invalidInput = true;
                    Console.WriteLine("Invalid input. Try again.");
                }
            }
            return "Invalid input. Try again";

        }
    }
    internal class WordList
    {
        public static string[] dogNicknames = ReturnCategoryArray("DogNames");              // 01
        public static string[] colors = ReturnCategoryArray("Colors");                      // 02
        public static string[] poisonPlants = ReturnCategoryArray("PoisonousPlants");       // 03
        public static string[] thingsToEat = ReturnCategoryArray("ThingsToEat");            // 04
        public static string[] thingsInMyRoom = ReturnCategoryArray("ThingsInMyRoom");      // 05
        public static string[] fabrictypes = ReturnCategoryArray("FabricTypes");            // 06
        public static string[] mangaList = ReturnCategoryArray("MangaList");                // 07
        public static string[] fonts = ReturnCategoryArray("Fonts");                        // 08
        public static string[] dndMonsters = ReturnCategoryArray("DNDmonsters");            // 09
        public static string[] periodicElements = ReturnCategoryArray("PeriodicElements");  // 10

        /// <summary>
        ///  Creates an array holding just the list of items for each word search category
        /// </summary>
        /// <param name="request"> the name of the category </param>
        /// <param name="wordList"> the array it's accessing (should be an array made from words.txt file)</param>
        /// <returns></returns>
        public static string[] ReturnCategoryArray(string request)
        {
            string filePath = "words.txt";
            string[] wordList = File.ReadAllLines(filePath);
            string[] returnedList = new string[15];

            if (wordList.Contains(request))
            {
                int position = Array.IndexOf(wordList, request);        // returns the index position of 'request' in wordList[]
                int j = 0;
                for (int i = position + 1; i <= position + 15; i++)
                {
                    returnedList[j] = wordList[i].ToString();           // fills in each element of 'returnedList' with the corresponding word from wordList[]
                    j++;
                }
                return returnedList;
            }
            return returnedList;                            // Shouldn't happen if 'request' is correct, Will return a blank array of 15 lines
        }
    }

}
