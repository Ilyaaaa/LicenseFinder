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
    public class AddAppLicenseViewModel : BaseViewModel
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

        #endregion

        #region Constructors

        public AddAppLicenseViewModel(INavigationService navigationService, IAppRepsository appRepository) : base(navigationService, appRepository)
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

        #region Private functions

        private void Save()
        {
            AppRepository.Save(new Core.Entities.App(AppName, SelectedType.LicenseType));
            Saved?.Invoke(this, EventArgs.Empty);
        }

        #endregion

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
