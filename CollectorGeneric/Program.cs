using CollectorGeneric;
using CollectorGeneric.DataProviders;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();

services.AddSingleton<IRepository<Numismatics>, ListRepository<Numismatics>>();
services.AddSingleton<IRepository<Coins>, ListRepository<Coins>>();
services.AddSingleton<IRepository<Banknotes>, ListRepository<Banknotes>>();
services.AddSingleton<INumismaticsProvider, NumismaticsProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app.Run();




//using CollectorGeneric.Data;
//using CollectorGeneric.Entities;
//using CollectorGeneric.Repositories;

//namespace CollectorGeneric
//{
//    internal class Program
//    {
//        private const string auditFileName = "auditLog.txt";

//        private static void Main()
//        {
//            //var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());
//            var coinsRepository = new FileRepository<Coins>();
//            var banknotesRepository = new FileRepository<Banknotes>();
//            coinsRepository.LoadRepository();
//            banknotesRepository.LoadRepository();




//            coinsRepository.ItemAdded += RepositoryOnCoinAdded;
//            coinsRepository.ItemRemove += RepositoryOnCoinRemove;
//            banknotesRepository.ItemAdded += RepositoryOnBanknoteAdded;
//            banknotesRepository.ItemRemove += RepositoryOnBanknoteRemove;

//            static void RepositoryOnCoinAdded(object? sender, Coins e)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine($"\nDodano monetę: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Material} {e.Diameter} {e.Weight} from {sender?.GetType().Name}");
//                Console.ResetColor();
//                SaveLogToFile($"{System.DateTime.Now};Coins Added;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Material}, {e.Diameter}, {e.Weight}");
//            }

//            static void RepositoryOnCoinRemove(object? sender, Coins e)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine($"\nUsunięto monetę: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Material} {e.Diameter} {e.Weight} from {sender?.GetType().Name}");
//                Console.ResetColor();
//                SaveLogToFile($"{System.DateTime.Now};Coins Remove;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Material}, {e.Diameter}, {e.Weight}");
//            }

//            static void RepositoryOnBanknoteAdded(object? sender, Banknotes e)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine($"\nDodano banknot: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Length} {e.Width} {e.Watermark} from {sender?.GetType().Name}");
//                Console.ResetColor();
//                SaveLogToFile($"{System.DateTime.Now};Banknotes Added;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Width}, {e.Length}, {e.Watermark}");
//            }

//            static void RepositoryOnBanknoteRemove(object? sender, Banknotes e)
//            {
//                Console.ForegroundColor = ConsoleColor.DarkRed;
//                Console.WriteLine($"\nUsunięto banknot: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} {e.YearOfRelease} {e.Length} {e.Width} {e.Watermark} from {sender?.GetType().Name}");
//                Console.ResetColor();
//                SaveLogToFile($"{System.DateTime.Now};Banknotes Remove;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}, {e.YearOfRelease}, {e.Length}, {e.Width}, {e.Watermark}");
//            }




//            ShowMenu();

//            while (true)
//            {
//                Console.ForegroundColor = ConsoleColor.Yellow;
//                Console.Write("\nWybierz 1, 2, 3, 4, 5 lub X aby zakończyć pracę programu. Twój wybór: ");
//                Console.ResetColor();

//                Console.ForegroundColor = ConsoleColor.Green;
//                string choice = Console.ReadLine();
//                Console.ResetColor();

//                if (choice == "1")
//                {
//                    ShowMenu();

//                    Console.ForegroundColor = ConsoleColor.Green;
//                    Console.WriteLine("\nWprowadź dane monety:\n");
//                    Console.ResetColor();

//                    try
//                    {
//                        var symbol = GetDataFromUser("Symbol: ");
//                        var name = GetDataFromUser("Nazwa: ");
//                        var denomination = GetDataFromUser("Nominał: ");
//                        var currency = GetDataFromUser("Waluta: ");
//                        var yearOfRelease = GetDataFromUser("Rok wydania: ");
//                        var material = GetDataFromUser("Materiał: ");
//                        var diameter = GetDataFromUser("Średnica: ");
//                        var weight = GetDataFromUser("Waga (g): ");
//                        Console.ResetColor();

//                        denomination = denomination.Replace(".", ",");
//                        diameter = diameter.Replace(".", ",");
//                        weight = weight.Replace(".", ",");

//                        coinsRepository.Add(new Coins { Symbol = symbol, Name = name, Denomination = float.Parse(denomination), Currency = currency, YearOfRelease = float.Parse(yearOfRelease), Material = material, Diameter = float.Parse(diameter), Weight = float.Parse(weight) });
//                    }
//                    catch (Exception e)
//                    {
//                        ShowBug(e.Message);
//                    }
//                }
//                else if (choice == "2")
//                {
//                    ShowMenu();

//                    Console.ForegroundColor = ConsoleColor.Green;
//                    Console.WriteLine("\nWprowadź dane banknotu:\n");
//                    Console.ResetColor();

//                    try
//                    {
//                        var symbol = GetDataFromUser("Symbol: ");
//                        var name = GetDataFromUser("Nazwa: ");
//                        var denomination = GetDataFromUser("Nominał: ");
//                        var currency = GetDataFromUser("Waluta: ");
//                        var yearOfRelease = GetDataFromUser("Rok wydania: ");
//                        var lenght = GetDataFromUser("Długość: ");
//                        var width = GetDataFromUser("Wysokość: ");
//                        var watermark = GetDataFromUser("Znak wodny: ");
//                        Console.ResetColor();

//                        lenght = lenght.Replace(".", ",");
//                        width = width.Replace(".", ",");

//                        banknotesRepository.Add(new Banknotes { Symbol = symbol, Name = name, Denomination = float.Parse(denomination), Currency = currency, YearOfRelease = float.Parse(yearOfRelease), Length = float.Parse(lenght), Width = float.Parse(width), Watermark = watermark });
//                    }
//                    catch (Exception e)
//                    {
//                        ShowBug(e.Message);
//                    }
//                }
//                else if (choice == "3")
//                {
//                    ShowMenu();

//                    ShowRepo(coinsRepository);
//                    Console.ForegroundColor = ConsoleColor.Green;
//                    Console.WriteLine("\nPodaj numer Id monety, którą chcesz usunąć z kolekcji:\n");

//                    try
//                    {
//                        Console.ResetColor(); var symbol = GetDataFromUser("ID: ");
//                        coinsRepository.Remove(coinsRepository.GetById((int)float.Parse(symbol)));
//                    }
//                    catch (Exception e)
//                    {
//                        ShowBug("Wprowadzono złą wartość identyfikatora Id.\n");
//                    }
//                }
//                else if (choice == "4")
//                {
//                    ShowMenu();

//                    ShowRepo(banknotesRepository);
//                    Console.ForegroundColor = ConsoleColor.Green;
//                    Console.WriteLine("\nPodaj numer Id banknotu, który chcesz usunąć z kolekcji:\n");

//                    try
//                    {
//                        Console.ResetColor(); var symbol = GetDataFromUser("ID: ");
//                        banknotesRepository.Remove(banknotesRepository.GetById((int)float.Parse(symbol)));
//                    }
//                    catch (Exception e)
//                    {
//                        ShowBug("Wprowadzono złą wartość identyfikatora Id.\n");
//                    }
//                }
//                else if (choice == "5")
//                {
//                    try
//                    {
//                        Console.ForegroundColor = ConsoleColor.Blue;
//                        Console.Write("\nZawartość Twojej kolekcji:\n\n");
//                        Console.ResetColor();
//                        WriteAllToConsole(coinsRepository);
//                        WriteAllToConsole(banknotesRepository);
//                    }
//                    catch (Exception e)
//                    {
//                        ShowBug(e.Message);
//                    }
//                }
//                else if (choice == "6")
//                {
//                    ShowMenu();
//                    try
//                    {
//                        if (File.Exists(Program.auditFileName))
//                        {
//                            using (var reader = File.OpenText(Program.auditFileName))
//                            {
//                                Console.ForegroundColor = ConsoleColor.Blue;
//                                Console.Write("\nZawartość pliku logów audytu:\n\n");
//                                Console.ForegroundColor = ConsoleColor.DarkYellow;
//                                Console.WriteLine("\t{0,-20} {1,-20} {2,-20}", "Data", "Operacja", "Obiekt");
//                                Console.WriteLine(("\t").PadRight(100, '-'));
//                                Console.ResetColor();

//                                var line = reader.ReadLine();
//                                while (line != null)
//                                {
//                                    var record = line.Split(';');
//                                    Console.WriteLine("\t{0,-20} {1,-20} {2,-20}", record[0], record[1], record[2]);
//                                    line = reader.ReadLine();
//                                }
//                            }
//                        }
//                    }
//                    catch (Exception e)
//                    {
//                        ShowBug(e.Message);
//                    }
//                }
//                else if (choice == "X" || choice == "x")
//                {
//                    coinsRepository.Save();
//                    banknotesRepository.Save();
//                    Environment.Exit(0);
//                }
//                else
//                {
//                    ShowBug("Wprowadzono złą wartość.\n");
//                }
//            }
//        }

//        static void WriteAllToConsole(IReadRepository<IEntity> repository)
//        {
//            var items = repository.GetAll();
//            foreach (var item in items)
//            {
//                Console.WriteLine(item);
//            }
//        }

//        static void ShowRepo(IReadRepository<Numismatics> repository)
//        {
//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.Write("\nZawartość Twojej kolekcji:\n\n");
//            Console.ForegroundColor = ConsoleColor.DarkYellow;
//            Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", "Id", "Symbol", "Nazwa", "Nominał", "Waluta", "Rok wyd.");
//            Console.WriteLine(("\t").PadRight(83, '-'));
//            Console.ResetColor();

//            var items = repository.GetAll();
//            foreach (var item in items)
//            {
//                Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", item.Id, item.Symbol, item.Name, item.Denomination, item.Currency, item.YearOfRelease);
//            }
//        }

//        private static void ShowBug(string bug)
//        {
//            Console.ForegroundColor = ConsoleColor.Red;
//            Console.WriteLine($"\nWystąpił błąd: {bug}");
//            Console.ResetColor();
//        }

//        private static void ShowMenu()
//        {
//            Console.Clear();
//            Console.ForegroundColor = ConsoleColor.Blue;
//            Console.WriteLine("          Witamy w programie Kolekcjoner 'Generic' (InFile):");
//            Console.ForegroundColor = ConsoleColor.DarkGray;
//            Console.WriteLine("====================================================================");
//            Console.ResetColor();
//            Console.WriteLine("   1. Dodawanie monety do kolekcji");
//            Console.WriteLine("   2. Dodawanie banknotu do kolekcji");
//            Console.WriteLine("   3. Usunięcie monety z kolekcji");
//            Console.WriteLine("   4. Usunięcie banknotu z kolekcji");
//            Console.WriteLine("   5. Wyświetl zasób kolekcji (repozytorium generyczne)");
//            Console.WriteLine("   6. Wyświetl zawartość plik audytu");
//            Console.WriteLine("   X. Zakończ pracę programu");
//            Console.ForegroundColor = ConsoleColor.DarkGray;
//            Console.WriteLine("====================================================================");
//            Console.ResetColor();
//        }

//        private static string? GetDataFromUser(string message)
//        {
//            Console.ForegroundColor = ConsoleColor.Yellow;
//            Console.Write(message);
//            Console.ForegroundColor = ConsoleColor.Gray;
//            string userInput = Console.ReadLine();
//            return userInput;
//        }

//        private static void SaveLogToFile(string auditLog)
//        {
//            using (var writer = File.AppendText(auditFileName))
//            {
//                writer.WriteLine(auditLog);
//            }
//        }
//    }
//}


