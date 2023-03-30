using CollectorGeneric.Data;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric
{
    internal class Program
    {
        private const string fileName = "auditLog.txt";

        private static void Main()
        {
            //var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());
            var numismaticsRepository = new FileRepository<Numismatics>();
            numismaticsRepository.LoadRepository();
            numismaticsRepository.ItemAdded += RepositoryOnItemAdded;
            numismaticsRepository.ItemRemove += RepositoryOnItemRemove;

            static void RepositoryOnItemAdded(object? sender, Numismatics e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nDodano numizmat: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} from {sender?.GetType().Name}");
                Console.ResetColor();
                SaveLogToFile($"{System.DateTime.Now};NumismaticsAdded;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}");
            }

            static void RepositoryOnItemRemove(object? sender, Numismatics e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nUsunięto numizmat: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} from {sender?.GetType().Name}");
                Console.ResetColor();
                SaveLogToFile($"{System.DateTime.Now};NumismaticsRemove;{e.Symbol}, {e.Name}, {e.Denomination}, {e.Currency}");
            }

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

                        numismaticsRepository.Add(new Coins { Symbol = symbol, Name = name, Denomination = float.Parse(denomination), Currency = currency, YearOfRelease = float.Parse(yearOfRelease), Material = material, Diameter = float.Parse(diameter), Weight = float.Parse(weight) });
                        numismaticsRepository.Save();
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

                        numismaticsRepository.Add(new Banknotes { Symbol = symbol, Name = name, Denomination = float.Parse(denomination), Currency = currency, YearOfRelease = float.Parse(yearOfRelease), Length = float.Parse(lenght), Width = float.Parse(width), Watermark = watermark });
                        numismaticsRepository.Save();
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "3")
                {
                    ShowMenu();
                    ShowRepo(numismaticsRepository);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPodaj numer Id numizmatu, który chcesz usunąć z kolekcji:\n");

                    try
                    {
                        Console.ResetColor(); var symbol = GetDataFromUser("ID: ");
                        numismaticsRepository.Remove(numismaticsRepository.GetById((int)float.Parse(symbol)));
                        numismaticsRepository.Save();
                    }
                    catch (Exception e)
                    {
                        ShowBug("Wprowadzono złą wartość identyfikatora Id.\n");
                    }
                }
                else if (choice == "4")
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Blue;
                        Console.Write("\nZawartość Twojej kolekcji:\n\n");
                        Console.ResetColor();
                        WriteAllToConsole(numismaticsRepository);
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "5")
                {
                    ShowMenu();
                    try
                    {
                        if (File.Exists(Program.fileName))
                        {
                            using (var reader = File.OpenText(Program.fileName))
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
                else if (choice == "X" || choice == "x")
                {
                    Environment.Exit(0);
                }
                else
                {
                    ShowBug("Wprowadzono złą wartość.\n");
                }
            }
        }

        static void WriteAllToConsole(IReadRepository<IEntity> repository)
        {
            var items = repository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine(item);
            }
        }

        static void ShowRepo(IReadRepository<Numismatics> repository)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.Write("\nZawartość Twojej kolekcji:\n\n");
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", "Id", "Symbol", "Nazwa", "Nominał", "Waluta", "Rok wyd.", "Średnica", "Waga (g)", "Materiał");
            Console.WriteLine(("\t").PadRight(83, '-'));
            Console.ResetColor();

            var items = repository.GetAll();
            foreach (var item in items)
            {
                Console.WriteLine("\t{0,-4} {1,-11} {2,-35} {3,7} {4,-10} {5,8}", item.Id, item.Symbol, item.Name, item.Denomination, item.Currency, item.YearOfRelease);

                //<Coins> -> <Numismatics> + 
                //material "Materiał"
                //diameter "Średnica"
                //weight "Waga"

                //<Banknotes>  -> <Numismatics> +
                //lenght "Długość"
                //width "Wysokość"
                //watermark "Znak wodny"
            }
        }

        private static void ShowBug(string bug)
        {
            Console.ForegroundColor = ConsoleColor.Red;
            Console.WriteLine($"\nWystąpił błąd: {bug}");
            Console.ResetColor();
        }

        private static void ShowMenu()
        {
            Console.Clear();
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("          Witamy w programie Kolekcjoner 'Generic' (InFile):");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
            Console.WriteLine("   1. Dodawanie monety do kolekcji");
            Console.WriteLine("   2. Dodawanie banknotu do kolekcji");
            Console.WriteLine("   3. Usunięcie numizmatu z kolekcji");
            Console.WriteLine("   4. Wyświetl zasób kolekcji (repozytorium generyczne)");
            Console.WriteLine("   5. Wyświetl zawartość plik audytu");
            Console.WriteLine("   X. Zakończ pracę programu");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
        }

        private static string? GetDataFromUser(string message)
        {
            Console.ForegroundColor = ConsoleColor.Yellow;
            Console.Write(message);
            Console.ForegroundColor = ConsoleColor.Gray;
            string userInput = Console.ReadLine();
            return userInput;
        }

        private static void SaveLogToFile(string auditLog)
        {
            using (var writer = File.AppendText(fileName))
            {
                writer.WriteLine(auditLog);
            }
        }
    }
}


