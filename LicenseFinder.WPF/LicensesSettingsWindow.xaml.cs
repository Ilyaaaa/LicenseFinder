using LicenseFinder.WPF.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace LicenseFinder.WPF
{
    public partial class LicensesSettingsWindow : Window
    {
        #region Properties

        private LicensesSettingsViewModel ViewModel;

        #endregion

        #region Constructors

        public LicensesSettingsWindow(LicensesSettingsViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;

            ViewModel = viewModel;
        }

        #endregion

        #region Overrides

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);

            Closing -= ViewModel.OnWindowClosing;
        }

        #endregion

        #region Private functions

        private void OnItemClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => ViewModel.ItemClickCommand.Execute(e);

        private void OnItemDeleteClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => ViewModel.DeleteItemCommand.Execute(e);

        #endregion
    }
}
