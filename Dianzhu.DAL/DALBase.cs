using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Hql;
using System.Web.Security;
using System.Data.SqlClient;

namespace Dianzhu.DAL
{
    public class DALBase<T> 
    {
         
        private ISession session = null;// new HybridSessionBuilder().GetSession();
         
        public ISession Session {
            get { return session; }
            set { session = value; }
        }
        public virtual void Delete(T o)
        {
            session.Delete(o);
            session.Flush();
        }
        public virtual void Save(T o)
        {
            
            using (var t = session.BeginTransaction())
            {
                session.Save(o);

                t.Commit();
            }
            session.Flush();
            
        }
        /// <summary>
        /// 保存列表
        /// </summary>
        /// <param name="list"></param>
        public virtual void SaveList(IList<T> list)
        {
            using (var trans = session.BeginTransaction())
            {

                foreach (T t in list)
                {
                    SaveOrUpdate(t);
                }
                trans.Commit();
            }
        }
        public virtual void Update(T o)
        {
            session.Update(o);
            session.Flush();
        }
        public void SaveOrUpdate(T o)
        {

            session.SaveOrUpdate(o);
            if (!session.Transaction.IsActive)
            {
                session.Flush();
            }


        }
        public virtual T GetOne(object id)
        {
            T r= session.Get<T>(id);
            if (!session.Transaction.IsActive)
            {
                session.Flush();
            }
            return r;
        }
        public T GetOneByQuery(IQueryOver<T> queryOver)
        {
            return GetOneByQuery(queryOver.List());
        }
        public T GetOneByQuery(string query)
        {
            return GetOneByQuery(GetList(query));
        }
        public T GetOneByQuery(IList<T> listT)
        {

            if (listT.Count == 1)
            {
                return listT[0];
            }
            else if (listT.Count == 0)
            {
                return default(T);
            }
            else
            {
                string errmsg = "错误:GetOnByQuery应该只能返回一个值.现在有" + listT.Count + "个值返回.";

                //NLibrary.NLogger.Logger.Error(errmsg);
                return listT[0];
                // throw new Exception(errmsg);
            }
        }

        public virtual IList<T> GetAll<T>() where T : class
        {
            session.Clear();
            return session.QueryOver<T>().List();
        }

        public IList<T> GetList(string query)
        {
            int totalRecords;
            return GetList(query, 0, 99999, out totalRecords);
        }
        public IList<T> GetList(IQueryOver<T, T> queryOver)
        {
            return queryOver.List();
        }

        public IList<T> GetList(string query, int pageIndex, int pageSize, out int totalRecords)
        {
            return GetList(query, string.Empty, false, pageIndex, pageSize, out totalRecords, string.Empty);
        }
        /// <summary>
        /// 从数据库获取对象列表
        /// </summary>
        /// <param name="query"> 查询语句, 只包含 select 和 where 部分 </param>
        /// <param name="orderColumns"> 排序属性名称, 用逗号分隔. </param>
        /// <param name="orderDesc">是否降序</param>
        /// <param name="pageIndex"></param>
        /// <param name="pageSize"></param>
        /// <param name="totalRecords"></param>
        /// <returns></returns>
        public IList<T> GetList(string query, string orderColumns, bool orderDesc, int pageIndex, int pageSize, out int totalRecords, string query_count)
        {
            totalRecords = 0;
            string strOrder = string.Empty;
            if (!string.IsNullOrEmpty(orderColumns))
            {
                strOrder = " order by " + orderColumns;
                if (orderDesc)
                    strOrder += " desc ";
            }
            IQuery qry = session.CreateQuery(query + strOrder);


            string queryCount = query_count;
            //todo 
            if (!string.IsNullOrEmpty(queryCount))
            { 
            //queryCount =phsu.StringHelper.BuildCountQuery(query);

            IQuery qryCount = session.CreateQuery(queryCount);
            totalRecords = (int)qryCount.UniqueResult<long>();
            }
            var returnList = qry.SetFirstResult((pageIndex - 1) * pageSize).SetMaxResults(pageSize).Future<T>().ToList();
            return returnList;
        }

        public System.Data.DataSet ExecuteSql(string pureSqlStatement)
        {
            //  ISQLQuery sqlQuery= session.CreateSQLQuery(pureSqlStatement);
            // System.Collections.IList result = sqlQuery.List();
            System.Data.DataSet ds = new System.Data.DataSet();
            using (SqlConnection conn = session.Connection as SqlConnection)
            {
                //conn.Open();

                var sqlDataAdapter = new SqlDataAdapter(pureSqlStatement, conn);

                sqlDataAdapter.Fill(ds);
            }
            return ds;
        }
    }
}
