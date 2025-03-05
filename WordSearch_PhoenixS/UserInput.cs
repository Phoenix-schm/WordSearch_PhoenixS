namespace WordSearch_PhoenixS
{
    public class UserInput
    {
        /// <summary>
        /// Checks if the userInput contained one of the ValidChoices enum.
        /// </summary>
        /// <returns>The valid userInput.</returns>
        public static string CheckCategoryChoice()
        {
            bool validInput = false;
            while (!validInput)                                                         // No way to break out of whileloop unless the input is valid
            {
                Console.Write("Please input your choice of wordsearch: ");
                string? userInput = Console.ReadLine();

                if (userInput != "" && userInput != null)
                {
                    foreach (WordSearch.ValidChoices choice in Enum.GetValues(typeof(WordSearch.ValidChoices)))
                    {
                        string choiceString = choice.ToString().Replace("_", " ");      // .Replace() replaces left parameter with right paramteter
                        int choiceInt = (int)choice;                                    // ToString() doesn't like modifying a cast

                        if (choiceString == "Invalid")
                        {
                            continue;
                        }
                        else if (userInput == choiceInt.ToString() || userInput.ToLower() == choiceString.ToLower()) // checks userinput is either a correct number or choice name
                        {
                            return choiceString.ToLower();
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a listed input. Try Again.");        // Occcurs only if the other user input wasn't valid
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cannot input nothing. Try again.");
                }
                Console.ResetColor();
            }
            return "Invalid choice. Wait what?";                                        // Shouldn't occur unless something went wrong
        }
        /// <summary>
        /// Checks if userInput is one of the eightValidWords or if they typed "return".
        /// </summary>
        /// <param name="eightValidWords"> The eightValid words the player can choose from.</param>
        /// <returns>The valid word.</returns>
        public static string CheckWordChoice(string[] eightValidWords)
        {
            bool validInput = false;
            Console.WriteLine("Type 'return' if you wish to return to the main menu");

            while (!validInput)                                                             // No way to break out of whileloop without a valid userInput
            {
                Console.Write("Type a word when you've found it in the wordsearch: ");
                string? userInput = Console.ReadLine();

                if (userInput != "" && userInput != null)
                {
                    if (userInput.ToLower() == "return")
                    {
                        return "return";
                    }
                    foreach (string word in eightValidWords)                                // Going through each word in eightValidWords
                    {
                        if (userInput.ToLower() == word)
                        {
                            return word;
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That is not a word in the word search");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cannot input nothing. Try again");
                }
                Console.ResetColor();
            }
            return "Invalid word choice. Wait, that's not right.";                          // Should never occur
        }

        /// <summary>
        /// Checks if the user inputted a number between 1 and 20.
        /// </summary>
        /// <param name="axis">The axis of the word search to be asked for.</param>
        /// <returns>The valid numbers.</returns>
        public static int CheckIfValidNumber(string axis)
        {
            bool validInput = false;

            while (!validInput)
            {
                Console.Write("Type in the " + axis + " coordinate of the first letter of the word: ");
                string? userInput = Console.ReadLine();

                if (userInput != null && userInput != "")
                {
                    int[] validCoordinates = new int[20];
                    for (int coordinate = 1; coordinate <= validCoordinates.Length; coordinate++)           // Checks between 1 and 20 (inclusive)
                    {
                        if (userInput == coordinate.ToString())
                        {
                            return coordinate - 1;                          // -1 to accomodate off-by-one in word search array
                        }
                    }
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("That wasn't a valid coordinate.");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Cannot input nothing.");
                }
                Console.ResetColor();
            }
            Console.WriteLine("Something bad happened here.");             // Should never occur
            return -1;
        }
    }
}
