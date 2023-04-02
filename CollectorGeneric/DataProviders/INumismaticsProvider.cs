﻿using CollectorGeneric.Entities;

namespace CollectorGeneric.DataProviders
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
        
        List<Coins> WhereCurrency(string currency); 
    }
}