using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class CallViewModelFactory : IViewModelFactory<CallViewModel>
    {
        private readonly IClientStore _clientStore;

        public CallViewModelFactory(IClientStore clientStore)
        {
            _clientStore = clientStore;
        }

        public ViewModelBase CreateViewModel()
        {
            return new CallViewModel(_clientStore);
        }
    }
}
