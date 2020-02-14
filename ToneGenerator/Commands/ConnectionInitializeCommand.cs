using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using ToneGenerator.ViewModels;

namespace ToneGenerator.Commands
{
    internal class ConnectionInitializeCommand : ICommand
    {
        private ConnectionViewModel _ViewModel;

        public ConnectionInitializeCommand(ConnectionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public void Execute(object parameter)
        {
            _ViewModel.createObject();

        }
    }
}