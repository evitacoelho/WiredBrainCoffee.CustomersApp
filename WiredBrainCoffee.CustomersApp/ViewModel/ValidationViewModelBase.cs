using System.Collections;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class ValidationViewModelBase : ViewModelBase, INotifyDataErrorInfo
    {
        private readonly Dictionary<string, List<string>> _errorsByPropertyName = new();
        //returns a bool value based on erorrs detected
        public bool HasErrors => _errorsByPropertyName.Any();
        //binding to data
        public event EventHandler<DataErrorsChangedEventArgs>? ErrorsChanged;

        //returns a collection of errors
        public IEnumerable GetErrors(string? propertyName)
        {
            //if the property name has errors listed in the dictionary, return the list of errors otherwise return an empty string
            return propertyName is not null && _errorsByPropertyName.ContainsKey(propertyName)
            ? _errorsByPropertyName[propertyName]
             : Enumerable.Empty<string>();


        }
        protected virtual void OnErrorsChanged(DataErrorsChangedEventArgs e)
        {
            ErrorsChanged?.Invoke(this, e);
        }


        //add method to add errors
        protected void AddError(string error,
            [CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (!_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName[propertyName] = new List<string>();
            }
            if (!_errorsByPropertyName[propertyName].Contains(error))
            {
                _errorsByPropertyName[propertyName].Add(error);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }


        //remove errors
        protected void ClearErrors([CallerMemberName] string? propertyName = null)
        {
            if (propertyName is null) return;

            if (_errorsByPropertyName.ContainsKey(propertyName))
            {
                _errorsByPropertyName.Remove(propertyName);
                OnErrorsChanged(new DataErrorsChangedEventArgs(propertyName));
                RaisePropertyChanged(nameof(HasErrors));
            }
        }

    }
}
