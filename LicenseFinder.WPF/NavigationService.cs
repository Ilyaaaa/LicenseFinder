using LicenseFinder.WPF.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System.Windows;

namespace LicenseFinder.WPF
{
    class NavigationService : INavigationService
    {
        #region Public functions

        public void Navigate<T>() where T : Window
        {
            ((App)Application.Current).ServiceProvider.GetRequiredService<T>().Show();
        }

        public void Navigate<T,ParamT>(ParamT param) where T : Window, ISetParameter<ParamT>
        {
            var w = ((App)Application.Current).ServiceProvider.GetRequiredService<T>();
            w.SetParameter(param);

            w.Show();
        }

        #endregion
    }
}
