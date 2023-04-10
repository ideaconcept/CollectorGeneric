using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric
{
    internal class EventHandlerService : IEventHandlerService
    {
        private readonly IRepository<Coins> _coinsRepository;
        private readonly IRepository<Banknotes> _banknotesRepository;

        private const string auditFileName = "auditLog.txt";

        public EventHandlerService(IRepository<Coins> coinsRepository, IRepository<Banknotes> banknotesRepository)
        {
            _coinsRepository = coinsRepository;
            _banknotesRepository = banknotesRepository;
        }

        public void ListenForEvents()
        {
            _coinsRepository.ItemAdded += RepositoryOnCoinAdded;
            _coinsRepository.ItemRemoved += RepositoryOnCoinRemove;
            _banknotesRepository.ItemAdded += RepositoryOnBanknoteAdded;
            _banknotesRepository.ItemRemoved += RepositoryOnBanknoteRemove;
        }

        static void RepositoryOnCoinAdded(object? sender, Coins e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nDodano monetę: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Material} {e.Diameter} {e.Weight} from {sender?.GetType().Name}");
            Console.ResetColor();
            SaveLogToFile($"{System.DateTime.Now};Coins Added;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Material}, {e.Diameter}, {e.Weight}");
        }

        static void RepositoryOnCoinRemove(object? sender, Coins e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nUsunięto monetę: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Material} {e.Diameter} {e.Weight} from {sender?.GetType().Name}");
            Console.ResetColor();
            SaveLogToFile($"{System.DateTime.Now};Coins Remove;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Material}, {e.Diameter}, {e.Weight}");
        }

        static void RepositoryOnBanknoteAdded(object? sender, Banknotes e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nDodano banknot: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Length} {e.Width} {e.Watermark} from {sender?.GetType().Name}");
            Console.ResetColor();
            SaveLogToFile($"{System.DateTime.Now};Banknotes Added;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Width}, {e.Length}, {e.Watermark}");
        }

        static void RepositoryOnBanknoteRemove(object? sender, Banknotes e)
        {
            Console.ForegroundColor = ConsoleColor.DarkRed;
            Console.WriteLine($"\nUsunięto banknot: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Length} {e.Width} {e.Watermark} from {sender?.GetType().Name}");
            Console.ResetColor();
            SaveLogToFile($"{System.DateTime.Now};Banknotes Remove;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Length}, {e.Width}, {e.Watermark}");
        }

        private static void SaveLogToFile(string auditLog)
        {
            using (var writer = File.AppendText(auditFileName))
            {
                writer.WriteLine(auditLog);
            }
        }
    }
}
