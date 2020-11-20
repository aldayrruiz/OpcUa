using System;
using System.Collections.Generic;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public enum Operation
    {
        ADDITTION, SUBTRACTION, DIVISION, MULTIPLICATION
    }

    public class CallViewModel : ViewModelBase
    {

        private string _nodeId;
        public string NodeId
        {
            get
            {
                return _nodeId;
            }
            set
            {
                _nodeId = value;
                OnPropertyChanged(nameof(NodeId));
            }

        }

        // Input variables
        private int _x;
        public int X
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

        private int _y;
        public int Y
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

        public Operation operation;



        
    }

    
}
