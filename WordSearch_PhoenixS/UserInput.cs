
namespace WordSearch_PhoenixS
{
    public class UserInput
    {
        /// <summary>
        /// Checks if the userInput contained one of the validInputList.
        /// </summary>
        /// <param name="validChoiceList"> The list of categories the player can choose from.</param>
        /// <returns>The valid userInput.</returns>
        public static string CheckCategoryChoice(string[] validChoiceList)
        {
            bool validInput = false;
            while (!validInput)                                                         // No way to break out of whileloop unless the input is valid
            {
                Console.Write("Please input your choice of wordsearch: ");
                string? userInput = Console.ReadLine();

                if (userInput != "" && userInput != null)
                {
                    foreach (string choice in validChoiceList)                           // Checks for if userInput is one of the listed valid inputs (string)
                    {
                        if (userInput.ToLower() == choice.ToLower())
                        {
                            return userInput;
                        }
                    }
                    for (int index = 0; index <= validChoiceList.Length + 1; index++)    // Checks if the user is a valid number
                    {
                        if (userInput == index.ToString())
                        {
                            return userInput;
                        }
                    }
                    Console.WriteLine("That is not a listed input. Try Again.");         // Occcurs only if the other user input wasn't valid
                }
                else
                {
                    Console.WriteLine("Cannot input nothing. Try again.");
                }
            }
            return "Invalid choice. Wait what?";                                         // Shouldn't occur unless something went wrong
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
                        if (userInput.ToUpper() == word)
                        {
                            return word;
                        }
                    }
                    Console.WriteLine("That is not a word in the word search");
                }
                else
                {
                    Console.WriteLine("Cannot input nothing. Try again");
                }
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
                    int coordinate = 0;
                    char[] userInputChar = userInput.ToCharArray();
                    foreach(char letter in userInputChar)
                    {
                        if (!Char.IsDigit(letter))                                  // If userInput contains a letter
                        {
                            Console.WriteLine("That wasn't a number!");
                            break;
                        }
                        else
                        {
                            int.TryParse(userInput, out coordinate);
                            coordinate -= 1;                                        // To accomodate the off-by-one
                            if (coordinate < 0 || coordinate > 19)
                            {
                                Console.WriteLine("That number is out of range.");
                                break;
                            }
                            else
                            {
                                return coordinate;
                            }
                        }
                    }
                }
                else
                {
                    Console.WriteLine("Cannot input nothing.");
                }
            }

            Console.WriteLine("Something bad happened here.");                      // Should never occur
            return -1;
        }
    }
}
