
namespace WordSearch_PhoenixS
{
    public class UserInput
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
