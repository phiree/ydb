using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;
using Dianzhu;
namespace Dianzhu.DAL
{
    public class DALMembership : IDAL.IMembership
    {
        IDAL.IDALBase<Model.DZMembership> dalBase = null;
        public IDAL.IDALBase<Model.DZMembership> DalBase
        {
            get { return new DalBase<Model.DZMembership>(); }
            set { dalBase = value; }
        }

        public void CreateUser(Model.DZMembership user)
        {
            DalBase.Save(user);
        }

        public void CreateUpdateMember(Model.DZMembership member)
        {

            DalBase.SaveOrUpdate(member);
             
        }

        public bool ValidateUser(string username, string password)
        {
            //  User user = session.QueryOver<User>(x=>x.User);
            bool result = false;
            IQuery query = DalBase.Session.CreateQuery("select u from DZMembership as u where u.Name='" + username + "' and u.Password='" + password + "'");
            int matchLength = query.Future<Model.DZMembership>().ToArray().Length;

            if (matchLength == 1) { result = true; }
            if (matchLength > 1)
            {
                throw new Exception("账户重名,拒绝登录");
            }

            return result;
        }

        public Model.DZMembership GetMemberByName(string username)
        {
            if (string.IsNullOrEmpty(username)) return null;
            IQuery query =DalBase.Session.CreateQuery("select m from  DZMembership as m where Name='" + username + "'");
            var temp = query.FutureValue<Model.DZMembership>();
            var user = temp.Value;
            return user;
        }

         
        public Model.DZMembership GetMemberById(Guid memberId)
        {
            IQuery query = DalBase.Session.CreateQuery("select m from  DZMembership as m where Id='" + memberId + "'");
            Model.DZMembership member = query.FutureValue<Model.DZMembership>().Value;
            return member;
        }

        public IList<Model.DZMembership> GetAllUsers()
        {
            IQuery query = DalBase.Session.CreateQuery("select u from DZMembership u ");
            return query.Future<Model.DZMembership>().ToList();
        }

        public IList<Model.DZMembership> GetAllUsers(int pageIndex, int pageSize, out long totalRecord)
        {
            IQuery qry = DalBase.Session.CreateQuery("select u from DZMembership u ");
            IQuery qryTotal = DalBase.Session.CreateQuery("select count(*) from DZMembership u ");
            List<Model.DZMembership> memList = qry.Future<Model.DZMembership>().Skip(pageIndex * pageSize).Take(pageSize).ToList();
            totalRecord = qryTotal.FutureValue<long>().Value;
            return memList;
        }

        
        //public Model.ScenicAdmin GetScenicAdmin(Guid id)
        //{
        //    IQuery query = session.CreateQuery("select sa from ScenicAdmin sa where sa.Membership.Id='" + id + "'");
        //    IFutureValue<Model.ScenicAdmin> sa = query.FutureValue<Model.ScenicAdmin>();
        //    if (sa == null) return null;
        //    return sa.Value;
        //}

         


      
        public void ChangePassword(Model.DZMembership member)
        {
            using (var x = DalBase.Session.Transaction)
            {
                x.Begin();
                DalBase.Session.Update(member);
                x.Commit();
            }

        }


        
    }
}