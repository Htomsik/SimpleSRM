﻿using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProjectMateTask.DiRegistrators;
using ProjectMateTask.Services.AppInfrastructure.NavigationServices.Base;
using ProjectMateTask.VMD;
using ProjectMateTask.VMD.AppInfrastructure;
using ProjectMateTask.VW.Windows;

namespace ProjectMateTask
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private IHost? _host;

        public IHost Host => _host ?? Program.CreateHostBuilder(Environment.GetCommandLineArgs()).Build();

   
        protected override async void OnStartup(StartupEventArgs e)
        {
            var host = Host;

            var initialNavigationServices = host.Services.GetRequiredService<INavigationService>();
            
            initialNavigationServices.Navigate();
            
            
            MainWindow = host.Services.GetRequiredService<MainWindow>();

            MainWindow.Show();
            
            base.OnStartup(e);
        
            await host.StartAsync().ConfigureAwait(false);
        }

        protected override async void OnExit(ExitEventArgs e)
        {
            var host = Host;

            base.OnExit(e);

            await host.StopAsync().ConfigureAwait(false);

            host.Dispose();
            
        }

        public static void ConfigureServices(HostBuilderContext host, IServiceCollection services)
        {
            services.AddSingleton(s => new MainWindow
            {
                DataContext = s.GetRequiredService<MainWindowVmd>()
            });

            services
                .StoreRegistration()
                .ServicesRegistration()
                .VmdRegistration();
        }
    }
}