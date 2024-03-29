using System.Globalization;
using System.Windows.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp.Converter
{
    public class NavigationSideToGridColumnConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            //takes the value of the navigation side and converts it from enum to int
            var navigationSide = (NavigationSide)value;
            return navigationSide == NavigationSide.Left ? 2 : 0;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
