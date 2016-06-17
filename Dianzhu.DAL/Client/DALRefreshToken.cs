using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NHibernate;

namespace Dianzhu.DAL
{
    public class DALRefreshToken : DALBase<Model.RefreshToken>
    {
        public DALRefreshToken()
        {

        }

        /// <summary>
        /// 添加新的RefreshToken
        /// </summary>
        /// <param name="token"></param>
        public bool AddRefreshToken(Model.RefreshToken token)
        {
            using (var x = Session.Transaction)
            {
                x.Begin();

                //判断某客户端的某个用户是否已经生成RefreshToken,若存在就先删除、后添加
                var existingToken = Session.QueryOver<Model.RefreshToken>().Where(r => r.Subject == token.Subject && r.ClientId == token.ClientId).SingleOrDefault();
                if (existingToken != null)
                {
                    Session.Delete(existingToken);
                }

                //Session.Save(token);
                bool b = true;
                try
                {
                    Session.Save(token);
                }
                catch { b = false; }
                x.Commit();
                return b;
            }
        }

        /// <summary>
        /// 根据Id删除RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        public void RemoveRefreshToken(string refreshTokenId)
        {
            var existingToken = Session.QueryOver<Model.RefreshToken>().Where(r => r.Id == refreshTokenId).SingleOrDefault();
            Delete(existingToken);
        }

        /// <summary>
        /// 删除RefreshToken
        /// </summary>
        /// <param name="refreshtoken"></param>
        public void RemoveRefreshToken(Model.RefreshToken refreshtoken)
        {
            Delete(refreshtoken);
        }

        /// <summary>
        /// 根据Id获取RefreshToken
        /// </summary>
        /// <param name="refreshTokenId"></param>
        /// <returns></returns>
        public Model.RefreshToken FindRefreshToken(string refreshTokenId)
        {
            Model.RefreshToken refreshtoken = null;
            IQuery query = Session.CreateQuery("select m from  RefreshToken as m where Id='" + refreshTokenId + "'");
            Action a = () => { refreshtoken = query.UniqueResult<Model.RefreshToken>(); };
            TransactionCommit(a);
            return refreshtoken;
        }

        /// <summary>
        /// 获取所有的RefreshToken
        /// </summary>
        /// <returns></returns>
        public IList<Model.RefreshToken> GetAllRefreshTokens()
        {
            IQuery query = Session.CreateQuery("select r from RefreshToken r ");
            return query.List<Model.RefreshToken>();
        }
    }
}
