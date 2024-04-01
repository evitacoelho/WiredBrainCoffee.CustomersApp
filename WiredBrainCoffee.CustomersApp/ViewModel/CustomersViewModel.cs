using System.Collections.ObjectModel;
using WiredBrainCoffee.CustomersApp.Command;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class CustomersViewModel:ViewModelBase
    {
        //provide data to the Customers property using a constructor that uses the data provider
        private readonly ICustomerDataProvider _customerDataProvider;
        private CustomerItemViewModel? _selectedCustomer;
        private NavigationSide _navigationSide;

        public CustomersViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
            //pass the Add() to delegateCommand 
            // generate a prop to be used in the view
            AddCommand = new DelegateCommand(Add);
            MoveNavigationCommand = new DelegateCommand(MoveNavigation);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        //create an observable collection of customers for the Customers View
        //observable collection will notify the data binding  when the collection changes
        //only having a get makes this property read only
        //initialize it to a new collection
        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        //full prop get set with notify change event on set
        public CustomerItemViewModel? SelectedCustomer 
        { 
            get => _selectedCustomer;
            set 
            {  
                _selectedCustomer = value; 
                RaisePropertyChanged();
                //raise event when if customer is selected or not
                RaisePropertyChanged(nameof(IsCustomerSelected)); 
                //to raise event change on the delegate command class
                DeleteCommand.RaiseCanExecuteChanged();
            }
        }

        public bool IsCustomerSelected => SelectedCustomer is not null;
        public NavigationSide NavigationSide
        {
            get => _navigationSide;
            private set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }
        public DelegateCommand AddCommand { get; }
        public DelegateCommand MoveNavigationCommand { get; }
        public DelegateCommand DeleteCommand { get; }

        //Call the Async method from the Data provider interface to load data for Customers prop for the view
        public async Task LoadAsync()
        {
            //if data is already loaded into property, do nothing
            if(Customers.Any())
            {
                return;
            }
            var customers = await _customerDataProvider.GetAllAsync();
            if(customers is not null)
            {
                foreach(var customer in customers)
                {
                    //once the data is populated add this to the observable collection
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        //Add method to implement the add button click
        //Adds a new customer to the observable collection of customers
        //match the signature of the delegate command by adding an object parameter
        private void Add(object? parameter)
        {
            //create a customer on button click add with some defined values
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);
            //add the customer to the observable collection of customers
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        //match the signature of the delegate command by adding an object parameter
        private void MoveNavigation(object? parameter)
        {
            NavigationSide = NavigationSide == NavigationSide.Left
              ? NavigationSide.Right
              : NavigationSide.Left;
          
        }
        private void Delete(object obj)
        {
            //remove an existing selected customer from the observable collection
            if(SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        //check if the customer exists to enable delete
        private bool CanDelete(object arg)
        {
            return SelectedCustomer is not null;
        }

    }
    public enum NavigationSide
    {
        Left,
        Right
    }

}
