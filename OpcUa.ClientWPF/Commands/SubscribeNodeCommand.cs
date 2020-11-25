using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Input;
using Opc.UaFx.Client;
using OpcUa.ClientWPF.ViewModels;

namespace OpcUa.ClientWPF.Commands
{
    public class SubscribeNodeCommand : ICommand
    {
        private readonly Func<object, bool> canExecute;
        private readonly Action<object> execute;


        public SubscribeNodeCommand(Action<object> execute)
            : base()
        {
            this.execute = execute;
        }

        public SubscribeNodeCommand(Action<object> execute, Func<object, bool> canExecute)
            : this(execute)
        {
            this.canExecute = canExecute;
        }


        public event EventHandler CanExecuteChanged;
        public bool CanExecute(object parameter)
        {
            return this.canExecute(parameter);
        }

        public void Execute(object parameter)
        {
            execute(parameter);
        }

        public void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
    }
}
