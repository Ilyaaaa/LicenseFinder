using System;
using System.Windows.Input;

namespace LicenseFinder.WPF
{
    public class Command : ICommand
    {
        #region Properties

        private Action _action;
        private Func<bool> _canExecute;

        #endregion

        #region Constructors

        public Command(Action action, Func<bool> canExecute)
        {
            _action = action;
            _canExecute = canExecute;
        }

        #endregion

        #region Public functions

        public bool CanExecute(object parameter)
        {
            return _canExecute.Invoke();
        }

        public void Execute(object parameter)
        {
            _action();
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        #endregion
    }
}
