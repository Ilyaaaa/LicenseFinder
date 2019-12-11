using LicenseFinder.Core.Interfaces;
using LicenseFinder.Models;
using LicenseFinder.WPF.Interfaces;
using LicenseFinder.WPF.Models;
using LicenseFinder.WPF.Properties;
using Microsoft.Win32;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Input;
using System.Windows.Interop;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace LicenseFinder.WPF.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        #region Properties

        public ObservableCollection<AppInfo> Apps { get; private set; }

        private AppInfo _selectedApp;
        public AppInfo SelectedApp
        {
            get => _selectedApp;
            set
            {
                _selectedApp = value;
                OnPropertyChanged("SelectedApp");
            }
        }

        private string _messageText;
        public string MessageText
        {
            get => _messageText;
            set
            {
                _messageText = value;
                OnPropertyChanged("MessageText");
            }
        }

        public ICommand OpenLicensesSettingsCommand { get; private set; }
        public ICommand CopyNameCommand { get; private set; }
        public ICommand CopyInstallPathCommand { get; private set; }

        #endregion

        #region Constructors

        public MainViewModel(INavigationService navigationService, IAppRepsository appRepository) : base(navigationService, appRepository)
        {
            OpenLicensesSettingsCommand = new Command(OpenLicensesSettings, CanOpenLicensesSettings);
            CopyNameCommand = new Command(CopyName, () => SelectedApp != null);
            CopyInstallPathCommand = new Command(CopyInstallPath, () => SelectedApp != null);

            Apps = new ObservableCollection<AppInfo>();

            AppRepository.Saved += OnAppLicenseSaved;
            AppRepository.Deleted += OnAppLicenseDeleted;

            string registryKey = @"SOFTWARE\Microsoft\Windows\CurrentVersion\Uninstall";
            LoadApps(Registry.LocalMachine.OpenSubKey(registryKey));
            LoadApps(Registry.CurrentUser.OpenSubKey(registryKey));

            string registryKey64 = @"SOFTWARE\Wow6432Node\Microsoft\Windows\CurrentVersion\Uninstall";
            LoadApps(Registry.LocalMachine.OpenSubKey(registryKey64));

            OnPropertyChanged("Apps");
        }

        #endregion

        #region Private functions

        private void LoadApps(RegistryKey key)
        {
            var defaultIcon = BitmapToImageSource(Resources._default);

            using (key)
            {
                foreach (string subkey_name in key.GetSubKeyNames())
                {
                    using (RegistryKey subkey = key.OpenSubKey(subkey_name))
                    {
                        var nameKey = (string) subkey.GetValue("DisplayName");
                        var locationKey = (string) subkey.GetValue("InstallLocation");
                        if (string.IsNullOrEmpty(nameKey) || string.IsNullOrEmpty(locationKey)) continue;

                        var app = new AppInfo(nameKey);
                        app.Path = locationKey;

                        var iconKey = (string)subkey.GetValue("DisplayIcon");
                        if (!string.IsNullOrEmpty(iconKey))
                            try
                            {
                                app.Icon = IconToImageSource(Icon.ExtractAssociatedIcon(iconKey));
                            }
                            catch (IOException ex)
                            {
                                app.Icon = defaultIcon;
                                Console.WriteLine(ex);
                            }

                        else
                            app.Icon = defaultIcon;

                        var appInfo = AppRepository.GetById(app.Name);
                        if (appInfo != null)
                            app.LicenseTypeInfo = new LicenseTypeInfo(appInfo.LicenseType);
                        else
                            app.LicenseTypeInfo = LicenseTypeInfo.Default;

                        Apps.Add(app);
                    }
                }
            }
        }

        private bool CanOpenLicensesSettings() => !LicensesSettingsViewModel.IsExists;

        private void OpenLicensesSettings() => NavigationService.Navigate<LicensesSettingsWindow>();

        private void OnAppLicenseDeleted(object sender, string e)
        {
            for (int i = 0; i < Apps.Count; i++)
            {
                var app = Apps[i];

                if (app.Name == e)
                {
                    app.LicenseTypeInfo = LicenseTypeInfo.Default;
                    ItemRefreshed?.Invoke(this, i);
                    break;
                }
            }
        }

        private void OnAppLicenseSaved(object sender, Core.Entities.App e)
        {
            for (int i = 0; i < Apps.Count; i++)
            {
                var app = Apps[i];
                
                if (app.Name == e.Name)
                {
                    app.LicenseTypeInfo = new LicenseTypeInfo(e.LicenseType);
                    ItemRefreshed?.Invoke(this, i);
                    break;
                }
            }
        }

        private void CopyName()
        {
            Clipboard.SetText(SelectedApp.Name);
            MessageText =  "\"" + SelectedApp.Name + "\" copied";
        }

        private void CopyInstallPath()
        {
            Clipboard.SetText(SelectedApp.Path);
            MessageText = "\"" + SelectedApp.Path + "\" copied";
        }

        #endregion

        #region Public functions

        public static ImageSource IconToImageSource(Icon icon)
        {
            Bitmap bitmap = icon.ToBitmap();
            IntPtr hBitmap = bitmap.GetHbitmap();

            ImageSource wpfBitmap = Imaging.CreateBitmapSourceFromHBitmap(
                hBitmap,
                IntPtr.Zero,
                Int32Rect.Empty,
                BitmapSizeOptions.FromEmptyOptions());

            return wpfBitmap;
        }

        public ImageSource BitmapToImageSource(Bitmap bmp)
        {
            var handle = bmp.GetHbitmap();
            return Imaging.CreateBitmapSourceFromHBitmap(handle, IntPtr.Zero, Int32Rect.Empty, BitmapSizeOptions.FromEmptyOptions());
        }

        #endregion

        #region Overrides

        public override void OnWindowClosing(object sender, CancelEventArgs e)
        {
            AppRepository.Saved -= OnAppLicenseSaved;
            AppRepository.Deleted -= OnAppLicenseDeleted;
        }

        #endregion

        #region events

        public event EventHandler<int> ItemRefreshed;

        #endregion
    }
}
