using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class SubscribeViewModelFactory : IViewModelFactory<SubscribeViewModel>
    {
        private readonly IClientStore _clientStore;

        public SubscribeViewModelFactory(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        public ViewModelBase CreateViewModel()
        {
            return new SubscribeViewModel(_clientStore);
        }
    }
}
