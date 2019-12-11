using SQLite;
using System;

namespace LicenseFinder.Core.Entities
{
    public enum LicenseType : int { Free = 0, Paid = 1, CanBePaid = 2, FreeForPersonalUse = 3 } 

    [Table("Apps")]
    public class App
    {
        #region Properties

        [PrimaryKey]
        public string Name { get; set; }
        public LicenseType LicenseType { get; set; }

        #endregion

        #region Constructors

        public App()
        {

        }

        public App(string name, LicenseType licenseType)
        {
            Name = name;
            LicenseType = licenseType;
        }

        #endregion

        #region Overrides

        public override bool Equals(object obj) => obj is App app && Name == app.Name;

        public override int GetHashCode() => HashCode.Combine(Name);

        #endregion
    }
}
