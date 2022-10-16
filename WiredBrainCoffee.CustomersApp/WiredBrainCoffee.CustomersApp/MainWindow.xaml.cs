using Microsoft.UI.Xaml;
using Microsoft.UI.Xaml.Controls;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp
{
    public sealed partial class MainWindow : Window
    {
        public MainViewModel ViewModel { get; }
        
        public MainWindow()
        {
            this.InitializeComponent();
            Title = "Customers App";
            ViewModel = new MainViewModel(new CustomerDataProvider());
            root.Loaded += Root_Loaded;
        }

        private async void Root_Loaded(object sender, RoutedEventArgs e)
        {
            await ViewModel.LoadAsync();
        }

        private void ButtonMoveNavigation_Click(object sender, RoutedEventArgs e)
        {
            // var column = (int)customerListGrid.GetValue(Grid.ColumnProperty);
            var column = Grid.GetColumn(customerListGrid);

            var newColumn = column == 0 ? 2 : 0;

            // customerListGrid.SetValue(Grid.ColumnProperty, newColumn);
            Grid.SetColumn(customerListGrid, newColumn);

            symbolIconMoveNavigation.Symbol = newColumn == 0 ? Symbol.Forward : Symbol.Back;
        }
    }
}
