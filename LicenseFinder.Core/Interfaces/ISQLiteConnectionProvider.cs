using SQLite;

namespace LicenseFinder.Core.Interfaces
{
    interface ISQLiteConnectionProvider
    {
        SQLiteConnection GetConnection();
    }
}
