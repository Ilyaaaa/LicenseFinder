using LicenseFinder.Core.Interfaces;
using LicenseFinder.WPF.Interfaces;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Windows.Input;

namespace LicenseFinder.WPF.ViewModels
{
    public class LicensesSettingsViewModel : BaseViewModel
    {
        #region Static properties

        public static bool IsExists = false;

        #endregion

        #region Properties

        public ObservableCollection<Core.Entities.App> Apps { get; private set; }

        private Core.Entities.App _selectedApp;
        public Core.Entities.App SelectedApp
        {
            get => _selectedApp;
            set
            {
                _selectedApp = value;
                OnPropertyChanged("SelectedApp");
            }
        }

        public ICommand AddAppLicenseCommand { get; private set; }
        public ICommand ItemClickCommand { get; private set; }
        public ICommand DeleteItemCommand { get; private set; }

        #endregion

        #region Constructors

        public LicensesSettingsViewModel(INavigationService navigationService, IAppRepsository appRepository) : base(navigationService, appRepository)
        {
            IsExists = true;

            Apps = AppRepository.GetAll();

            AppRepository.Saved += OnAppLicenseSaved;
            AppRepository.Deleted += OnAppLicenseDeleted;

            AddAppLicenseCommand = new Command(AddAppLicense, () => true);
            ItemClickCommand = new Command(OnItemClick, () => true);
            DeleteItemCommand = new Command(DeleteItem, () => true);
        }

        #endregion

        #region Private functions

        private void DeleteItem() => AppRepository.Delete(SelectedApp.Name);

        private void OnItemClick() => NavigationService.Navigate<EditAppLicenseWindow, Core.Entities.App>(SelectedApp);

        private void AddAppLicense() => NavigationService.Navigate<AddAppLicenseWindow>();

        private void OnAppLicenseDeleted(object sender, string e)
        {
            foreach (Core.Entities.App app in Apps)
                if (app.Name == e)
                {
                    Apps.Remove(app);
                    break;
                }
        }

        private void OnAppLicenseSaved(object sender, Core.Entities.App e)
        {
            var index = Apps.IndexOf(e);
            if (index == -1)
                Apps.Add(e);
            else
                Apps[index] = e;

            OnPropertyChanged("Apps");
        }

        #endregion

        #region Overrides

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {
            IsExists = false;

            AppRepository.Saved -= OnAppLicenseSaved;
            AppRepository.Deleted -= OnAppLicenseDeleted;
        }

        #endregion
    }
}
