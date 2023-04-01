
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;

namespace CollectorGeneric
{
     
    public class App : IApp
    {
        public readonly IRepository<Coins> _coinsRepository;
        public readonly IRepository<Banknotes> _banknotesRepository;

        public App(IRepository<Coins> coinsRepository)
        {
            _coinsRepository = coinsRepository;
        }
        
        public App(IRepository<Banknotes> banknotesRepository)
        {
            _banknotesRepository = banknotesRepository;
        }

        public void Run()
        {
            Console.WriteLine("I'm here in Run() metod.");
        }
    }
}
