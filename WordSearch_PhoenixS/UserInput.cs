
namespace WordSearch_PhoenixS
{
    public class UserInput
    {
        /// <summary>
        /// Checks user input for string and int's for valid inputs
        /// </summary>
        /// <param name="validInputList"> the list of categories the player can choose from</param>
        /// <returns></returns>
        public static string CheckCategoryChoice(string[] validInputList)
        {
            bool validInput = false;

            while (!validInput)                                                    // no way to break out of whileloop unless the input is valid
            {
                string? userInput = Console.ReadLine();

                if (userInput != "")
                {
                    foreach (string choice in validInputList)                       // checks for if userInput is one of the listed valid inputs (string)
                    {
                        if (userInput.ToLower() == choice.ToLower())
                        {
                            return userInput;
                        }
                    }
                    for (int i = 0; i <= validInputList.Length + 1; i++)            // checks if the user is a valid number
                    {
                        if (userInput == i.ToString())
                        {
                            return userInput;
                        }
                    }
                    Console.WriteLine("That is not a listed input. Try Again.");                 // Occcurs only if the other for/each loops don't catch anything
                }
                else
                {
                    Console.WriteLine("Cannot input nothing. Try again.");
                }
            }
            return "Invalid input. Wait what?";                                     // Shouldn't occur unless something went wrong
        }

        public static string CheckWordChoice(string[] eightCategoryWord)
        {
            bool validInput = false;
            Console.Write("Type the word when you've found it in the wordsearch: ");

            while (!validInput)                                     // no way to break out of while
            {
                string? userInput = Console.ReadLine();

                if (userInput != "")
                {
                    foreach (string word in eightCategoryWord)
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
            return "Invalid input. Wait, that's not right";
        }
        public static int CheckIfValidNumber(string axis)
        {
            bool validInput = false;
            Console.Write("Type in the " + axis + " coordinate of the first letter of the word: ");
            
            while (!validInput)
            {
                string? userInput = Console.ReadLine();
                if (userInput != null || userInput != "")
                {
                    int coordinate = 0;
                    char[] userInputChar = userInput.ToCharArray();
                    foreach(char letter in userInputChar)
                    {
                        if (!Char.IsDigit(letter))
                        {
                            Console.WriteLine("That wasn't a number!");
                            break;
                        }
                        else
                        {
                            int.TryParse(userInput, out coordinate);
                            coordinate -= 1;
                            if (coordinate < 0 || coordinate > 19)
                            {
                                Console.WriteLine("That number is out of range");
                                break;
                            }
                            else
                            {
                                return coordinate;
                            }
                        }
                    }
                    
                }
            }

            Console.WriteLine("Something bad happened here.");
            return -1;
        }

        public static void CheckLetterIndex(int y, int x)
        {

        }
    }
}
