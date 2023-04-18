using CollectorGeneric;
using CollectorGeneric.Components.DataProviders;
using CollectorGeneric.Data.Entities;
using CollectorGeneric.Data.Repositories;
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