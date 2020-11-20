using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class NodeAttributesViewModel : ViewModelBase
    {

        private string _displayName;

        public string DisplayName
        {
            get
            {
                return _displayName;
            }
            set
            {
                _displayName = value;
                OnPropertyChanged(nameof(DisplayName));
            }
        }

        private string _value;
        public string Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = value;
                OnPropertyChanged(nameof(Value));
            }
        }

        private string _serverTimeStamp;
        public string ServerTimeStamp
        {
            get
            {
                return _serverTimeStamp;
            }
            set
            {
                _serverTimeStamp = value;
                OnPropertyChanged(nameof(ServerTimeStamp));
            }
        }

        public void setAttributes(string displayName, string value, string serverTimeStamp)
        {
            DisplayName = displayName;
            Value = value;
            ServerTimeStamp = serverTimeStamp;
        }
    }
}
