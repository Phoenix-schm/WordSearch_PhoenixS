
namespace WordSearch_PhoenixS
{
    public class UserInput
    {
        /// <summary>
        /// Checks user input for string and int's for valid inputs
        /// </summary>
        /// <param name="validInputList"> the list of categories the player can choose from</param>
        /// <returns></returns>
        public static string InputCheck(string[] validInputList)
        {
            bool invalidInput = true;

            while (invalidInput)                                                    // no way to break out of whileloop unless the input is valid
            {
                string? userInput = Console.ReadLine();

                if (userInput != null)
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
                    Console.WriteLine("Invalid Input. Try Again.");                 // Occcurs only if the other for/each loops don't catch anything
                }
                else
                {
                    Console.WriteLine("Cannot input nothing. Try again.");
                }
            }
            return "Invalid input. Wait what?";                                     // Shouldn't occur unless something went wrong

        }
    }
}
