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
            var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());
            numismaticsRepository.ItemAdded += RepositoryOnItemAdded;
            numismaticsRepository.ItemRemove += RepositoryOnItemRemove;

            static void RepositoryOnItemAdded(object? sender, Numismatics e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nDodano numizmat: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} from {sender?.GetType().Name}");
                Console.ResetColor();
                SaveLogToFile($"[{System.DateTime.Now}]; NumismaticsAdded; [{e.Symbol} {e.Name} {e.Denomination} {e.Currency}]");
            }

            static void RepositoryOnItemRemove(object? sender, Numismatics e)
            {
                Console.ForegroundColor = ConsoleColor.DarkRed;
                Console.WriteLine($"\nUsunięto numizmat: {e.Symbol} {e.Name} {e.Denomination} {e.Currency} from {sender?.GetType().Name}");
                Console.ResetColor();
                SaveLogToFile($"[{System.DateTime.Now}]; NumismaticsRemove; [{e.Symbol} {e.Name} {e.Denomination} {e.Currency}]");
            }

            ShowMenu();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nWybierz 1, 2, 3, 4, 5, 6 lub X aby zakończyć pracę programu. Twój wybór: ");
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
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nZawartość Twojej kolekcji:\n");
                    Console.ResetColor();
                    WriteAllToConsole(numismaticsRepository);
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nPodaj numer Id numizmatu, który chcesz usunąć z kolekcji:\n");

                    try
                    {
                        Console.ResetColor();var symbol = GetDataFromUser("ID: ");
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
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nW Twojej kolekcji 'generycznej' znajdują się następujące numizmaty:\n");
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
                    try
                    {
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "6")
                {
                    try
                    {
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
            Console.WriteLine("          Witamy w programie Kolekcjoner 'Generic' (InMemory):");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
            Console.WriteLine("   1. Dodawanie monety do kolekcji");
            Console.WriteLine("   2. Dodawanie banknotu do kolekcji");
            Console.WriteLine("   3. Usunięcie numizmatu z kolekcji");
            Console.WriteLine("   4. Wyświetl zasób kolekcji (repozytorium generyczne)");
            Console.WriteLine("   5. Wyświetl zasób kolekcji (serializacja)");
            Console.WriteLine("   6. Wyświetl zaartość plik audytu");
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


