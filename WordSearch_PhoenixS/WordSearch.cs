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
                "Fabric Types", "Manga Names", "Fonts", "DND Monsters", "Periodic Table Elements", 
                "Quit"
            };

            while(isPlaying)
            {
                Console.WriteLine("Welcome to the Amazing Word Search");
                Console.WriteLine("You have ten categories to choose from:");
                DisplayValidUserInputs(validUserInputs);
                Console.WriteLine("-------------------------- \n");

                validInput = UserInput.CheckCategoryChoice(validUserInputs);
                validInput = validInput.ToLower();

                PlayWordSearch_FromCategory(validInput, ref isPlaying);
            }
        }

        /// <summary>
        /// Displays contents of a string array.
        /// </summary>
        /// <param name="validInputsList"> The list of valid user inputs.</param>
        static void DisplayValidUserInputs(string[] validInputsList)
        {
            for (int index = 1; index < validInputsList.Length; index++)
            {
                Console.WriteLine(index + ") " +  validInputsList[index - 1]);
            }
        }

        /// <summary>
        /// Holds all the choices the player can make.
        /// </summary>
        /// <param name="userInput">Player input that will run through the switch statement.</param>
        /// <param name="_isPlaying">Boolean checking if the user has chosen to quit.</param>
        static void PlayWordSearch_FromCategory(string userInput, ref bool _isPlaying)
        {
            switch (userInput)
            {
                case "dog nicknames":           case "1":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("dog nicknames"));
                    break;
                case "colors":                  case "2":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("colors"));
                    break;
                case "poisonous plants":       case "3":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("poisonous plants"));
                    break;
                case "things in my room":       case "4":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("things in my room"));
                    break;
                case "things to eat":           case "5":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("things to eat"));
                    break;
                case "fabric types":            case "6":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("fabric types"));
                    break;
                case "manga names":             case "7":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("manga list"));
                    break;
                case "fonts":                   case "8":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("fonts"));
                    break;
                case "dnd monsters":            case "9":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("ddd monsters"));
                    break;
                case "periodic elements":     case "10":
                    PlayWordSearch_Game(CategoryList.CreateCategoryList("periodic elements"));
                    break;
                case "quit":                    case "11":
                    _isPlaying = false;
                    Console.WriteLine("Bye bye");
                    break;
                default:
                    Console.WriteLine("How the hell did you manage to get this response?");     // somehwow the input was valid but isn't any of the listed items.
                    break;                                                                      // Should occur if you updated the words.txt file but didn't edit this list.
            }
        }
        
        /// <summary>
        /// If the player decides to play,make a list of eightRandomWords, create a word search,
        /// add those words to the word search, and then prompt the user to find the words.
        /// </summary>
        /// <param name="category">The category that eight random words are being taken out of.</param>
        static void PlayWordSearch_Game(string[] category)
        {
            string[] eightRandomWords = RandomWordsFromCategory(category);
            char[,] wordSearch = DefaultWordSearch();                                           // Creates word search and fills it with blanks,
                                                                                                //      IMPORTANT! Otherwise wordsearch is fill with '\0' and won't fill properly
            wordSearch = PlayWordSearch_CreateWordSearch(eightRandomWords, wordSearch);
            PlayWordSearch_FindTheWord(wordSearch, eightRandomWords);
            Console.WriteLine();
        }
        
        /// <summary>
        /// Creates a word search using the inputCategory
        /// </summary>
        /// <param name="inputCategory"> the category the user chose </param>
        static char[,] PlayWordSearch_CreateWordSearch(string[] eightCategoryWords, char[,] newWordSearch)
        {
            for(int index = 0; index < eightCategoryWords.Length; index++)                              // Passes in each random word
            {
                newWordSearch = ModifyCurrentWordSearch(eightCategoryWords[index], newWordSearch, eightCategoryWords);        // Each time a word is passed in it creates a new word search
            }
            return newWordSearch;
        }

        /// <summary>
        /// Prompts the user to find a word and for the words first leters y,x coordinate.
        /// Once the user has successfully found the eight words, exits the foreach loop
        /// Also asks the user if they'd like to return to the main menu, and will let them do so if they type 'return'
        /// </summary>
        /// <param name="wordSearch">The word search being shown.</param>
        /// <param name="randomWordsList">The list of words the user will have to find.</param>
        static void PlayWordSearch_FindTheWord(char[,] wordSearch,string[] randomWordsList)
        {
            char[,] fakeWordSearch = DefaultWordSearch();                                                // FOR DISPLAY PURPOSES ONLY
            string userInput = "";
            bool isValid;
            foreach(string word in randomWordsList)
            {
                do
                {
                    fakeWordSearch = FillFakeWordSearch(fakeWordSearch, wordSearch);                    // Creates a fake word search for display ONLY    
                    Console.WriteLine("Word Search Puzzle: ");
                    DisplayWordSearch(fakeWordSearch, wordSearch);
                    Console.WriteLine();

                    Console.WriteLine("Search for these words:");
                    foreach (string displayWord in randomWordsList)                                     // Lists out the remaining words to be found
                    {
                        Console.WriteLine(displayWord);
                    }

                    userInput = UserInput.CheckWordChoice(randomWordsList);                             // Prompts the user for a word, or to 'return'
                    if (userInput == "return")
                    {
                        break;
                    }
                    int userY_axis = UserInput.CheckIfValidNumber("y");                                 // Prompting user for y and x coordinates
                    int userX_axis = UserInput.CheckIfValidNumber("x"); 
                    isValid = CheckUserCoordinates(userY_axis, userX_axis, ref wordSearch, userInput);
                    if (!isValid)
                    {
                        Console.WriteLine("That wasn't a valid coordinate. Try again.");
                    }
                    else                                                                                // If the coordinates were correct then take the word off the list
                    {
                        for (int index = 0; index < randomWordsList.Length; index++)
                        {
                            if (userInput == randomWordsList[index])
                            {
                                randomWordsList[index] = "";
                            }
                        }
                    }
                } while (!isValid);
                if (userInput == "return")
                {
                    break;
                }
            }
            if (userInput != "return")                                                                  // If user won
            {
                Console.WriteLine();
                Console.WriteLine("Congrats! You made it through the word search");
                Console.WriteLine("-----------------------------");
            }
        }

        /// <summary>
        /// Adds 'word' to the word search. Randomly chooses how the word will be displayed in the word search
        /// </summary>
        /// <param name="chosenWord"> Word to be added to the word search</param>
        /// <param name="currentWordSearch"> The current iteratioin of the word search</param>
        /// <returns>New word search to be modified.</returns>
        static char[,] ModifyCurrentWordSearch(string chosenWord, char[,] currentWordSearch, string[] eightCategoryWords)
        {
            int[] searchTypeList = ReturnRandomNumberList(8, 8);
            int index = 0;
            bool wasWordPlaced = false;
            while (!wasWordPlaced)                                                  // while the word wasn't placed in word search
            {
                switch (searchTypeList[index])
                {
                    case 0:
                        currentWordSearch = Horizontal.PlaceWordInWordSearch(chosenWord, currentWordSearch, 0, ref wasWordPlaced);              // Horizontal, in order
                        break;
                    case 1:
                        currentWordSearch = Horizontal.PlaceWordInWordSearch(chosenWord, currentWordSearch, 1, ref wasWordPlaced);              // Horizontal, in reverse
                        break;
                    case 2:
                        currentWordSearch = Vertical.PlaceWordInWordSearch(chosenWord, currentWordSearch, 0, ref wasWordPlaced);                // Vertical, in order
                        break;
                    case 3:
                        currentWordSearch = Vertical.PlaceWordInWordSearch(chosenWord, currentWordSearch, 1, ref wasWordPlaced);                // Vertical, in reverse
                        break;
                    case 4:
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, 0, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in order
                        break;
                    case 5:
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, 1, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in reverse
                        break;
                    case 6:
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, 0, searchTypeList[index], ref wasWordPlaced);    // Diagonal '\', in order
                        break;
                    case 7:
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, 1, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in reverse
                        break;
                }
                index++;
                if (index == searchTypeList.Length)                         // Edge case scenario that a word cannot be placed in the word search at ALL
                {                                                           //      Biggest possible point of failure. If words can never be placed into the word search
                                                                            //      Then this while loop will go on forever. Make sure that words are short enough not to cause issues.
                    char[,] startOverWordSearch = DefaultWordSearch();
                    PlayWordSearch_CreateWordSearch(eightCategoryWords, startOverWordSearch);
                }
            }
            return currentWordSearch;
        }

        /// <summary>
        /// Checks the parameters userY and userX for if they correctly are the index of chosenWords first letter
        /// </summary>
        /// <param name="userY">The inputted y-coordinate.</param>
        /// <param name="userX">The inputted x-coordinate.</param>
        /// <param name="wordSearch">The word search being checked.</param>
        /// <param name="chosenWord">The word being checked for.</param>
        /// <returns></returns>
        static bool CheckUserCoordinates(int userY, int userX, ref char[,] wordSearch, string chosenWord)
        {
            bool isValid = Horizontal.CheckUserCoordinates(userY, userX, ref wordSearch, chosenWord);
            if (!isValid)
            {
                isValid = Vertical.CheckUserCoordinates(userY, userX, ref wordSearch, chosenWord);
                if (!isValid)
                {
                    isValid = Diagonal.CheckUserCoordinates(userY, userX, ref wordSearch, chosenWord);
                }
            }
            return isValid;
        }

        /// <summary>
        /// Displays the word search. Including numbered axises.
        /// </summary>
        /// <param name="fakeWordSearch">The word search meant only for display.</param>
        /// <param name="wordSearch">The word search filled with blanks spaces and will and actually be modified.</param>
        public static void DisplayWordSearch(char[,] fakeWordSearch, char[,] wordSearch)
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
                    if (wordSearch[y_axis, x_axis] != ' ' && wordSearch[y_axis, x_axis] != '@')       // If there's a letter, turn it green (for debugging purposes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + fakeWordSearch[y_axis, x_axis] + " ");
                        Console.ResetColor();
                    }
                    else if (wordSearch[y_axis, x_axis] == '@')                                 // If the word was found already
                    {
                        Console.Write(" " + " " + " ");
                    }
                    else                                                                        // Else, show fake word search's random letters
                    {
                        Console.Write(" " + fakeWordSearch[y_axis, x_axis] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// Fills a fake word search with the words of realWordSearch AND random letters everywhere else
        /// </summary>
        /// <param name="fakeWordSearch">The fake word search being displayed.</param>
        /// <param name="realWordSearch">The real word search being modified and checked in the game.</param>
        /// <returns></returns>
        static char[,] FillFakeWordSearch(char[,] fakeWordSearch, char[,] realWordSearch)
        {
            for (int y_axis = 0; y_axis < fakeWordSearch.GetLength(0); y_axis++)
            {
                for (int x_axis = 0; x_axis < fakeWordSearch.GetLength(1); x_axis++)
                {
                    if (realWordSearch[y_axis, x_axis] != ' ')                                  // If the real word search has a letter
                    {
                        fakeWordSearch[y_axis, x_axis] = realWordSearch[y_axis, x_axis];
                    }
                    else if (fakeWordSearch[y_axis, x_axis] != ' ')                             // If there's already a letter in fakeWordSearch
                    {
                        continue;
                    }
                    else                                                                        // FakeWordSearch has an empty space
                    {
                        fakeWordSearch[y_axis, x_axis] = RandomLetter();
                    }
                }
            }
            return fakeWordSearch;
        }

        /// <summary>
        /// The default version of the word search filled with blanks.
        /// Important due to creating a blank [20,20] fills the grid with '\0' and makes checking and modifications useless.
        /// </summary>
        /// <returns>A default word search filled with blank characters.</returns>
        public static char[,] DefaultWordSearch()
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
        /// Creates a string[] of eight random words of user choice category
        /// </summary>
        /// <param name="categoryWordList"> The list of words that the eight words will be chosen from </param>
        /// <returns> Returns a string array of eight random words, no repeats.</returns>
        static string[] RandomWordsFromCategory(string[] categoryWordList)
        {
            string[] randomWords = new string[8];

            // creates an int[] of random eight numbers
            int[] randomIntList = ReturnRandomNumberList(8, 15);

            for (int index = 0; index < 8; index++)
            {
                randomWords[index] = categoryWordList[randomIntList[index]].ToUpper();      // Add a random word from categoryWordList into randomWords[]
            }
            return randomWords;
        }

        /// <summary>
        /// Returns a list of random numbers, no duplicates.
        /// </summary>
        /// <param name="maxIndexOfList">The maximum amount fo numbers that can be in the list.</param>
        /// <param name="maxRandomNumber">The largest number that can be input into the list.</param>
        /// <returns>Returns a list of random numbers.</returns>
        public static int[] ReturnRandomNumberList(int maxIndexOfList, int maxRandomNumber)
        {
            int index = 0;
            int[] randomIntList = new int[maxIndexOfList];
            int useZeroOnce = 0;

            while (index < maxIndexOfList)
            {
                int randomInt = SearchType.RandomNumber(0, maxRandomNumber);

                if (randomIntList.Contains(randomInt) && randomInt != 0)  // because randomIntList[] already has 0, must allow 0 to pass through
                {
                    continue;
                }
                else
                {
                    if (randomInt == 0 && useZeroOnce == 0)                // insures 0 will occur at least once in the array
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

        /// <summary>
        /// Outputs a random letter from the alphabet (excludes X, Y, Z) uppercase
        /// </summary>
        /// <returns>Returns a random letter.</returns>
        public static char RandomLetter()
        {
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                                'R', 'S', 'T', 'U', 'V', 'W' };
            int index = SearchType.RandomNumber(0, alphabet.Length);
            return alphabet[index];
        }

    }
}
