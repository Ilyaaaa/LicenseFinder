using LicenseFinder.WPF.Interfaces;
using LicenseFinder.WPF.ViewModels;
using System;
using System.Windows;

namespace LicenseFinder.WPF
{
    public partial class EditAppLicenseWindow : Window, ISetParameter<Core.Entities.App>
    {
        #region Properties

        private EditAppLicenseViewModel ViewModel;

        #endregion

        #region Constructors

        public EditAppLicenseWindow(EditAppLicenseViewModel viewModel)
        {
            InitializeComponent();

            base.DataContext = viewModel;
            base.Closing += viewModel.OnWindowClosing;
            viewModel.Saved += OnSave;

            ViewModel = viewModel;
        }

        #endregion

        #region Public functions

        public void SetParameter(Core.Entities.App parameter) =>  ViewModel.SetData(parameter);

        #endregion

        #region Private functions

        private void OnSave(object sender, EventArgs e) => base.Close();

        #endregion

        #region Overrides

        protected void OnClosing()
        {
            base.Closing -= ViewModel.OnWindowClosing;
            ViewModel.Saved -= OnSave;
        }

        #endregion
    }
}
