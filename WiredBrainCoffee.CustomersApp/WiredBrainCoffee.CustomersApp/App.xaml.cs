using Microsoft.Extensions.DependencyInjection;
using Microsoft.UI.Xaml;
using WiredBrainCoffee.CustomersApp.Data;
using WiredBrainCoffee.CustomersApp.ViewModel;

namespace WiredBrainCoffee.CustomersApp
{
    public partial class App : Application
    {
        private Window? m_window;
        private readonly ServiceProvider _serviceProvider;
        public App()
        {
            this.InitializeComponent();
            ServiceCollection services = new();
            ConfigureServices(services);
            _serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddTransient<MainWindow>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<ICustomerDataProvider, CustomerDataProvider>();
        }

        protected override void OnLaunched(Microsoft.UI.Xaml.LaunchActivatedEventArgs args)
        {
            m_window = _serviceProvider.GetService<MainWindow>();
            m_window?.Activate();
        }
    }
}
