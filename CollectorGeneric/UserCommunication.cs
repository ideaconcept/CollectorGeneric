using CollectorGeneric.DataProviders;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric
{
    public class UserCommunication : UserCommunicationBase, IUserCommunication
    {
        private readonly IRepository<Numismatics> _numismaticsRepository;
        private readonly IRepository<Coins> _coinsRepository;
        private readonly IRepository<Banknotes> _banknotesRepository;
        private readonly INumismaticsProvider _numismaticsProvider;

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
        }

        public void SelectAMenuOption()
        {
            bool CloseApp = false;
            Console.WriteLine("Jestem tutaj :-)");

        }
    }
}
