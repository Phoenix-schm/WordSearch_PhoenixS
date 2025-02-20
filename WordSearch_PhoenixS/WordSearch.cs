using System;
using System.IO;
using System.Drawing;

namespace WordSearch_PhoenixS
{
    class WordSearch
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
                Console.WriteLine();
                Console.Write("Please input your choice of wordsearch: ");
                validInput = UserInput.InputCheck(validUserInputs);
                validInput = validInput.ToLower();

                PlayerChoice(validInput, ref isPlaying, validUserInputs);
                Console.WriteLine("Type 'Show List' to show the categories again.");
            }
        }

        /// <summary>
        /// Displays contents of a string array
        /// </summary>
        /// <param name="validInputsList"> The list of valid user inputs </param>
        static void DisplayValidUserInputs(string[] validInputsList)                  // displays each category/valid input and its associated number
        {
            for (int i = 1; i < validInputsList.Length; i++)
            {
                Console.WriteLine(i + ") " +  validInputsList[i - 1]);
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
                    CategoryWordSearchCreation(CategoryList.dogNicknames);
                    break;
                case "colors":                  case "2":
                    CategoryWordSearchCreation(CategoryList.colors);
                    break;
                case "poisonous flowers":       case "3":
                    CategoryWordSearchCreation(CategoryList.poisonPlants);
                    break;
                case "things in my room":       case "4":
                    CategoryWordSearchCreation(CategoryList.thingsInMyRoom);
                    break;
                case "things to eat":           case "5":
                    CategoryWordSearchCreation(CategoryList.thingsToEat);
                    break;
                case "fabric types":            case "6":
                    CategoryWordSearchCreation(CategoryList.fabrictypes);
                    break;
                case "manga names":             case "7":
                    CategoryWordSearchCreation(CategoryList.mangaList);
                    break;
                case "fonts":                   case "8":
                    CategoryWordSearchCreation(CategoryList.fonts);
                    break;
                case "dnd monsters":            case "9":
                    CategoryWordSearchCreation(CategoryList.dndMonsters);
                    break;
                case "periodic table elements":     case "10":
                    CategoryWordSearchCreation(CategoryList.periodicElements);
                    break;
                case "quit":                    case "11":
                    boolean = false;
                    break;
                case "show list":               case "12":
                    DisplayValidUserInputs(categoriesDisplay);
                    break;
                default:
                    Console.WriteLine("How the hell did you manage to get this response?");     // somehwow the input was valid but isn't any of the listed items.
                    break;                                                                      // Should occur if you updated the words.txt file but didn't edit this list.
            }
        }

        /// <summary>
        /// Creates a word search using the inputCategory
        /// </summary>
        /// <param name="inputCategory"> the category the user chose </param>
        static void CategoryWordSearchCreation(string[] inputCategory)
        {
            char[,] newWordSearch = DefaultWordSearch(' ');                                // Creates the default version of the word search first, filled with blanks
            string[] eightRandomWords = RandomWordsFromCategory(inputCategory);         // Choose eight random words from inputCategory
            
            for(int i = 0; i < eightRandomWords.Length; i++)                            // Passes in each random word
            {
                char[] randomWord = ConvertWordToCharArray(eightRandomWords[i]);
                newWordSearch = NewWordSearch(randomWord, newWordSearch, eightRandomWords); // Each time a word is passed in it creates a new word search
            }

            Console.WriteLine("Word Search Puzzle: ");
            DisplayWordSearch(newWordSearch);

            //char[,] verticalWordSearch = Vertical.RotateWordSearch(newWordSearch);
            //DisplayWordSearch(verticalWordSearch);


            Console.WriteLine();
            Console.WriteLine("Search for these words:");
            foreach (string word in eightRandomWords)
            {
                Console.WriteLine(word);
            }
            // change this function to output the eightRandomWords string[] later? For use in the actual solving of the word search
        }

        /// <summary>
        /// Displays the word search. Including numbered axises.
        /// </summary>
        /// <param name="wordSearch"> Word search to be displayed</param>
        public static void DisplayWordSearch(char[,] wordSearch)
        {
            string NumberedYaxis = string.Join(" ", NumberedAxisInWordSearch());
            Console.WriteLine("  " + NumberedYaxis);                                            // Displays the column numbers

            string[] NumberedXaxis = NumberedAxisInWordSearch();

            // Displays the wordsearch, currently colors are for help debugging
            for (int y_axis = 0; y_axis < wordSearch.GetLength(0); y_axis++)
            {
                Console.Write(NumberedXaxis[y_axis]);                                           // Displays the row number
                for (int x_axis = 0; x_axis < wordSearch.GetLength(1); x_axis++)
                {
                    if (Char.IsLetter(wordSearch[y_axis, x_axis]))                               // if there's a letter, turn it green (for debugging purposes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + wordSearch[y_axis, x_axis] + " ");
                    }
                    else                                                                        // else, fill the word search with random letters
                    {
                        Console.ResetColor();
                        Console.Write(" " + RandomLetter() + " ");
                    }
                    
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Used to display the numbered axis' of the word search
        /// Creates the numbered axis' 01 - 20 in a string array
        /// </summary>
        /// <returns> string array holding numbers from "01" 0 "20" </returns>
        static string[] NumberedAxisInWordSearch()
        {
            string[] NumberedAxis = {"01","02","03","04","05","06","07","08","09","10",
                                     "11","12","13","14","15","16","17","18","19","20"};
            return NumberedAxis;
        }

        /// <summary>
        /// Adds 'word' to the word search. Randomly chooses how the word will be displayed in the word search
        /// </summary>
        /// <param name="word"> Word to be added to the word search</param>
        /// <param name="currentWordSearchArray"> The current iteratioin of the word search</param>
        /// <param name="category"> The string array of the category the user chose</param>
        /// <returns></returns>
        static char[,] NewWordSearch(char[] word, char[,] currentWordSearchArray, string[] category)
        {
            int randomNum = SearchType.RandomNumber(0, 4);

            switch(randomNum)
            {
                case 0:  case 1:            // horizontal outputs
                    currentWordSearchArray = Horizontal.OutputWordInWordSearch(word, currentWordSearchArray, category, randomNum);
                    return currentWordSearchArray;
                case 2:  case 3:            // Vertical outpus
                    currentWordSearchArray = Vertical.OutputWordInWordSearch(word, currentWordSearchArray, category, randomNum);
                    return currentWordSearchArray;
                case 4:  case 5:        // '/' diagonal 
                    return currentWordSearchArray;                
                case 6:  case 7:        // '\' diagonal
                    return currentWordSearchArray;
                default:
                    return currentWordSearchArray;
            }
        }
        
        /// <summary>
        /// Creates the default word search filled with blank spaces
        /// </summary>
        /// <returns> A 20 by 20 2-dimensional character array </returns>
        static char[,] DefaultWordSearch(char fillWordSearch)
        {
            char[,] defaultWordSearch = new char[20, 20];

            for (int y_axis = 0; y_axis < defaultWordSearch.GetLength(0); y_axis++)
            {
                for (int x_axis = 0; x_axis < defaultWordSearch.GetLength(1); x_axis++)
                {
                    defaultWordSearch[y_axis, x_axis] = fillWordSearch;
                }
            }
            return defaultWordSearch;
        }

        /// <summary>
        /// Converts a string 'word' into a character array. Used for converting category words into character arrays
        /// </summary>
        /// <param name="word"> word to be converted in a character array</param>
        /// <returns> The character array of 'word'</returns>
        static char[] ConvertWordToCharArray(string word)
        {
            char[] charOfWord = word.ToCharArray();
            return charOfWord;
        }
        
        /// <summary>
        /// Creates a string[] of eight random words of user choice category
        /// </summary>
        /// <param name="categoryWordList"> The list of words that the eight words will be chosen from </param>
        /// <returns> Returns a string array of eight random words, no repeats </returns>
        static string[] RandomWordsFromCategory(string[] categoryWordList)
        {
            string[] randomWords = new string[8];

            // creates an int[] of random eight numbers
            int[] randomIntList = ReturnRandomNumberList(8, 15);

            for (int i = 0; i < 8; i++)
            {
                randomWords[i] = categoryWordList[randomIntList[i]].ToUpper();      // Add a random word from categoryWordList into randomWords[]
            }
            return randomWords;
        }

        /// <summary>
        /// Outputs a random letter from the alphabet (excludes X, Y, Z) uppercase
        /// </summary>
        /// <returns></returns>
        static char RandomLetter()
        {
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                                'R', 'S', 'T', 'U', 'V', 'W' };

            int index = SearchType.RandomNumber(0, alphabet.Length);
            return alphabet[index];
        }
        /// <summary>
        /// Creates a list of random numbers with no duplicates
        /// </summary>
        /// <param name="maxIndex"> The maximum amount of integers that can be held in the array </param>
        /// <param name="maxRandomNumber"> Array will be filled with numbers betwee 0 (inclusive) and maxRandomNumber (exclusive)</param>
        /// <returns></returns>
        public static int[] ReturnRandomNumberList(int maxIndex, int maxRandomNumber)
        {
            int index = 0;
            int[] randomIntList = new int[maxIndex];
            int useZeroOnce = 0;

            while (index < maxIndex)
            {
                int randomInt = SearchType.RandomNumber(0, maxRandomNumber);

                if (randomIntList.Contains(randomInt) && randomInt != 0)                        // because randomIntList[] already has 0, must allow 0 to pass through
                {
                    continue;
                }
                else
                {
                    if (randomInt == 0 && useZeroOnce == 0)                                     // insures 0 will occur at least once in the array
                    {
                        randomIntList[index++] = randomInt;
                        useZeroOnce++;
                    }
                    else if (randomInt != 0)
                    {
                        randomIntList[index++] = randomInt;
                    }
                }
            }
            return randomIntList;
        }

    }
}
