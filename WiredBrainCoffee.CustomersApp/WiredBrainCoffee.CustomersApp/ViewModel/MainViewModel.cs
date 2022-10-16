using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;
using WiredBrainCoffee.CustomersApp.Command;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.Model;

namespace WiredBrainCoffee.CustomersApp.ViewModel
{
    public class MainViewModel : ViewModelBase
    {
        public MainViewModel(ICustomerDataProvider customerDataProvider)
        {
            _customerDataProvider = customerDataProvider;
            AddCommand = new DelegateCommand(Add);
            DeleteCommand = new DelegateCommand(Delete, CanDelete);
        }

        private readonly ICustomerDataProvider _customerDataProvider;

        public ObservableCollection<CustomerItemViewModel> Customers { get; } = new();

        private CustomerItemViewModel? _selectedCustomer;
        public CustomerItemViewModel? SelectedCustomer
        {
            get => _selectedCustomer;
            set
            {
                if (_selectedCustomer != value)
                {
                    _selectedCustomer = value;
                    RaisePropertyChanged();
                    RaisePropertyChanged(nameof(IsCustomerSelected));
                    DeleteCommand.RaiseCanExecuteChanged();
                }
            }
        }

        public DelegateCommand AddCommand { get; }

        public DelegateCommand DeleteCommand { get; }

        public bool IsCustomerSelected => SelectedCustomer is not null;

        private bool CanDelete(object? parameter) => SelectedCustomer is not null;

        private void Delete(object? parameter)
        {
            if(SelectedCustomer is not null)
            {
                Customers.Remove(SelectedCustomer);
                SelectedCustomer = null;
            }
        }

        public async Task LoadAsync()
        {
            if (Customers.Any())
            {
                return;
            }

            var customers = await _customerDataProvider.GetAllAsync();
            if (customers is not null)
            {
                foreach (var customer in customers)
                {
                    Customers.Add(new CustomerItemViewModel(customer));
                }
            }
        }

        private void Add(object? parameter) 
        {
            var customer = new Customer { FirstName = "New", LastName = "Customer", IsDeveloper = false };
            var customerViewModel = new CustomerItemViewModel(customer);
            Customers.Add(customerViewModel);

            SelectedCustomer = customerViewModel;
        }            
    }
}
