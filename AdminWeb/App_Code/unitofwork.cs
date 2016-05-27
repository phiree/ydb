using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

/// <summary>
/// unitofwork 的摘要说明
/// </summary>
public class unitofwork
{
    NHibernate.ISession session;
    public unitofwork()
    {
        session = new Dianzhu.DAL.HybridSessionBuilder().GetSession();
    }
    NHibernate.ITransaction itra;
    public void Start()
    {
        itra = session.BeginTransaction();
    }
    public void Commit()
    {
        itra.Commit();
    }
}