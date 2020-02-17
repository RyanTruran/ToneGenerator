using System;
using System.Windows.Input;
using ToneGenerator.Models;
using ToneGenerator.ViewModels;

namespace ToneGenerator.Commands
{
    internal class ButtonPressCommand : ICommand
    {
        private ConnectionViewModel _ViewModel;

        public ButtonPressCommand(ConnectionViewModel viewModel)
        {
            _ViewModel = viewModel;
        }
        /// <summary>
        /// Verifies that the button press can still be executed prior to execution.
        /// </summary>
        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        //
        public bool CanExecute(object parameter)
        {
            //only allow button press if a connection has been established.
            return _ViewModel.Connected;
        }

        public void Execute(object parameter)
        {
            //Gets the Content from the button, to play as a tone, then passes it to the SendNote Command
            _ViewModel.SendNote(parameter.ToString());
        }
    }
}