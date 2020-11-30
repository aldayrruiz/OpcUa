using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class WriteViewModelFactory : IViewModelFactory<WriteViewModel>
    {
        private readonly IClientStore _clientStore;

        public WriteViewModelFactory(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        public ViewModelBase CreateViewModel()
        {
            return new WriteViewModel(_clientStore);
        }
    }
}
