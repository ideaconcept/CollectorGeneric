using CollectorGeneric.DataProviders;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CollectorGeneric
{
    public class App : IApp
    {
        //private readonly IRepository<Numismatics> _numismaticsRepository;
        //private readonly IRepository<Coins> _coinsRepository;
        //private readonly IRepository<Banknotes> _banknotesRepository;
        //private readonly INumismaticsProvider _numismaticsProvider;
        private readonly IUserCommunication _userCommunication;
        private readonly IEventHandlerService _eventHandlerService;

        public App(
            //IRepository<Numismatics> numismaticsRepository,
            //IRepository<Coins> coinsRepository,
            //IRepository<Banknotes> banknotesRepository,
            //INumismaticsProvider numismaticsProvider,
            IUserCommunication userCommunication,
            IEventHandlerService eventHandlerService)
        {
            //_numismaticsRepository = numismaticsRepository;
            //_coinsRepository = coinsRepository;
            //_banknotesRepository = banknotesRepository;
            //_numismaticsProvider = numismaticsProvider;
            _userCommunication = userCommunication;
            _eventHandlerService = eventHandlerService;

        }

        public void Run()
        {
            _eventHandlerService.ListenForEvents();
            _userCommunication.SelectAMenuOption();

            //    var lista = AddCoins();
            //    foreach (var coin in lista)
            //    {
            //        _coinsRepository.Add(coin);            
            //    }

            //    //lista = _numismaticsProvider.OrderByDenominationAndNameDescending();

            //    foreach (var coins in _numismaticsProvider.FilterNumismatics(10))
            //    {
            //        Console.WriteLine(coins);
            //    }

            //    var wartosc = _numismaticsProvider.GetMinDenominationOfAllNumismatics();
            //    Console.WriteLine($"\nNajniższa wartość monety: {wartosc}\n");


            //    var lista2 = AddCoins();
            //    foreach (var coin in lista)
            //    {
            //        _coinsRepository.Add(coin);
            //    }

            //    foreach (var waluta in _numismaticsProvider.GetUniqueCurrency())
            //    {
            //        Console.WriteLine(waluta);
            //    }
            //}

            //public static List<Coins> AddCoins()
            //{
            //    return new List<Coins>
            //    {
            //    new Coins {
            //        Id = 1,
            //        Material = "AG 879",
            //        Diameter = 13.4f,
            //        Weight = 10.1f,
            //        Symbol = "A01",
            //        Name = "Moneta 1",
            //        Denomination = 20,
            //        Currency = "Złotych",
            //        YearOfRelease = 1997,
            //        },
            //    new Coins {
            //        Id = 2,
            //        Material = "AG 900",
            //        Diameter = 15.8f,
            //        Weight = 13.1f,
            //        Symbol = "A02",
            //        Name = "Moneta 2",
            //        Denomination = 50,
            //        Currency = "Złotych",
            //        YearOfRelease = 2014,
            //        },
            //    new Coins {
            //        Id = 3,
            //        Material = "Au 999",
            //        Diameter = 20.1f,
            //        Weight = 15.5f,
            //        Symbol = "A03",
            //        Name = "Moneta 3",
            //        Denomination = 100,
            //        Currency = "Guldenów",
            //        YearOfRelease = 2022,
            //        },
            //    new Coins {
            //        Id = 4,
            //        Material = "Al",
            //        Diameter = 9.99f,
            //        Weight = 5.5f,
            //        Symbol = "A04",
            //        Name = "Moneta 5",
            //        Denomination = 10,
            //        Currency = "Guldenów",
            //        YearOfRelease = 1976,
            //        }
            //    };
        }
    }
}
