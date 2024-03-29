using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace WiredBrainCoffee.CustomersApp
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
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
            var column = Grid.GetColumn(customerListGrid);
            var newColumn = column == 0 ? 2 : 0;
            Grid.SetColumn(customerListGrid, newColumn);
        }
    }
}