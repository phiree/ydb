using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dianzhu.IDAL
{
    public interface IDALBase<T>
    {
        ISession Session { get; }
        void Delete(T o);
        void Save(T o);
        void SaveList(IList<T> list);
        void Update(T o);
        void SaveOrUpdate(T o);
        T GetOne(object id);
        T GetOneByQuery(IQueryOver<T> queryover);
        T GetOneByQuery(string query);
        IList<T> GetAll<T>() where T : class;
        IList<T> GetList(string query);
        IList<T> GetList(IQueryOver<T, T> queryOver);
        IList<T> GetList(string query, int pageIndex, int pageSize, out int totalRecords);
        IList<T> GetList(string query, string orderColumns, bool orderDesc, int pageIndex, int pageSize, out int totalRecords, string query_count);
        System.Data.DataSet ExecuteSql(string pureSqlStatement);
    }
}
