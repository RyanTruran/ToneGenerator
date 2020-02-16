using System;
using System.Windows.Input;
using ToneGenerator.Models;
using ToneGenerator.ViewModels;

namespace ToneGenerator.Commands
{
    internal class ButtonPressCommand : ICommand
    {
        private ConnectionViewModel _ViewModel;
        private Connection _connection;

        public ButtonPressCommand(ConnectionViewModel viewModel)
        {
            _ViewModel = viewModel;
     //       _connection = connection;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            return _ViewModel.Connected;
        }

        public void Execute(object parameter)
        {
            _ViewModel.SendNote(parameter.ToString());
        }
    }
}