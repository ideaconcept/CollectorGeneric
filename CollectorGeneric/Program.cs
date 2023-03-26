using CollectorGeneric.Data;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;
using System.Xml.Linq;

var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());

AddCoins(numismaticsRepository);
AddBanknotes(numismaticsRepository);
WriteAllToConsole(numismaticsRepository);


static void AddCoins(IWriteRepository<Coins> coinsRepository)
{
    coinsRepository.Add(new Coins { Symbol = "Symbol1", Name = "Nazwa 1", Denomination = 10.00f, Currency = "Złotych", YearOfRelease = 2021, Material = "Złoto 998", Diameter = 14.14f, Weight = 32.0f });
    coinsRepository.Save();
}

static void AddBanknotes(IWriteRepository<Banknotes> banknotesRepository)
{
    banknotesRepository.Add(new Banknotes { Symbol = "Symbol2", Name = "Nazwa 2", Denomination = 12.00f, Currency = "Złotych", YearOfRelease = 1998, Length = 15, Width = 7.5f, Watermark = "Znak wodny"});


    banknotesRepository.Save();
}

static void WriteAllToConsole(IReadRepository<IEntity> repository)
{
    var items = repository.GetAll();
    foreach (var item in items)
    {
        Console.WriteLine(item);
    }
}