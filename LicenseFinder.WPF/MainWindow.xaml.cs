using LicenseFinder.WPF.ViewModels;
using System.ComponentModel;
using System.Windows;

namespace LicenseFinder
{
    public partial class MainWindow : Window
    {
        #region Properties

        private MainViewModel ViewModel;

        #endregion

        #region Constructors

        public MainWindow(MainViewModel viewModel)
        {
            InitializeComponent();

            DataContext = viewModel;
            Closing += viewModel.OnWindowClosing;
            viewModel.ItemRefreshed += OnItemRefreshed;

            ViewModel = viewModel;
        }

        #endregion

        #region Private functons

        private void OnItemRefreshed(object sender, int e) => List.Items.Refresh();

        private void CopyName(object sender, System.Windows.Input.MouseButtonEventArgs e) => ViewModel.CopyNameCommand.Execute(e);

        private void CopyInstallPath(object sender, System.Windows.Input.MouseButtonEventArgs e) => ViewModel.CopyInstallPathCommand.Execute(e);

        #endregion

        #region Overrides

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            Closing -= ViewModel.OnWindowClosing;
            ViewModel.ItemRefreshed -= OnItemRefreshed;
        }

        #endregion
    }
}
