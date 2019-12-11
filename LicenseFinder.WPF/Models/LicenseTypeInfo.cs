using LicenseFinder.Core.Entities;

namespace LicenseFinder.WPF.Models
{
    public class LicenseTypeInfo
    {
        #region Static properties

        public static LicenseTypeInfo Default = new LicenseTypeInfo("Unknown");

        #endregion

        #region Properties

        public LicenseType LicenseType { get; set; }
        public string TypeName { get; set; }

        #endregion

        #region Constructors

        public LicenseTypeInfo(LicenseType licenseType)
            {
            LicenseType = licenseType;

            switch (LicenseType)
            {
                case LicenseType.Free:
                    TypeName = "Free";
                    break;

                case LicenseType.Paid:
                    TypeName = "Paid";
                    break;

                case LicenseType.CanBePaid:
                    TypeName = "Can be paid";
                    break;

                case LicenseType.FreeForPersonalUse:
                    TypeName = "Free for personal use";
                    break;

                default: 
                    TypeName = string.Empty;
                    break;
            }
        }

        private LicenseTypeInfo(string name)
        {
            TypeName = name;
        }

    #endregion
}
}
