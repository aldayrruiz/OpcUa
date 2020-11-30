using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class SubscribeViewModelFactory : IViewModelFactory<SubscribeViewModel>
    {
        public ViewModelBase CreateViewModel()
        {
            return new SubscribeViewModel();
        }
    }
}
