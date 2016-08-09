using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
using NHibernate.Criterion;

namespace Dianzhu.DAL
{
    public class DALReceptionChatDD : NHRepositoryBase<ReceptionChatDD,Guid>,IDAL.IDALReceptionChatDD
    {
        /// <summary>
        /// 根据用户获取点点聊天表中未复制过的聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
        public IList<ReceptionChatDD> GetChatDDListByOrder(IList< DZMembership> fromList)
        {
            List<ReceptionChatDD> result = new List<ReceptionChatDD>();
            
            foreach (DZMembership from in fromList)
            {
                var list = Find(x => x.From.Id == from.Id && x.IsCopy == false);

                result.AddRange(list);
            }

            return result;
        }

        /// <summary>
        /// 根据数量获取用户列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
        public IList<ServiceOrder> GetCustomListDistinctFrom(int num)
        { 
                var cList = Session.QueryOver<ReceptionChatDD>().SelectList(
                list => list
                .Select(Projections.Distinct(Projections.Property<ReceptionChatDD>(x => x.ServiceOrder)))).Where(x => x.IsCopy == false).Take(num).List<ServiceOrder>();

               

                return cList;
          
        }
    }
}
