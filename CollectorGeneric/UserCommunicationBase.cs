using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric
{
    public abstract class UserCommunicationBase
    {
        protected string? GetDataFromUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            string userInput = Console.ReadLine();
            return userInput;
        }
        
        protected void ShowBug(string bug)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWystąpił błąd: {bug}");
            Console.ResetColor();
        }
        protected void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        protected void ShowRepo(IReadRepository<Numismatics> repository)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nZawartość Twojej kolekcji:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", "Id", "Symbol", "Nazwa", "Nominał", "Waluta", "Rok wyd.");
            Console.WriteLine(("\t").PadRight(83, '-'));
            Console.ResetColor();

            var items = repository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", item.Id, item.Symbol, item.Name, item.Denomination, item.Currency, item.YearOfRelease);
            }
        }
    }
}
