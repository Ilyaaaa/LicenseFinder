using LicenseFinder.Core.Interfaces;
using SQLite;
using System;
using System.IO;

namespace LicenseFinder.Core.Providers
{
    class SQLiteConnectionProvider : ISQLiteConnectionProvider
    {
        #region Static properties

        private static SQLiteConnectionProvider _instance;
        public static SQLiteConnectionProvider Instance
        {
            get
            {
                if (_instance == null)
                    _instance = new SQLiteConnectionProvider();

                return _instance;
            }
        }
        #endregion

        #region Properties

        private readonly string DBFilePath;

        #endregion

        #region Constructors

        private SQLiteConnectionProvider()
        {
            DBFilePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "db.sql");
        }

        #endregion

        #region Public functions

        public SQLiteConnection GetConnection() => new SQLiteConnection(DBFilePath);

        #endregion
    }
}
