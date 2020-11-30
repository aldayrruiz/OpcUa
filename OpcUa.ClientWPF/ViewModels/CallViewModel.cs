using OpcUa.ClientWPF.Commands;
using OpcUa.ClientWPF.State.Clients;
using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class CallViewModel : ViewModelBase
    {
        public CallMethodNodeCommand CallMethodNodeCommand { get; set; }

        public CallViewModel(IClientStore clientStore)
        {
            Address = "https://localhost/"; // TODO: Make this variable available to all views. One single Address?
            CallMethodNodeCommand = new CallMethodNodeCommand(this, clientStore);
        }

        // Input variables
        private double _x;
        public double X
        {
            get
            {
                return _x;
            }
            set
            {
                _x = value;
                OnPropertyChanged(nameof(X));
            }
        }

        private double _y;
        public double Y
        {
            get
            {
                return _y;
            }
            set
            {
                _y = value;
                OnPropertyChanged(nameof(Y));
            }
        }

        private string _address;
        public string Address
        {
            get
            {
                return _address;
            }
            set
            {
                _address = value;
                OnPropertyChanged(nameof(Address));
            }
        }

        public string _method;
        public string Method
        {
            get
            {
                return _method;
            }
            set
            {
                _method = value;
                OnPropertyChanged(nameof(Method));
            }
        }

        public string _result;
        public string Result
        {
            get
            {
                return _result;
            }
            set
            {
                _result = value;
                OnPropertyChanged(nameof(Result));
            }
        }

        public string _errorMessage;
        public string ErrorMessage
        {
            get
            {
                return _errorMessage;
            }
            set
            {
                _errorMessage = value;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }
    }
}
