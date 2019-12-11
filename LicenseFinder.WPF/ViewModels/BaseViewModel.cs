using LicenseFinder.Core.Interfaces;
using LicenseFinder.WPF.Interfaces;
using System.ComponentModel;

namespace LicenseFinder.WPF.ViewModels
{
    public abstract class BaseViewModel : INotifyPropertyChanged
    {
        #region Properties

        protected IAppRepsository AppRepository;
        protected INavigationService NavigationService;

        #endregion

        #region Constructors

        public BaseViewModel(INavigationService navigationService, IAppRepsository appRepository)
        {
            NavigationService = navigationService;
            AppRepository = appRepository;
        }

        #endregion

        #region Private functions

        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));

        #endregion

        #region Events

        public event PropertyChangedEventHandler PropertyChanged;

        #endregion

        #region Abstract functions

        abstract public void OnWindowClosing(object sender, CancelEventArgs e);

        #endregion
    }
}
