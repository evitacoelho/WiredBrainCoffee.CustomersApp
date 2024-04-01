using Microsoft.Extensions.DependencyInjection;
using System.Windows;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private readonly ServiceProvider _serviceProvider;

        public App()
        {
            //create and configure a service collection
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            //inject services / dependencies here
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>(); 
            services.AddTransient<CustomersViewModel>();
            services.AddTransient<ProductsViewModel>();

            //add interfaces and their implementation
            services.AddTransient<ICustomerDataProvider, CustomerDataProvider>();
            services.AddTransient<IProductDataProvider, ProductDataProvider>();
        }

        //called on startup
        protected override void OnStartup(StartupEventArgs e)
        {
            base.OnStartup(e);
            //create and display an instance of the main window on start up
            //all the constructors here are dependencies
           // var mainWindow = new MainWindow(new MainViewModel(new CustomersViewModel(new CustomerDataProvider()), new ProductsViewModel()));
            var mainWindow = _serviceProvider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }

}
