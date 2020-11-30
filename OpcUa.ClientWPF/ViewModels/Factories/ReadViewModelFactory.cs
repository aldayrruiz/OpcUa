using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class ReadViewModelFactory : IViewModelFactory<ReadViewModel>
    {
        private readonly IClientStore _clientStore;

        public ReadViewModelFactory(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        public ViewModelBase CreateViewModel()
        {
            return new ReadViewModel(_clientStore);
        }
    }
}
