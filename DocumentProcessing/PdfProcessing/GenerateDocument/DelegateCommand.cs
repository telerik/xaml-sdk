using System;
using System.Windows.Input;

namespace GenerateDocument
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> executeAction;

        public DelegateCommand(Action<object> executeAction)
        {
            this.executeAction = executeAction;
        }

        public bool CanExecute(object parameter)
        {
            return true;
        }

        public event EventHandler CanExecuteChanged;

        public void Execute(object parameter)
        {
            this.executeAction(parameter);
        }
    }
}
