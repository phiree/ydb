using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;

namespace Dianzhu.ApplicationService.Remind
{
    public class RemindService: IRemindService
    {
        BLL.BLLServiceOrderRemind bllremind;
        public RemindService(BLL.BLLServiceOrderRemind bllremind)
        {
            this.bllremind = bllremind;
        }

        /// <summary>
        /// 条件读取提醒
        /// </summary>
        /// <param name="filter"></param>
        /// <param name="remind"></param>
        /// <returns></returns>
        public IList<remindObj> GetReminds(common_Trait_Filtering filter, common_Trait_RemindFiltering remind)
        {
            IList<Model.ServiceOrderRemind> listremind = null;
            int[] page = utils.CheckFilter(filter);
            listremind = bllremind.GetReminds(page[0], page[1], utils.CheckGuidID(remind.orderID, "orderID"), utils.CheckGuidID(filter.userID, "orderID"), utils.CheckDateTime(remind.startTime,"yyyyMMdd", "startTime"), utils.CheckDateTime(remind.endTime, "yyyyMMdd", "startTime"));
            if (listremind == null)
            {
                throw new Exception(Dicts.StateCode[4]);
            }
            IList<remindObj> remindobj = Mapper.Map<IList<Model.ServiceOrderRemind>, IList<remindObj>>(listremind);
            return remindobj;

        }

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <param name="remind"></param>
        /// <returns></returns>
        public countObj GetRemindsCount(common_Trait_RemindFiltering remind)
        {
            countObj c = new countObj();
            c.count = bllremind.GetRemindsCount(utils.CheckGuidID(remind.orderID, "orderID"), Guid.Empty, utils.CheckDateTime(remind.startTime, "yyyyMMdd", "startTime"), utils.CheckDateTime(remind.endTime, "yyyyMMdd", "startTime")).ToString();
            return c;
        }

        /// <summary>
        /// 根据ID获取提醒
        /// </summary>
        /// <param name="remindID"></param>
        /// <returns></returns>
        public remindObj GetRemindById(string remindID)
        {
            Model.ServiceOrderRemind remind = bllremind.GetRemindById(utils.CheckGuidID(remindID, "remindID"));
            remindObj remindobj = Mapper.Map<Model.ServiceOrderRemind, remindObj>(remind);
            return remindobj;
        }

        /// <summary>
        /// 根据ID删除提醒
        /// </summary>
        /// <param name="remindID"></param>
        /// <returns></returns>
        public object DeleteRemindById(string remindID)
        {
            Guid guid = utils.CheckGuidID(remindID, "remindID");
            Model.ServiceOrderRemind remind = bllremind.GetRemindById(guid);
            if (remind == null)
            {
                throw new Exception("该提醒不存在！");
            }
            bllremind.DeleteRemindById(remind);
            return "删除成功！";
        }
    }
}
