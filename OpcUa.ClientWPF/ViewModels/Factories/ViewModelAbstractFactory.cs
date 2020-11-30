using OpcUa.ClientWPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class ViewModelAbstractFactory : IViewModelAbstractFactory
    {
        private readonly IViewModelFactory<ReadViewModel> _readViewModelFactory;
        private readonly IViewModelFactory<WriteViewModel> _writeViewModelFactory;
        private readonly IViewModelFactory<CallViewModel> _callViewModelFactory;
        private readonly IViewModelFactory<SubscribeViewModel> _subscribeViewModelFactory;

        public ViewModelAbstractFactory(
            IViewModelFactory<ReadViewModel> readViewModelFactory,
            IViewModelFactory<WriteViewModel> writeViewModelFactory, 
            IViewModelFactory<CallViewModel> callViewModelFactory, 
            IViewModelFactory<SubscribeViewModel> subscribeViewModelFactory)
        {
            _readViewModelFactory = readViewModelFactory;
            _writeViewModelFactory = writeViewModelFactory;
            _callViewModelFactory = callViewModelFactory;
            _subscribeViewModelFactory = subscribeViewModelFactory;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Read:
                    return _readViewModelFactory.CreateViewModel();
                case ViewType.Write:
                    return _writeViewModelFactory.CreateViewModel();
                case ViewType.Call:
                    return _callViewModelFactory.CreateViewModel();
                case ViewType.Subscribe:
                    return _subscribeViewModelFactory.CreateViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a view model.", "viewType");
            }
        }
    }
}
