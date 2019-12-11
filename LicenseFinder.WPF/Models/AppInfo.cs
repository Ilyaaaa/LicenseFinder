using LicenseFinder.WPF.Models;
using System.Drawing;
using System.Windows.Media;

namespace LicenseFinder.Models
{
    public class AppInfo
    {
        #region Properties

        public ImageSource Icon { get; set; }
        public string Name { get; set; }
        public string Path { get; set; }
        public LicenseTypeInfo LicenseTypeInfo { get; set; }

        #endregion

        #region Constructors

        public AppInfo(string name)
        {
            Name = name;
        }

        #endregion
    }
}
