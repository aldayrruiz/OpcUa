using Microsoft.Extensions.DependencyInjection;
using OpcUa.ClientWPF.State;
using OpcUa.ClientWPF.State.Clients;
using OpcUa.ClientWPF.State.Connectors;
using OpcUa.ClientWPF.State.Navigators;
using OpcUa.ClientWPF.ViewModels;
using OpcUa.ClientWPF.ViewModels.Factories;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;

namespace OpcUa.ClientWPF
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        protected override void OnStartup(StartupEventArgs e)
        {

            IServiceProvider serviceProvider = CreateServiceProvider();
            Window window = new MainWindow();

            window.DataContext = serviceProvider.GetRequiredService<MainViewModel>();
            window.Show();
            

            base.OnStartup(e);
        }

        private IServiceProvider CreateServiceProvider()
        {
            IServiceCollection services = new ServiceCollection();

            services.AddSingleton<IViewModelAbstractFactory, ViewModelAbstractFactory>();
            services.AddSingleton<IViewModelFactory<ReadViewModel>, ReadViewModelFactory>();
            services.AddSingleton<IViewModelFactory<WriteViewModel>, WriteViewModelFactory>();
            services.AddSingleton<IViewModelFactory<CallViewModel>, CallViewModelFactory>();
            services.AddSingleton<IViewModelFactory<SubscribeViewModel>, SubscribeViewModelFactory>();

            services.AddSingleton<IClientStore, ClientStore>();
            services.AddSingleton<IConnector, Connector>();

            services.AddScoped<ServerAddressViewModel>();
            services.AddScoped<INavigator, Navigator>();
            services.AddScoped<MainViewModel>();

            return services.BuildServiceProvider();
        }
    }
}
