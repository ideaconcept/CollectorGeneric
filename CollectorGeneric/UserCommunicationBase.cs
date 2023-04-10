namespace CollectorGeneric
{
    public abstract class UserCommunicationBase
    {
        private static string? GetDataFromUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            string userInput = Console.ReadLine();
            return userInput;
        }
    }
}
