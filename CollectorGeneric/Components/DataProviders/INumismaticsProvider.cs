using CollectorGeneric.Data.Entities;
using CollectorGeneric.Data.Repositories;

namespace CollectorGeneric.Components.DataProviders
{
    public interface INumismaticsProvider
    {
        List<Coins> FilterNumismatics(float minimalDenomination);

        List<string> GetUniqueCurrency();

        float GetMinDenominationOfAllNumismatics();

        List<Coins> GetSpecyficColumns();

        string AnonymousClass();



        List<Coins> OrderByName();

        List<Coins> OrderByNameDescending();

        List<Coins> OrderByCurrencyAndDenomination();

        List<Coins> OrderByDenominationAndNameDescending();



        List<Coins> WhereStartsWith(string prefix);

        List<Coins> WhereStartsWithAndDenominationIsGreaterThan(string prefix, float denomination);

        List<Coins> WhereCurrencyIs(string currency);
    }
}
