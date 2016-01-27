using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using NHibernate.Hql;
using System.Web.Security;
using System.Data.SqlClient;
using System.Data.Common;
using System.Data;
using MySql.Data.MySqlClient;

namespace Dianzhu.DAL
{
    /// <summary>
    ///  hibernat的 基础的CURD 实现.
    /// ISession 注入依赖
    ///     为何不直接实现:
    ///    1)session依赖于 数据库
    ///     2)该类是基类
    ///      3)如果不注入,那么,每个被初始化的子类 都会直接依赖数据库
    ///     4)无法单元测试.
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class DALBase<T> 
    {
        public DALBase()
        {
            session= new HybridSessionBuilder().GetSession();
        }
        public DALBase(string fortestonly)
        { }
        private ISession session = null;// new HybridSessionBuilder().GetSession();
         
    
        public ISession Session {
            get { return session; }
            set { session = value; }
        }
        public virtual void Delete(T o)
        {
            using (var t = session.BeginTransaction())
            {
                session.Delete(o); t.Commit();
            }
        }
        public virtual void Save(T o)
        {
            
            using (var t = session.BeginTransaction())
            {
                session.Save(o);

                t.Commit();
            }
           
            
        }
        public virtual void Save(T o, object id)
        {
            using (var t = session.BeginTransaction())
            {
                session.Save(o,id);

                t.Commit();
            }
         
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
            using (var t = session.BeginTransaction())
            {
                session.Update(o); t.Commit();
            }
        }
        public virtual void SaveOrUpdate(T o)
        {
            using (var t = session.BeginTransaction())
            {
                session.SaveOrUpdate(o); t.Commit();

            }


        }
        public virtual T GetOne(object id)
        {
            using (var t = session.BeginTransaction())
            {
                T r = session.Get<T>(id);

                if(t!=null)
                { 
                t.Commit();
                }
                return r;
            }
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
            using (var t = session.BeginTransaction())
            {
                T o;
                if (listT.Count == 1)
                {
                    o= listT[0];
                }
                else if (listT.Count == 0)
                {
                   o= default(T);
                }
                else
                {
                    string errmsg = "错误:GetOnByQuery应该只能返回一个值.现在有" + listT.Count + "个值返回.";

                    //NLibrary.NLogger.Logger.Error(errmsg);
                    o= listT[0];
                    // throw new Exception(errmsg);
                }
                t.Commit();
                return o;
            }
        }

        public virtual IList<T> GetAll<T>() where T : class
        {
            using (var t = session.BeginTransaction())
            {
                t.Commit();
                return session.QueryOver<T>().List();
            }
        }

        public IList<T> GetList(string query)
        {
            int totalRecords;
            return GetList(query, 0, 99999, out totalRecords);
        }
        public IList<T> GetList(IQueryOver<T, T> queryOver)
        {
            using (var t = session.BeginTransaction())
            {
                t.Commit();
                return queryOver.List();
            }
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
            using (var t = session.BeginTransaction())
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

                t.Commit();
                return returnList;
            }
        }

        public System.Data.DataSet ExecuteSql(string pureSqlStatement)
        {
            //  ISQLQuery sqlQuery= session.CreateSQLQuery(pureSqlStatement);
            // System.Collections.IList result = sqlQuery.List();
            System.Data.DataSet ds = new System.Data.DataSet();
            IDbConnection conn = session.Connection as DbConnection;
            
            if (conn.GetType() == typeof(SqlConnection))
            {
                var sqlDataAdapter = new SqlDataAdapter(pureSqlStatement,(SqlConnection) conn);

                sqlDataAdapter.Fill(ds);
            }
            else if (conn.GetType() == typeof(MySqlConnection))
            {
                var sqlDataAdapter = new MySqlDataAdapter(pureSqlStatement, (MySqlConnection)conn);

                sqlDataAdapter.Fill(ds);
            }
            else
            {
                throw new Exception("Unknow database type");
            }
            return ds;
        }
    }
}
