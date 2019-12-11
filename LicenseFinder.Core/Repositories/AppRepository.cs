using LicenseFinder.Core.Entities;
using LicenseFinder.Core.Interfaces;
using LicenseFinder.Core.Providers;
using SQLite;
using System;
using System.Collections.ObjectModel;

namespace LicenseFinder.Core.Repositories
{
    public class AppRepository : IAppRepsository
    {
        #region Properties

        private SQLiteConnection Connection;

        #endregion

        #region Constructors

        public AppRepository()
        {
            Connection = SQLiteConnectionProvider.Instance.GetConnection();
            Connection.CreateTable<App>();
        }

        #endregion

        #region Public functions

        public void Delete(string Id)
        {
            Connection.Delete<App>(Id);
            Deleted?.Invoke(this, Id);
        }

        public ObservableCollection<App> GetAll() => new ObservableCollection<App>(Connection.Table<App>());

        public App GetById(string Id) => Connection.Table<App>().Where(app => app.Name == Id).FirstOrDefault();

        public void Save(App entity)
        {
            Connection.InsertOrReplace(entity);
            Saved?.Invoke(this, entity);
        }

        #endregion

        #region Events

        public event EventHandler<App> Saved;
        public event EventHandler<string> Deleted;

        #endregion
    }
}
