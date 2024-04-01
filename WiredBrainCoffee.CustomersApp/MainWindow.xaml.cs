using System.Windows;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp
{
    
   
    public partial class MainWindow : Window
    {
        private MainViewModel _viewModel;

        public MainWindow()
        {
            InitializeComponent();
            //set the data context for this view --> bind it the view model
            _viewModel = new MainViewModel(new CustomersViewModel(new CustomerDataProvider()), new ProductsViewModel());
            DataContext = _viewModel;

            //event handler for loaded event when the view is ready 
            //this populates the customers for the view when fired
            Loaded += Customers_ViewLoaded;
        }
        private async void Customers_ViewLoaded(object sender, RoutedEventArgs e)
        {
            //invokes the method from the viewmodel to load the view with customer data from the data provider
            await _viewModel.LoadAsync();
        }
    }
}