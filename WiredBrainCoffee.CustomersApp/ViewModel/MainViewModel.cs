using WiredBrainCoffee.CustomersApp.Command;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel:ViewModelBase
    {
        private ViewModelBase? _selectedViewModel;
       
        private CustomersViewModel customersViewModel;

       
        //selects the VM for the view
        public MainViewModel(CustomersViewModel customersViewModel, ProductsViewModel productsViewModel)
        {
            CustomersViewModel = customersViewModel;
            ProductsViewModel = productsViewModel; 
            SelectedViewModel = CustomersViewModel;
            //command to select a VM
            SelectViewModelCommand = new DelegateCommand(SelectViewModel);
        }

        private async void SelectViewModel(object? parameter)
        {
           SelectedViewModel = parameter as ViewModelBase;
           await LoadAsync();
        }

        public ViewModelBase? SelectedViewModel 
        { get => _selectedViewModel;
            set
            {
                 _selectedViewModel = value;
                RaisePropertyChanged();
            }  
        }
        public CustomersViewModel CustomersViewModel { get; }
        public ProductsViewModel ProductsViewModel { get; }
        public DelegateCommand SelectViewModelCommand { get; }
  
        public async override Task LoadAsync()
        {
            if(SelectedViewModel != null)
            {
                await SelectedViewModel.LoadAsync();
            }
        }
    }
}
