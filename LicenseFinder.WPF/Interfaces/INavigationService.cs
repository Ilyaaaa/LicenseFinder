using System.Windows;

namespace LicenseFinder.WPF.Interfaces
{
    public interface INavigationService
    {
        void Navigate<T>() where T : Window;
        void Navigate<T, ParamT>(ParamT param) where T : Window, ISetParameter<ParamT>;
    }
}
