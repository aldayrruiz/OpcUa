using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;

namespace OpcUa.ClientWPF.ViewModels
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyPropertyChanging
    {
        protected ViewModelBase()
            : base()
        {
        }

        public event PropertyChangedEventHandler PropertyChanged;
        public event PropertyChangingEventHandler PropertyChanging;

        protected virtual void OnPropertyChanging(string propertyName)
        {
            PropertyChanging?.Invoke(this, new PropertyChangingEventArgs(propertyName));
        }

        protected void OnPropertyChanged(string propertyName)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

    }
}
