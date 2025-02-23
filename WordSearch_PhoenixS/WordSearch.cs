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

            while(isPlaying)
            {
                DisplayStartMessage(validUserInputs);
                Console.Write("Please input your choice of wordsearch: ");
                validInput = UserInput.CheckCategoryChoice(validUserInputs);
                validInput = validInput.ToLower();

                PlayWordSearchFromCategory(validInput, ref isPlaying, validUserInputs);
                Console.WriteLine();
                Console.WriteLine("Congrats! You made it through the word search");
                Console.WriteLine("-----------------------------\n");
            }
        }
        static void DisplayStartMessage(string[] validUserInputs)
        {
            Console.WriteLine("Welcome to the Amazing Word Search");
            Console.WriteLine("You have ten categories to choose from:");
            DisplayValidUserInputs(validUserInputs);
            Console.WriteLine("-------------------------- \n");
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
        }

        /// <summary>
        /// Holds all the choices the player can make
        /// </summary>
        /// <param name="input"> player input</param>
        static void PlayWordSearchFromCategory(string input, ref bool boolean, string[] categoriesDisplay)
        {
            char[,] wordSearch = DefaultWordSearch(' ');

            switch (input)
            {
                case "dog nicknames":           case "1":
                    PlayWordSearch_Game(CategoryList.dogNicknames, wordSearch);
                    break;
                case "colors":                  case "2":
                    PlayWordSearch_Game(CategoryList.colors, wordSearch);
                    break;
                case "poisonous flowers":       case "3":
                    PlayWordSearch_Game(CategoryList.poisonPlants, wordSearch);
                    break;
                case "things in my room":       case "4":
                    PlayWordSearch_Game(CategoryList.thingsInMyRoom, wordSearch);
                    break;
                case "things to eat":           case "5":
                    PlayWordSearch_Game(CategoryList.thingsToEat, wordSearch);
                    break;
                case "fabric types":            case "6":
                    PlayWordSearch_Game(CategoryList.fabrictypes, wordSearch);
                    break;
                case "manga names":             case "7":
                    PlayWordSearch_Game(CategoryList.mangaList, wordSearch);
                    break;
                case "fonts":                   case "8":
                    PlayWordSearch_Game(CategoryList.fonts, wordSearch);
                    break;
                case "dnd monsters":            case "9":
                    PlayWordSearch_Game(CategoryList.dndMonsters, wordSearch);
                    break;
                case "periodic table elements":     case "10":
                    PlayWordSearch_Game(CategoryList.periodicElements, wordSearch);
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
        static void PlayWordSearch_Game(string[] category, char[,] wordSearch)
        {
            string[] eightRandomWords = RandomWordsFromCategory(category);
            wordSearch = CategoryWordSearchCreation(eightRandomWords, wordSearch);

            FindTheWord(wordSearch, eightRandomWords);
        }

        /// <summary>
        /// Creates a word search using the inputCategory
        /// </summary>
        /// <param name="inputCategory"> the category the user chose </param>
        static char[,] CategoryWordSearchCreation(string[] eightCategoryWords, char[,] newWordSearch)
        {
            for(int i = 0; i < eightCategoryWords.Length; i++)                                // Passes in each random word
            {
                char[] randomWord = ConvertWordToCharArray(eightCategoryWords[i]);            // chooses a random word from the list
                newWordSearch = NewWordSearch(randomWord, newWordSearch);                   // Each time a word is passed in it creates a new word search
            }

            return newWordSearch;
        }

        /// <summary>
        /// Displays the word search. Including numbered axises.
        /// </summary>
        /// <param name="wordSearch"> Word search to be displayed</param>
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
                    if (wordSearch[y_axis, x_axis] != ' ' && wordSearch[y_axis, x_axis] != '@')                               // if there's a letter, turn it green (for debugging purposes)
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + fakeWordSearch[y_axis, x_axis] + " ");
                    }
                    else if (wordSearch[y_axis, x_axis] == '@')
                    {
                        Console.Write(" " + " " + " ");
                    }
                    else                                                                        // else, fill the word search with random letters
                    {
                        Console.ResetColor();
                        Console.Write(" " + fakeWordSearch[y_axis, x_axis] + " ");
                    }
                    
                }
                Console.ResetColor();
                Console.WriteLine();
            }
        }
        static char[,] FillFakeWordSearch(char[,] fakeWordSearch, char[,] realWordSearch)
        {
            for (int y_axis = 0; y_axis < fakeWordSearch.GetLength(0); y_axis++)
            {
                for (int x_axis = 0; x_axis < fakeWordSearch.GetLength(1); x_axis++)
                {
                    if (realWordSearch[y_axis, x_axis] != ' ')                                  // if the real word search has a letter
                    {
                        fakeWordSearch[y_axis, x_axis] = realWordSearch[y_axis, x_axis];
                    }
                    else if (fakeWordSearch[y_axis, x_axis] != ' ')                             // if there's already a letter in fakeWordSearch
                    {
                        continue;
                    }
                    else                                                                        // fakeWordSearch is empty
                    {
                        fakeWordSearch[y_axis, x_axis] = RandomLetter();
                    }
                }
            }
            return fakeWordSearch;
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
        /// <param name="currentWordSearch"> The current iteratioin of the word search</param>
        /// <param name="category"> The string array of the category the user chose</param>
        /// <returns></returns>
        static char[,] NewWordSearch(char[] word, char[,] currentWordSearch)
        {
            int[] searchTypeList = ReturnRandomNumberList(8, 8);
            int index = 0;
            bool wasWordPlaced = false;

            while (!wasWordPlaced)                                                  // while the word wasn't placed
            {
                switch (searchTypeList[index])
                {
                    case 0:
                        currentWordSearch = Horizontal.OutputWordInWordSearch(word, currentWordSearch, 0, ref wasWordPlaced);              // Horizontal, in order
                        break;
                    case 1:
                        currentWordSearch = Horizontal.OutputWordInWordSearch(word, currentWordSearch, 1, ref wasWordPlaced);              // Horizontal, in reverse
                        break;
                    case 2:
                        currentWordSearch = Vertical.OutputWordInWordSearch(word, currentWordSearch, 0, ref wasWordPlaced);                // Vertical, in order
                        break;
                    case 3:
                        currentWordSearch = Vertical.OutputWordInWordSearch(word, currentWordSearch, 1, ref wasWordPlaced);                // Vertical, in reverse
                        break;
                    case 4:
                        currentWordSearch = Diagonal.OutputWordInWordSearch(word, currentWordSearch, 0, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in order
                        break;
                    case 5:
                        currentWordSearch = Diagonal.OutputWordInWordSearch(word, currentWordSearch, 1, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in reverse
                        break;
                    case 6:
                        currentWordSearch = Diagonal.OutputWordInWordSearch(word, currentWordSearch, 0, searchTypeList[index], ref wasWordPlaced);    // Diagonal '\', in order
                        break;
                    case 7:
                        currentWordSearch = Diagonal.OutputWordInWordSearch(word, currentWordSearch, 1, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in reverse
                        break;
                }
                index++;
            }
            return currentWordSearch;

            
        }
        
        /// <summary>
        /// Creates the default word search filled with blank spaces
        /// </summary>
        /// <returns> A 20 by 20 2-dimensional character array </returns>
        public static char[,] DefaultWordSearch(char fillerChar)
        {
            char[,] defaultWordSearch = new char[20, 20];

            for (int y_axis = 0; y_axis < defaultWordSearch.GetLength(0); y_axis++)
            {
                for (int x_axis = 0; x_axis < defaultWordSearch.GetLength(1); x_axis++)
                {
                    defaultWordSearch[y_axis, x_axis] = fillerChar;
                }
            }
            return defaultWordSearch;
        }
        static void FindTheWord(char[,] wordSearch,string[] randomWordsList)
        {
            char[,] fakeWordSearch = DefaultWordSearch(' ');                                                // FOR DISPLAY PURPOSES ONLY

            bool isValid;
            foreach(string word in randomWordsList)
            {
                do
                {
                    fakeWordSearch = FillFakeWordSearch(fakeWordSearch, wordSearch);
                    Console.WriteLine("Word Search Puzzle: ");
                    DisplayWordSearch(fakeWordSearch, wordSearch);
                    Console.WriteLine();
                    Console.WriteLine("Search for these words:");
                    foreach (string displayWord in randomWordsList)
                    {
                        Console.WriteLine(displayWord);
                    }

                    string? userInput = UserInput.CheckWordChoice(randomWordsList);

                    int userY_axis = UserInput.CheckIfValidNumber("y");
                    int userX_axis = UserInput.CheckIfValidNumber("x"); 
                    isValid = CheckUserCoordinates(userY_axis, userX_axis, ref wordSearch, userInput);
                    if (!isValid)
                    {
                        Console.WriteLine("That wasn't a valid coordinate. Try again.");
                    }
                    else
                    {
                        for (int i = 0; i < randomWordsList.Length; i++)
                        {
                            if (userInput == randomWordsList[i])
                            {
                                randomWordsList[i] = "";
                            }
                        }
                    }
                } while (!isValid);

            }
        }
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
        /// Creates a list of random numbers with no duplicates
        /// </summary>
        /// <param name="maxNumberInList"> The maximum amount of integers that can be held in the array (exclusive) </param>
        /// <param name="maxRandomNumber"> Array will be filled with numbers betwee 0 (inclusive) and maxRandomNumber (exclusive)</param>
        /// <returns>index of random numbers, no duplicates </returns>
        public static int[] ReturnRandomNumberList(int maxNumberInList, int maxRandomNumber)
        {
            int index = 0;
            int[] randomIntList = new int[maxNumberInList];
            int useZeroOnce = 0;

            while (index < maxNumberInList)
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

        /// <summary>
        /// Outputs a random letter from the alphabet (excludes X, Y, Z) uppercase
        /// </summary>
        /// <returns></returns>
        public static char RandomLetter()
        {
            char[] alphabet = { 'A', 'B', 'C', 'D', 'E', 'F', 'G', 'H', 'I', 'J', 'K', 'L', 'M', 'N', 'O', 'P', 'Q',
                                'R', 'S', 'T', 'U', 'V', 'W' };

            int index = SearchType.RandomNumber(0, alphabet.Length);
            return alphabet[index];
        }

    }
}
