using CollectorGeneric.Entities;

namespace CollectorGeneric.DataProviders.Extensions
{
    public static class CoinsHelper
    {
        public static IEnumerable<Coins> ByCurrency(this IEnumerable<Coins> query, string currency)
        {
            return query.Where(x => x.Currency == currency);
        }
    }
}
