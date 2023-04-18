using CollectorGeneric.Components.DataProviders;
using CollectorGeneric.Data.Entities;
using CollectorGeneric.Data.Repositories;
using System.Drawing;
using System.Linq;
using System.Text;

namespace CollectorGeneric
{
    public class App : IApp
    {
        private readonly IUserCommunication _userCommunication;
        private readonly IEventHandlerService _eventHandlerService;

        public App(
            IUserCommunication userCommunication,
            IEventHandlerService eventHandlerService)
        {
            _userCommunication = userCommunication;
            _eventHandlerService = eventHandlerService;
        }

        public void Run()
        {
            _eventHandlerService.ListenForEvents();
            _userCommunication.SelectAMenuOption();
        }
    }
}