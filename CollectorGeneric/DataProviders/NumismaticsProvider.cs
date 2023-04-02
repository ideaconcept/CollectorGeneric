using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;
using System.Data.Common;
using System.Text;

namespace CollectorGeneric.DataProviders
{
    public class NumismaticsProvider : INumismaticsProvider
    {
        private readonly IRepository<Coins> _coinsRepository;

        public NumismaticsProvider(IRepository<Coins> coinsRepository)
        {
            _coinsRepository = coinsRepository;
        }

        public string AnonymousClass()
        {
            var coins = _coinsRepository.GetAll();

            var list = coins.Select(coins => new
            {
                Identyfikator = coins.Id,
                Oznaczenie = coins.Symbol,
                Nazwa = coins.Name
            });

            StringBuilder sb = new (2048);
            foreach (var coin in list)
            {
                sb.AppendLine($"Identyfikator monety: {coin.Identyfikator}");
                sb.AppendLine($"     Symbol: {coin.Oznaczenie}");
                sb.AppendLine($"     Nazwa: {coin.Nazwa}");
            }

            return sb.ToString();
        }

        public List<Coins> FilterNumismatics(float minimalDenomination)
        {
            var coins = _coinsRepository.GetAll();
            var list = new List<Coins>();
            list = coins.Where(x => x.Denomination >= minimalDenomination).ToList();
            return list;
        }

        public float GetMinDenominationOfAllNumismatics()
        {
            var coins = _coinsRepository.GetAll();
            return coins.Select(x => x.Denomination).Min();
        }

        public List<Coins> GetSpecyficColumns()
        {
            var coins = _coinsRepository.GetAll();
            var list = coins.Select(coins => new Coins
                {
                    Id = coins.Id,
                    Symbol = coins.Symbol,
                    Name = coins.Name,
                }).ToList();
            
            return list;
        }

        public List<string> GetUniqueCurrency()
        {
            var coins = _coinsRepository.GetAll();
            return coins.Select(x => x.Currency).Distinct().ToList();
        }

        public List<Coins> OrderByCurrencyAndDenomination()
        {
            var coins = _coinsRepository.GetAll();
            return coins
                .OrderBy(x => x.Currency)
                .ThenBy(x => x.Denomination)
                .ToList();
        }

        public List<Coins> OrderByDenominationAndNameDescending()
        {
            var coins = _coinsRepository.GetAll();
            return coins
                .OrderByDescending(x => x.Currency)
                .ThenByDescending(x => x.Denomination)
                .ToList();
        }

        public List<Coins> OrderByName()
        {
            var coins = _coinsRepository.GetAll();
            return coins.OrderBy(x => x.Name).ToList();
        }

        public List<Coins> OrderByNameDescending()
        {
            var coins = _coinsRepository.GetAll();
            return coins.OrderByDescending(x => x.Name).ToList();
        }

        public List<Coins> WhereCurrency(string currency)
        {
            throw new NotImplementedException();
        }

        public List<Coins> WhereStartsWith(string prefix)
        {
            var coins = _coinsRepository.GetAll();
            return coins.Where(x => x.Name.StartsWith(prefix)).ToList();
        }

        public List<Coins> WhereStartsWithAndDenominationIsGreaterThan(string prefix, float denomination)
        {
            var coins = _coinsRepository.GetAll();
            return coins.Where(x => x.Name.StartsWith(prefix) && x.Denomination >= denomination).ToList();
        }
    }
}
