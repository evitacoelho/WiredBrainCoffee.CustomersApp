using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class ViewModelBase : INotifyPropertyChanged
    {
        //property changed event handler implemented in the INotify interface
        //used to notify when a property is changed using setter methods
        public event PropertyChangedEventHandler? PropertyChanged;
       
        //helper method to notify property change
        //param: name of the changed property
        //use Callermember attribute when no param is passed
        //can be overridden in the base class
       protected virtual void RaisePropertyChanged([CallerMemberNameAttribute] string? propertyName = null)
        {
            //checks if propertychanged event is fired
            //invokes property change event handler with the property name that is changed
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        public virtual Task LoadAsync() =>  Task.CompletedTask;
        
    }
}
