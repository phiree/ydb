using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 

namespace Dianzhu.DAL
{
    public interface IDALReceptionChatDD  

    {


            void Save(ReceptionChatDD chat);

        /// <summary>
        /// 根据用户获取点点聊天表中未复制过的聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
          IList<ReceptionChatDD> GetChatDDListByOrder(IList<DZMembership> fromList);

        /// <summary>
        /// 根据数量获取用户列表
        /// </summary>
        /// <param name="num"></param>
        /// <returns></returns>
          IList<ServiceOrder> GetCustomListDistinctFrom(int num);
    }
}
