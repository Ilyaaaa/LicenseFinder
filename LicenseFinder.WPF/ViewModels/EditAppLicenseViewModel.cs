using LicenseFinder.Core.Entities;
using LicenseFinder.Core.Interfaces;
using LicenseFinder.WPF.Interfaces;
using LicenseFinder.WPF.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;

namespace LicenseFinder.WPF.ViewModels
{
    public class EditAppLicenseViewModel : BaseViewModel
    {
        #region properties

        public List<LicenseTypeInfo> LicenseTypes { get; private set; }

        private LicenseTypeInfo _selectedType;
        public LicenseTypeInfo SelectedType
        {
            get => _selectedType;

            set
            {
                _selectedType = value;
                OnPropertyChanged("SelectedType");
            }
        }

        private string _appName;
        public string AppName
        {
            get => _appName;

            set
            {
                _appName = value;
                OnPropertyChanged("AppName");
            }
        }

        public ICommand SaveCommand { get; private set; }

        private Core.Entities.App OldApp;

        #endregion

        #region Constructors

        public EditAppLicenseViewModel(INavigationService navigationService, IAppRepsository appRepository) : base(navigationService, appRepository)
        {
            LicenseTypes = new List<LicenseTypeInfo>()
            {
                new LicenseTypeInfo(LicenseType.Free),
                new LicenseTypeInfo(LicenseType.Paid),
                new LicenseTypeInfo(LicenseType.CanBePaid),
                new LicenseTypeInfo(LicenseType.FreeForPersonalUse)
            };
            SelectedType = LicenseTypes[0];

            SaveCommand = new Command(Save, () => true);
        }

        #endregion

        #region Public functions

        public void SetData(Core.Entities.App app)
        {
            AppName = app.Name;
            SelectedType = LicenseTypes.Find(info => info.LicenseType == app.LicenseType) ?? LicenseTypes[0];

            OldApp = app;
        }

        #endregion

        #region Private functions

        private void Save()
        {
            AppRepository.Save(new Core.Entities.App(AppName, SelectedType.LicenseType));
            if (OldApp != null && OldApp.Name != AppName)
                AppRepository.Delete(OldApp.Name);

            Saved?.Invoke(this, EventArgs.Empty);
        }

        #endregion

        #region Overrides

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {

        }

        #endregion

        #region Events

        public event EventHandler Saved;

        #endregion
    }
}
