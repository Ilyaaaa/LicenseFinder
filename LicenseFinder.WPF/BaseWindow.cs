using System.ComponentModel;
using System.Windows;

namespace LicenseFinder.WPF
{
    public abstract class BaseWindow<T> : Window
    {
        protected T Parameter;

        public void Show(T param)
        {
            Parameter = param;
            Show();
        }


        protected abstract void OnClosing();

        protected override void OnClosing(CancelEventArgs e)
        {
            base.OnClosing(e);
            OnClosing();
        }
    }
}
