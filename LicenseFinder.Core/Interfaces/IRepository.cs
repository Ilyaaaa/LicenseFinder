using System;
using System.Collections.ObjectModel;

namespace LicenseFinder.Core.Interfaces
{
    public interface IRepository<T>
    {
        event EventHandler<T> Saved;
        event EventHandler<string> Deleted;

        ObservableCollection<T> GetAll();
        T GetById(string Id);
        void Save(T entity);
        void Delete(string Id);
    }
}
