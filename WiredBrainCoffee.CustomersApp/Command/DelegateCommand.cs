using System.CodeDom;
using System.Windows.Input;

namespace WiredBrainCoffee.CustomersApp.Command
{
    public class DelegateCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Func<object, bool>? _canExecute;

        //the ctor of the delegate command class uses action delegate for execution of commands --> Execute()
        //For any return values use the delegate func with parameter and return type --> canExecute()
        //both take object paramter as the Execute() and canExecute()
        //canexecute parameter can be null as all ui commands may not use the canExecute()
        public DelegateCommand(Action<object> execute, Func<object, bool>? canExecute = null)
        {
            ArgumentNullException.ThrowIfNull(execute);
            _execute = execute;
            _canExecute = canExecute;
        }
        //checks if command can be executed every time it is raised
        public event EventHandler? CanExecuteChanged;
        
        //checks if CanExecuteChanged event is raised
        public void RaiseCanExecuteChanged()
        {
            //invokes the event if it is not null
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }
        //checks if command execution is enabled every time canExecuteChanged event is raised
        public bool CanExecute(object? parameter)
        {
            //return true if _canExecute is null (means the command does not need a check if it is enabled)
            //return the status of the command if otherwise
           return _canExecute is null || _canExecute(parameter);
         }

        //executes the command in UI after invoking canExecute()
        public void Execute(object? parameter)
        {
            //call the execute delegate with the object parameter 
            _execute(parameter);
        }

    }
}
