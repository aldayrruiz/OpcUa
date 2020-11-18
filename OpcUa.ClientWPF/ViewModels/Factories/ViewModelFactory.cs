using OpcUa.ClientWPF.State.Navigators;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels.Factories
{
    public class ViewModelFactory : IViewModelFactory
    {
        private readonly CreateViewModel<HomeViewModel> _createHomeViewModel;
        private readonly CreateViewModel<ReadViewModel> _createReadViewModel;
        private readonly CreateViewModel<WriteViewModel> _createWriteViewModel;
        private readonly CreateViewModel<CallViewModel> _createCallViewModel;
        private readonly CreateViewModel<SubscribeViewModel> _createSubscribeViewModel;

        public ViewModelFactory(
            CreateViewModel<HomeViewModel> createHomeViewModel, 
            CreateViewModel<ReadViewModel> createReadViewModel, 
            CreateViewModel<WriteViewModel> createWriteViewModel, 
            CreateViewModel<CallViewModel> createCallViewModel, 
            CreateViewModel<SubscribeViewModel> createSubscribeViewModel)
        {
            _createHomeViewModel = createHomeViewModel;
            _createReadViewModel = createReadViewModel;
            _createWriteViewModel = createWriteViewModel;
            _createCallViewModel = createCallViewModel;
            _createSubscribeViewModel = createSubscribeViewModel;
        }

        public ViewModelBase CreateViewModel(ViewType viewType)
        {
            switch (viewType)
            {
                case ViewType.Home:
                    return _createHomeViewModel();
                case ViewType.Read:
                    return _createReadViewModel();
                case ViewType.Write:
                    return _createWriteViewModel();
                case ViewType.Call:
                    return _createCallViewModel();
                case ViewType.Subscribe:
                    return _createSubscribeViewModel();
                default:
                    throw new ArgumentException("The ViewType does not have a ViewModel.", "viewType");
            }
        }
    }
}
