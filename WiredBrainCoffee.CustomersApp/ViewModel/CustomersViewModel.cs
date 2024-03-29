using System.Collections.ObjectModel;
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
            }
        }

        public NavigationSide NavigationSide
        {
            get => _navigationSide;
            private set
            {
                _navigationSide = value;
                RaisePropertyChanged();
            }
        }



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
        internal void Add()
        {
            //create a customer on button click add with some defined values
            var customer = new Customer { FirstName = "New" };
            var viewModel = new CustomerItemViewModel(customer);
            //add the customer to the observable collection of customers
            Customers.Add(viewModel);
            SelectedCustomer = viewModel;
        }

        internal void MoveNavigation()
        {
            NavigationSide = NavigationSide == NavigationSide.Left
              ? NavigationSide.Right
              : NavigationSide.Left;
          
        }

    }
    public enum NavigationSide
    {
        Left,
        Right
    }

}
