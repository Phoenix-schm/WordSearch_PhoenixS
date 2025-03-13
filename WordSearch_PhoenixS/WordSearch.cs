namespace WordSearch_PhoenixS
{
    class WordSearch
    {
        public enum ValidChoices
        {
            Invalid,
            Dog_Nicknames, Colors, Poisonous_Plants, Things_In_My_Room, Things_To_Eat,
            Fabric_Types, Manga_Titles, Fonts, DND_Monsters, Periodic_Elements,
            Quit
        }
        static void Main(string[] args)
        {
            // variable declarations
            bool isPlaying = true;
            string userInput;

            while (isPlaying)
            {
                Console.WriteLine("Welcome to the Amazing Word Search");
                Console.WriteLine("You have ten categories to choose from:");
                DisplayValidChoices();
                Console.WriteLine("-------------------------- \n");

                userInput = UserInput.CheckCategoryChoice();
                if (userInput == "quit")
                {
                    isPlaying = false;
                }
                else
                {
                    string[] eightRandomWords = RandomWordsFromCategory(CategoryList.CreateCategoryList(userInput));
                    char[,] wordSearch = DefaultWordSearch();                               // Creates word search and fills it with random Uppercase letters,
                                                                                            //      IMPORTANT! Otherwise wordsearch is fill with '\0' and won't fill properly
                    wordSearch = CreateWordSearch(eightRandomWords, wordSearch);
                    PlayWordSearchGame(wordSearch, eightRandomWords);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Displays contents of ValidChoices enum
        /// </summary>
        static void DisplayValidChoices()
        {
            foreach (ValidChoices choice in Enum.GetValues(typeof(ValidChoices)))
            {
                string choiceString = choice.ToString().Replace("_", " ");              // .Replace() replaces left parameter with right paramteter
                if (choice == ValidChoices.Invalid)
                {
                    continue;
                }
                else
                {
                    Console.Write((int)choice + ") " + choiceString);
                    Console.WriteLine();
                }
            }
        }

        /// <summary>
        /// Creates a word search using the inputCategory
        /// </summary>
        /// <param name="eightCategoryWords"> the category the user chose </param>
        /// <param name="newWordSearch">The word search created with each word added to it.</param>
        static char[,] CreateWordSearch(string[] eightCategoryWords, char[,] newWordSearch)
        {
            for (int index = 0; index < eightCategoryWords.Length; index++)                              // Passes in each random word
            {
                newWordSearch = ModifyCurrentWordSearch(eightCategoryWords[index], newWordSearch, ref index);        // Each time a word is passed in it creates a new word search
            }
            return newWordSearch;
        }

        /// <summary>
        /// Prompts the user to find a word and for the words first leters y,x coordinate.
        /// Once the user has successfully found the eight words, exits the foreach loop
        /// Also asks the user if they'd like to return to the main menu, and will let them do so if they type 'return'
        /// </summary>
        /// <param name="wordSearch">The word search being shown.</param>
        /// <param name="eightRandomWords">The list of words the user will have to find.</param>
        static void PlayWordSearchGame(char[,] wordSearch, string[] eightRandomWords)
        {
            string userInput = "";
            bool isValidCoordinates;
            foreach (string word in eightRandomWords)
            {
                do                                                                  // isValid gets set to true when there's a correct checkUserCoordinates
                {                                                                   // Must use do-while so that it goes through this loop again
                    Console.WriteLine("Word Search Puzzle: ");
                    DisplayWordSearch(wordSearch);
                    Console.WriteLine();

                    Console.WriteLine("Search for these words:");
                    foreach (string displayWord in eightRandomWords)                                     // Lists out the remaining words to be found
                    {
                        Console.WriteLine(displayWord.ToUpper());
                    }

                    userInput = UserInput.CheckWordChoice(eightRandomWords);                             // Prompts the user for a word, or to 'return'
                    if (userInput == "return")
                    {
                        break;
                    }

                    int userY_axis = UserInput.CheckIfValidNumber("y");                                  // Prompting user for y and x coordinates
                    int userX_axis = UserInput.CheckIfValidNumber("x");
                    isValidCoordinates = CheckUserCoordinates(userY_axis, userX_axis, ref wordSearch, userInput);
                    if (!isValidCoordinates)
                    {
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("That wasn't a valid coordinate. Try again.");
                        Console.ResetColor();
                    }
                    else                                                                                // If the coordinates were correct then take the word off the list
                    {
                        for (int index = 0; index < eightRandomWords.Length; index++)
                        {
                            if (userInput == eightRandomWords[index])
                            {
                                eightRandomWords[index] = "";
                            }
                        }
                    }
                } while (!isValidCoordinates);

                if (userInput == "return")
                {
                    break;
                }
            }
            if (userInput != "return")                                                                  // If user won
            {
                Console.WriteLine();
                Console.WriteLine("Congrats! You made it through the word search.");
                Console.WriteLine("-----------------------------");
            }
        }

        /// <summary>
        /// Adds 'word' to the word search. Randomly chooses how the word will be displayed in the word search
        /// </summary>
        /// <param name="chosenWord"> Word to be added to the word search</param>
        /// <param name="currentWordSearch"> The current iteratioin of the word search</param>
        /// <param name="wordIndex">The word index of the eightCategoryWords in CreateWordsearch(), resets to 0 if the words couldn't be fit.</param>
        /// <returns>New word search to be modified.</returns>
        static char[,] ModifyCurrentWordSearch(string chosenWord, char[,] currentWordSearch, ref int wordIndex)
        {
            int[] searchTypeList = ReturnRandomNumberList(8, 8);
            int index = 0;
            bool wasWordPlaced = false;
            while (!wasWordPlaced)                                                  // while the word wasn't placed in word search
            {
                switch (searchTypeList[index])
                {
                    case 0:
                        currentWordSearch = Horizontal.PlaceWordInWordSearch(chosenWord, currentWordSearch, ref wasWordPlaced);              // Horizontal, in order
                        break;
                    case 1:
                        chosenWord = ReverseChosenWord(chosenWord);
                        currentWordSearch = Horizontal.PlaceWordInWordSearch(chosenWord, currentWordSearch, ref wasWordPlaced);              // Horizontal, in reverse
                        break;
                    case 2:
                        currentWordSearch = Vertical.PlaceWordInWordSearch(chosenWord, currentWordSearch, ref wasWordPlaced);                // Vertical, in order
                        break;
                    case 3:
                        chosenWord = ReverseChosenWord(chosenWord);
                        currentWordSearch = Vertical.PlaceWordInWordSearch(chosenWord, currentWordSearch, ref wasWordPlaced);                // Vertical, in reverse
                        break;
                    case 4:
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in order
                        break;
                    case 5:
                        chosenWord = ReverseChosenWord(chosenWord);
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in reverse
                        break;
                    case 6:
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, searchTypeList[index], ref wasWordPlaced);    // Diagonal '\', in order
                        break;
                    case 7:
                        chosenWord = ReverseChosenWord(chosenWord);
                        currentWordSearch = Diagonal.PlaceWordInWordSearch(chosenWord, currentWordSearch, searchTypeList[index], ref wasWordPlaced);    // Diagonal '/', in reverse
                        break;
                }
                index++;
                if (index == searchTypeList.Length)                         // Edge case scenario that a word cannot be placed in the word search at ALL
                {                                                           //      Biggest possible point of failure. If words can never be placed into the word search, this will go on foever
                                                                            //      Make sure words are not long enough to permanently keep it resetting endlessly
                    wordIndex = 0;                                          // Reset wordIndex so that it runs through word list again
                    return DefaultWordSearch();
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
        /// <param name="wordSearch">The word search filled with blanks spaces and will and actually be modified.</param>
        public static void DisplayWordSearch(char[,] wordSearch)
        {
            string[] numberedAxis = {"01","02","03","04","05","06","07","08","09","10","11","12","13","14","15","16","17","18","19","20"};

            string numberedYaxis = string.Join(" ", numberedAxis);
            Console.WriteLine("  " + numberedYaxis);                                            // Displays the column numbers

            // Displays the wordsearch, currently colors are for help debugging
            for (int y_axis = 0; y_axis < wordSearch.GetLength(0); y_axis++)
            {
                Console.Write(numberedAxis[y_axis]);                                           // Displays the row number
                for (int x_axis = 0; x_axis < wordSearch.GetLength(1); x_axis++)
                {
                    if (Char.IsLower(wordSearch[y_axis, x_axis]) && wordSearch[y_axis, x_axis] != '@')       // If there's a letter, turn it green (for debugging purposes)
                    {                                                                                       //  and make it Uppercase
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(" " + Char.ToUpper(wordSearch[y_axis, x_axis]) + " ");
                        Console.ResetColor();
                    }
                    else if (wordSearch[y_axis, x_axis] == '@')                                 // If the word was found already
                    {
                        Console.Write(" " + " " + " ");
                    }
                    else                                                                        // Else, show random Uppercase letters
                    {
                        Console.Write(" " + wordSearch[y_axis, x_axis] + " ");
                    }
                }
                Console.WriteLine();
            }
        }

        /// <summary>
        /// The default version of the word search filled with random uppercase letters.
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
                    defaultWordSearch[y_axis, x_axis] = RandomLetter();
                }
            }
            return defaultWordSearch;
        }

        /// <summary>
        /// Outputs a the reverse of a string.
        /// </summary>
        /// <param name="chosenWord">Word being reversed</param>
        /// <returns>Reverse of a string.</returns>
        static string ReverseChosenWord(string chosenWord)
        {
            char[] wordToChar = chosenWord.ToCharArray();
            Array.Reverse(wordToChar);
            return string.Join("", wordToChar);
        }
        /// <summary>
        /// Creates a string[] of eight random words of user choice category
        /// </summary>
        /// <param name="categoryWordList"> The list of words that the eight words will be chosen from </param>
        /// <returns> Returns a string array of eight random words, no repeats.</returns>
        static string[] RandomWordsFromCategory(string[] categoryWordList)
        {
            string[] randomWordsList = new string[8];

            // creates an int[] of random eight numbers
            int[] randomIntList = ReturnRandomNumberList(8, 15);

            for (int index = 0; index < 8; index++)
            {
                randomWordsList[index] = categoryWordList[randomIntList[index]].ToLower();      // Add a random word from categoryWordList into randomWords[]
            }
            return randomWordsList;
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
                    if (randomInt == 0 && useZeroOnce == 0)               // insures 0 will occur at least once in the array
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
