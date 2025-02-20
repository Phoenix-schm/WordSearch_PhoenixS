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
                    Console.WriteLine("How the hell did you manage to get this response?");     // somehwo the input was valid but isn't any of the listed items.
                    break;                                                                      // Should occur if you updated the words.txt file but didn't edit this list.
            }
        }

        /// <summary>
        /// Creates a word search using the inputCategory
        /// </summary>
        /// <param name="inputCategory"> the category the user chose </param>
        static void CategoryWordSearchCreation(string[] inputCategory)
        {
            char[,] newWordSearch = DefaultWordSearch();                                // Creates the default version of the word search first, filled with blanks
            string[] eightRandomWords = RandomWordsFromCategory(inputCategory);         // Choose eight random words from inputCategory
            
            for(int i = 0; i < eightRandomWords.Length; i++)                            // Passes in each random word
            {
                char[] randomWord = ConvertWordToCharArray(eightRandomWords[i]);
                newWordSearch = NewWordSearch(randomWord, newWordSearch, eightRandomWords); // Each time a word is passed in it creates a new word search
            }

            DisplayWordSearch(newWordSearch);

            Console.WriteLine();
            Console.WriteLine("Search for these words:");
            foreach (string word in eightRandomWords)
            {
                Console.WriteLine(word);
            }
            // change this function to output the eightRandomWords string[] later? For use in the actual solving of the word search
        }

        static void DisplayWordSearch(char[,] wordSearch)
        {
            Console.WriteLine("Word Search Puzzle: ");

            string NumberedYaxis = string.Join(" ", NumberedAxisInWordSearch());
            Console.WriteLine("  " + NumberedYaxis);                                            // Displays the column numbers

            string[] NumberedxAxis = NumberedAxisInWordSearch();

            // Displays the wordsearch, currently colors are for help debugging
            for (int y_axis = 0; y_axis < wordSearch.GetLength(0); y_axis++)
            {
                Console.Write(NumberedxAxis[y_axis]);                                           // Displays the row number
                for (int x_axis = 0; x_axis < wordSearch.GetLength(1); x_axis++)
                {
                    if (Char.IsLower(wordSearch[y_axis, x_axis]))                               // if there's a letter, turn it green (for debugging purposes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + Char.ToUpper(wordSearch[y_axis, x_axis]) + " ");
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
        static string[] NumberedAxisInWordSearch()
        {
            string[] NumberedAxis = {"01","02","03","04","05","06","07","08","09","10",
                                     "11","12","13","14","15","16","17","18","19","20"};
            return NumberedAxis;
        }

        static char[,] NewWordSearch(char[] word, char[,] currentWordSearchArray, string[] category)
        {
            int randomNum = SearchType.RandomNumber(0, 2);

            switch(randomNum)
            {
                case 0:  case 1:            // horizontal outputs
                    currentWordSearchArray = Horizontal.OutputWordInWordSearch(word, currentWordSearchArray, category, randomNum);
                    return currentWordSearchArray;
                case 2:  case 3:            // Vertical outpus
                    return currentWordSearchArray;
                case 4:  case 5:        // '/' diagonal 
                    return currentWordSearchArray;                
                case 6:  case 7:        // '\' diagonal
                    return currentWordSearchArray;
                default:
                    return currentWordSearchArray;
            }
        }
        static char[,] DefaultWordSearch()                      // Fills the word search with blank spaces
        {
            char[,] defaultWordSearch = new char[20, 20];

            for (int y_axis = 0; y_axis < defaultWordSearch.GetLength(0); y_axis++)
            {
                for (int x_axis = 0; x_axis < defaultWordSearch.GetLength(1); x_axis++)
                {
                    defaultWordSearch[y_axis, x_axis] = ' ';
                }
            }
            return defaultWordSearch;
        }

        static char[] ConvertWordToCharArray(string word)
        {
            //converts string 'word' into a char[], used for converting category words into char[] in WordSearchCreation()
            char[] charOfWord = word.ToCharArray();
            return charOfWord;
        }
        
        /// <summary>
        /// Creates a string[] of eight random words of user choice category
        /// </summary>
        /// <param name="categoryWordList"> The list of words that the eight words will be chosen from </param>
        /// <returns></returns>
        static string[] RandomWordsFromCategory(string[] categoryWordList)
        {
            string[] randomWords = new string[8];

            // creates an int[] of random eight numbers
            int[] randomIntList = ReturnRandomNumberList(8, 15);

            for (int i = 0; i < 8; i++)
            {
                randomWords[i] = categoryWordList[randomIntList[i]].ToLower();      // Add a random word from categoryWordList into randomWords[]
            }
            return randomWords;
        }
        static char RandomLetter()
        {
            // Outputs a random letter from alphabet[] (excludes X, Y, Z)
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                                'R', 'S', 'T', 'U', 'V', 'W' };

            int index = SearchType.RandomNumber(0, alphabet.Length);
            return alphabet[index];
        }
        public static int[] ReturnRandomNumberList(int maxIndex, int maxRandomNumber)
        {
            int index = 0;
            int[] randomIntList = new int[maxIndex];

            while (index < maxIndex)
            {
                int randomInt = SearchType.RandomNumber(0, maxRandomNumber);

                if (randomIntList.Contains(randomInt))
                {
                    continue;
                }
                else
                {
                    randomIntList[index++] = randomInt;
                }
            }
            return randomIntList;
        }
    }

    class UserInput
    {
        /// <summary>
        /// Checks user input for string and int's for valid inputs
        /// </summary>
        /// <param name="userInputsList"> the list of categories the player can choose from</param>
        /// <returns></returns>
        public static string InputCheck(string[] userInputsList)
        {
            bool invalidInput = true;

            while (invalidInput)                                        // no way to break out of whileloop unless the input is valid
            {
                string? input = Console.ReadLine();

                if (input != null)
                {
                    foreach (string choice in userInputsList)
                    {
                        if (input.ToLower() == choice.ToLower())
                        {
                            return input;
                        }
                    }
                    for (int i = 0; i <= userInputsList.Length + 1; i++)           
                    {
                        if (input == i.ToString())
                        {
                            return input;
                        }
                    }
                    Console.WriteLine("Invalid Input. Try Again.");     // Occcurs only if the other for/each loops don't catch anything
                }
                else
                {
                    Console.WriteLine("Cannot input nothing. Try again.");
                }
            }
            return "Invalid input. Try again";

        }
    }

}
