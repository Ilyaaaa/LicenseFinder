using LicenseFinder.Core.Interfaces;
using LicenseFinder.Core.Repositories;
using LicenseFinder.WPF.Interfaces;
using LicenseFinder.WPF.ViewModels;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Windows;

namespace LicenseFinder.WPF
{
    public partial class App : Application
    {
        #region Properties

        public IServiceProvider ServiceProvider { get; private set; }

        #endregion

        #region Constructors

        public App()
        {
            var services = new ServiceCollection();
            services.AddSingleton<IAppRepsository, AppRepository>();
            services.AddSingleton<INavigationService, NavigationService>();
            services.AddTransient(typeof(MainViewModel));
            services.AddTransient(typeof(LicensesSettingsViewModel));
            services.AddTransient(typeof(AddAppLicenseViewModel));
            services.AddTransient(typeof(EditAppLicenseViewModel));
            services.AddTransient(typeof(MainWindow));
            services.AddTransient(typeof(LicensesSettingsWindow));
            services.AddTransient(typeof(AddAppLicenseWindow));
            services.AddTransient(typeof(EditAppLicenseWindow));

            ServiceProvider = services.BuildServiceProvider();
            ServiceProvider.GetRequiredService<MainWindow>().Show();
        }

        #endregion
    }
}
