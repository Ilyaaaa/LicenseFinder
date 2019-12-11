using LicenseFinder.WPF.ViewModels;
using System;
using System.ComponentModel;
using System.Windows;

namespace LicenseFinder.WPF
{
    public partial class AddAppLicenseWindow : Window
    {
        #region Properties

        private AddAppLicenseViewModel ViewModel;

        #endregion

        #region Constructors

        public AddAppLicenseWindow(AddAppLicenseViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
            viewModel.Saved += OnSave;

            ViewModel = viewModel;
        }

        #endregion

        #region Private functions

        private void OnSave(object sender, EventArgs e) => Close();

        #endregion

        #region Overrides

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Closing -= ViewModel.OnWindowClosing;
            ViewModel.Saved -= OnSave;
        }

        #endregion
    }
}
