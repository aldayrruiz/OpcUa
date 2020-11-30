using OpcUa.ClientWPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public interface IViewModelAbstractFactory 
    {
        ViewModelBase CreateViewModel(ViewType viewType);
    }
}
