using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;
 
using System.Collections;
 

namespace Dianzhu.DAL
{
    public interface IDALReceptionChat 

    {



            void Save(ReceptionChat chat);

        /// <summary>
        /// 根据订单获取聊天记录列表
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
          IList<ReceptionChat> GetChatByOrder(ServiceOrder order);

        /// <summary>
        /// 查询是否有该订单的聊天记录
        /// </summary>
        /// <param name="guid"></param>
        /// <returns></returns>
          IList<ReceptionChat> FindChatByOrder(ServiceOrder order);
    }
}
