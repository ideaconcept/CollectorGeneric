using CollectorGeneric.DataProviders;
using CollectorGeneric.Data;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;
using System.Xml.Linq;

namespace CollectorGeneric
{
    public class UserCommunication : UserCommunicationBase, IUserCommunication
    {
        private readonly IRepository<Numismatics> _numismaticsRepository;
        private readonly IRepository<Coins> _coinsRepository;
        private readonly IRepository<Banknotes> _banknotesRepository;
        private readonly INumismaticsProvider _numismaticsProvider;

        private const string auditFileName = "auditLog.txt";

        public UserCommunication(
            IRepository<Numismatics> numismaticsRepository,
            IRepository<Coins> coinsRepository,
            IRepository<Banknotes> banknotesRepository,
            INumismaticsProvider numismaticsProvider)
        {
            _numismaticsRepository = numismaticsRepository;
            _coinsRepository = coinsRepository;
            _banknotesRepository = banknotesRepository;
            _numismaticsProvider = numismaticsProvider;

            //var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());
            _coinsRepository.LoadRepository();
            _banknotesRepository.LoadRepository();
        }

        public void SelectAMenuOption()
        {
            ShowMenu();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nWybierz 1, 2, 3, 4, 5 lub X aby zakończyć pracę programu. Twój wybór: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                string choice = Console.ReadLine();
                Console.ResetColor();

                if (choice == "1")
                {
                    ShowMenu();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nWprowadź dane monety:\n");
                    Console.ResetColor();

                    try
                    {
                        var symbol = GetDataFromUser("Symbol: ");
                        var name = GetDataFromUser("Nazwa: ");
                        var denomination = GetDataFromUser("Nominał: ");
                        var currency = GetDataFromUser("Waluta: ");
                        var yearOfRelease = GetDataFromUser("Rok wydania: ");
                        var material = GetDataFromUser("Materiał: ");
                        var diameter = GetDataFromUser("Średnica: ");
                        var weight = GetDataFromUser("Waga (g): ");
                        Console.ResetColor();

                        denomination = denomination.Replace(".", ",");
                        diameter = diameter.Replace(".", ",");
                        weight = weight.Replace(".", ",");

                        _coinsRepository.Add(new Coins { Symbol = symbol, Name = name, Denomination = float.Parse(denomination), Currency = currency, YearOfRelease = float.Parse(yearOfRelease), Material = material, Diameter = float.Parse(diameter), Weight = float.Parse(weight) });
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "2")
                {
                    ShowMenu();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nWprowadź dane banknotu:\n");
                    Console.ResetColor();

                    try
                    {
                        var symbol = GetDataFromUser("Symbol: ");
                        var name = GetDataFromUser("Nazwa: ");
                        var denomination = GetDataFromUser("Nominał: ");
                        var currency = GetDataFromUser("Waluta: ");
                        var yearOfRelease = GetDataFromUser("Rok wydania: ");
                        var lenght = GetDataFromUser("Długość: ");
                        var width = GetDataFromUser("Wysokość: ");
                        var watermark = GetDataFromUser("Znak wodny: ");
                        Console.ResetColor();

                        lenght = lenght.Replace(".", ",");
                        width = width.Replace(".", ",");

                        _banknotesRepository.Add(new Banknotes { Symbol = symbol, Name = name, Denomination = float.Parse(denomination), Currency = currency, YearOfRelease = float.Parse(yearOfRelease), Length = float.Parse(lenght), Width = float.Parse(width), Watermark = watermark });
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "3")
                {
                    ShowMenu();

                    ShowRepo(_coinsRepository);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPodaj numer Id monety, którą chcesz usunąć z kolekcji:\n");

                    try
                    {
                        Console.ResetColor(); var symbol = GetDataFromUser("ID: ");
                        _coinsRepository.Remove(_coinsRepository.GetById((int)float.Parse(symbol)));
                    }
                    catch (Exception e)
                    {
                        ShowBug("Wprowadzono złą wartość identyfikatora Id.\n");
                    }
                }
                else if (choice == "4")
                {
                    ShowMenu();

                    ShowRepo(_banknotesRepository);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPodaj numer Id banknotu, który chcesz usunąć z kolekcji:\n");

                    try
                    {
                        Console.ResetColor(); var symbol = GetDataFromUser("ID: ");
                        _banknotesRepository.Remove(_banknotesRepository.GetById((int)float.Parse(symbol)));
                    }
                    catch (Exception e)
                    {
                        ShowBug("Wprowadzono złą wartość identyfikatora Id.\n");
                    }
                }
                else if (choice == "5")
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\nZawartość Twojej kolekcji:\n\n");
                        Console.ResetColor();
                        WriteAllToConsole(_coinsRepository);
                        WriteAllToConsole(_banknotesRepository);
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "6")
                {
                    ShowMenu();
                    try
                    {
                        if (File.Exists(auditFileName))
                        {
                            using (var reader = File.OpenText(auditFileName))
                            {
                                Console.ForegroundColor = ConsoleColor.Blue;
                                Console.Write("\nZawartość pliku logów audytu:\n\n");
                                Console.ForegroundColor = ConsoleColor.DarkYellow;
                                Console.WriteLine("\t{0,-20} {1,-20} {2,-20}", "Data", "Operacja", "Obiekt");
                                Console.WriteLine(("\t").PadRight(100, '-'));
                                Console.ResetColor();

                                var line = reader.ReadLine();
                                while (line != null)
                                {
                                    var record = line.Split(';');
                                    Console.WriteLine("\t{0,-20} {1,-20} {2,-20}", record[0], record[1], record[2]);
                                    line = reader.ReadLine();
                                }
                            }
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "7")
                {
                    ShowMenu();
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nNajniższa wartość monety w Twojej kolekcji wynosi:\n");
                        Console.ResetColor();
                        var value = _numismaticsProvider.GetMinDenominationOfAllNumismatics();
                        Console.WriteLine($" {value}\n");
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "8")
                {
                    ShowMenu();
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine("\nW Twojej kolekcji występują następujące waluty:\n");
                        Console.ResetColor();

                        foreach (var currency in _numismaticsProvider.GetUniqueCurrency())
                        {
                            Console.WriteLine(currency);
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "9")
                {
                    ShowMenu();
                    try
                    {
                        var parameter = GetDataFromUser("Podaj minimalną wartość monety: ");
                        Console.ResetColor();

                        parameter = parameter?.Replace(".", ",");

                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.WriteLine($"\nZawartość Twojej kolekcji monet o wartości większej lub równej {parameter}:\n\n");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", "Id", "Symbol", "Nazwa", "Nominał", "Waluta", "Rok wyd.");
                        Console.WriteLine(("\t").PadRight(83, '-'));
                        Console.ResetColor();

                        foreach (var coins in _numismaticsProvider.FilterNumismatics(float.Parse(parameter)))
                        {
                            Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", coins.Id, coins.Symbol, coins.Name, coins.Denomination, coins.Currency, coins.YearOfRelease);
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "10")
                {
                    ShowMenu();
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\nZawartość Twojej kolekcji monet posortowanej rosnąco wg wartości oraz waluty:\n\n");
                        Console.ForegroundColor = ConsoleColor.DarkYellow;
                        Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", "Id", "Symbol", "Nazwa", "Nominał", "Waluta", "Rok wyd.");
                        Console.WriteLine(("\t").PadRight(83, '-'));
                        Console.ResetColor();

                        foreach (var coins in _numismaticsProvider.OrderByCurrencyAndDenomination())
                        {
                            //Console.WriteLine(coins);
                            Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", coins.Id, coins.Symbol, coins.Name, coins.Denomination, coins.Currency, coins.YearOfRelease);
                        }
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "X" || choice == "x")
                {
                    _coinsRepository.Save();
                    _banknotesRepository.Save();
                    Environment.Exit(0);
                }
                else
                {
                    ShowBug("Wprowadzono złą wartość.\n");
                }
            }
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("               Witamy w programie Kolekcjoner 'Generic'");
            Console.WriteLine("                   (Dependency Injection + InFile)");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=========================================================================");
            Console.ResetColor();
            Console.WriteLine("   1. Dodawanie monety do kolekcji");
            Console.WriteLine("   2. Dodawanie banknotu do kolekcji");
            Console.WriteLine("   3. Usunięcie monety z kolekcji");
            Console.WriteLine("   4. Usunięcie banknotu z kolekcji");
            Console.WriteLine("   5. Wyświetl zasób kolekcji (repozytorium generyczne)");
            Console.WriteLine("   6. Wyświetl zawartość plik audytu");
            Console.WriteLine("   7. Najniższa wartość monety w Twojej kolekcji");
            Console.WriteLine("   8. Wykaz walut występujących w kolekcji");
            Console.WriteLine("   9. Wykaz kolekcji monet o wartości równej lub wyższej niż zadana");
            Console.WriteLine("   10. Wykaz kolekcji monet posortowanej wg wartości oraz waluty");
            Console.WriteLine("   X. Zakończ pracę programu");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("=========================================================================");
            Console.ResetColor();
        }
    }
}
