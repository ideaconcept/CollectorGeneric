using CollectorGeneric.Data;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric
{
    internal class Program
    {
        static void CoinAdded(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDodano nową monetę.\n");
            Console.ResetColor();
        }
        static void BanknoteAdded(object sender, EventArgs args)
        {
            Console.ForegroundColor = ConsoleColor.Blue;
            Console.WriteLine("\nDodano nowy banknot.\n");
            Console.ResetColor();
        }

        private static void Main()
        {
            var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());

            ShowMenu();

            while (true)
            {
                Console.ForegroundColor = ConsoleColor.Yellow;
                Console.Write("\nWybierz 1, 2 lub X aby zakończyć pracę programu. Twój wybór: ");
                Console.ResetColor();

                Console.ForegroundColor = ConsoleColor.Green;
                string choice = Console.ReadLine();
                Console.ResetColor();

                if (choice == "1")
                {
                    ShowMenu();

                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("\nWprowadź dane dla monety:\n");
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
                        var weight = GetDataFromUser("Waga: ");
                        Console.ResetColor();

                        denomination = denomination.Replace(".", ",");
                        diameter = diameter.Replace(".", ",");
                        weight = weight.Replace(".", ",");

                        var zmienna = "Symbol: " + symbol + ", Name: " + name + ", Denominaton: " + denomination + ", Currency: " + currency + ", Year of release: " + yearOfRelease + ", Material: " + material + ", Diameter: " + diameter + ", Weight: " + weight;

                        coinsRepository.Add(new Coins { Symbol = symbol, Name = name, Denomination = denomination, Currency = currency, YearOfRelease = yearOfRelease, Material = material, Diameter = diameter, Weight = weight });
                        //AddCoins(numismaticsRepository);
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
                    Console.WriteLine("\nWprowadź dane dla banknotu:\n");
                    Console.ResetColor();

                    try
                    {
                        AddBanknotes(numismaticsRepository);
                    }
                    catch (Exception e)
                    {
                        ShowBug(e.Message);
                    }
                }
                else if (choice == "3")
                {
                    try
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.WriteLine("\nW Twojej kolekcji generycznej znajudują się następujące numizmaty:\n");
                        Console.ResetColor();
                        WriteAllToConsole(numismaticsRepository);
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

        static void AddNumizmatic(IRepository<Numismatics> numizmatocRepository)
        {
            numizmatocRepository.Add(new Numismatics { Symbol = "Symbol0", Name = "Nazwa 0", Denomination = 5.00f, Currency = "Złotych", YearOfRelease = 1970 });
            //numizmatocRepository.Add(new Numismatics { Symbol = "Symbol0", Name = "Nazwa 0", Denomination = 5.00f, Currency = "Złotych", YearOfRelease = 1970 });
            numizmatocRepository.Save();
        }

        static void AddCoins(IWriteRepository<Coins> coinsRepository)
        {
            coinsRepository.Add(new Coins { Symbol = "Symbol1", Name = "Nazwa 1", Denomination = 10.00f, Currency = "Złotych", YearOfRelease = 2021, Material = "Złoto 998", Diameter = 14.14f, Weight = 32.0f });
            coinsRepository.Save();
        }

        static void AddBanknotes(IWriteRepository<Banknotes> banknotesRepository)
        {
            banknotesRepository.Add(new Banknotes { Symbol = "Symbol2", Name = "Nazwa 2", Denomination = 20.00f, Currency = "Złotych", YearOfRelease = 1998, Length = 15, Width = 7.5f, Watermark = "Znak wodny" });
            banknotesRepository.Add(new Banknotes { Symbol = "Symbol3", Name = "Nazwa 3", Denomination = 50.00f, Currency = "Złotych", YearOfRelease = 2022, Length = 15, Width = 7.5f, Watermark = "Brak" });
            banknotesRepository.Save();
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
            Console.WriteLine("             Witamy w programie Kolekcjoner Generic:");
            Console.ForegroundColor = ConsoleColor.DarkGray;
            Console.WriteLine("====================================================================");
            Console.ResetColor();
            Console.WriteLine("   1. Dodawanie do kolekcji monety");
            Console.WriteLine("   2. Dodawanie do kolekcji banknotu");
            Console.WriteLine("   3. Wyświetl zasób kolekcji");
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
    }
}


