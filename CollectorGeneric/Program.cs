using CollectorGeneric;
using CollectorGeneric.DataProviders;
using CollectorGeneric.Entities;
using CollectorGeneric.Repositories;
using Microsoft.Extensions.DependencyInjection;

var services = new ServiceCollection();
services.AddSingleton<IApp, App>();
services.AddSingleton<IUserCommunication, UserCommunication>();
services.AddSingleton<IEventHandlerService, EventHandlerService>();

services.AddSingleton<IRepository<Numismatics>, ListRepository<Numismatics>>();
services.AddSingleton<IRepository<Coins>, ListRepository<Coins>>();
services.AddSingleton<IRepository<Banknotes>, ListRepository<Banknotes>>();
services.AddSingleton<INumismaticsProvider, NumismaticsProvider>();

var serviceProvider = services.BuildServiceProvider();
var app = serviceProvider.GetService<IApp>();

app.Run();




//using CollectorGeneric.Data;
//using CollectorGeneric.Entities;
//using CollectorGeneric.Repositories;

//namespace CollectorGeneric
//{
//    internal class Program
//    {
//        private const string auditFileName = "auditLog.txt";

//        private static void Main()
//        {
//            //var numismaticsRepository = new SqlRepository<Numismatics>(new CollectorGenericDbContext());
//            var coinsRepository = new FileRepository<Coins>();
//            var banknotesRepository = new FileRepository<Banknotes>();
//            coinsRepository.LoadRepository();
//            banknotesRepository.LoadRepository();
//        }
//    }
//}


