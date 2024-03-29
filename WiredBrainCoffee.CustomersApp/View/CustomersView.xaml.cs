using System.Windows;
using System.Windows.Controls;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.View
{
    /// <summary>
    /// Interaction logic for CustomersView.xaml
    /// </summary>
    public partial class CustomersView : UserControl
    {
        private CustomersViewModel _viewModel;

        public CustomersView()
        {
            InitializeComponent();

            //set the data context for this view --> bind it the view model
            _viewModel =  new CustomersViewModel(new CustomerDataProvider());
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

        private void BtnMoveNavigation_Click(object sender, RoutedEventArgs e)
        {
            //**Methid 1: Using Dependency property on GetValue() or SetValue()
            //get the value of the column of customer list
            //this can be done using the dependency property
            //GetValue returns an object --> type cast to int to get the column number

            //var column = (int)customerListGrid.GetValue(Grid.ColumnProperty);

            //evaluate the target column to move 
            //var newColumn = column == 0 ? 2 : 0;

            //set the column to its new value
            //dependency object stores values in a dictionary
            //customerListGrid.SetValue(Grid.ColumnProperty, newColumn);


            //**Method 2: GetColumn which takes a UI element and returns the property value as an int
            //var column = Grid.GetColumn(customerListGrid);
            //var newColumn = column == 0 ? 2 : 0;
            //Grid.SetColumn(customerListGrid, newColumn);
            //define this in the View model
            _viewModel.MoveNavigation();
        }
      

        private void BtnAdd_Click(object sender, RoutedEventArgs e)
        {
            _viewModel.Add();
        }
    }
}
