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
        /// <param name="customer"></param>
        /// <returns></returns>
        public IList<remindObj> GetReminds(common_Trait_Filtering filter, common_Trait_RemindFiltering remind,Customer customer)
        {
            IList<Model.ServiceOrderRemind> listremind = null;
            Ydb.Common.Specification.TraitFilter filter1 = utils.CheckFilter(filter, "ServiceOrderRemind");
            listremind = bllremind.GetReminds(filter1, utils.CheckGuidID(remind.orderID, "orderID"), utils.CheckGuidID(customer.UserID, "customer.UserID"), utils.CheckDateTime(remind.startTime,"yyyyMMdd", "startTime"), utils.CheckDateTime(remind.endTime, "yyyyMMdd", "startTime"));
            if (listremind == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                return new List<remindObj>();
            }
            IList<remindObj> remindobj = Mapper.Map<IList<Model.ServiceOrderRemind>, IList<remindObj>>(listremind);
            return remindobj;

        }

        /// <summary>
        /// 统计投诉的数量
        /// </summary>
        /// <param name="remind"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public countObj GetRemindsCount(common_Trait_RemindFiltering remind, Customer customer)
        {
            countObj c = new countObj();
            c.count = bllremind.GetRemindsCount(utils.CheckGuidID(remind.orderID, "orderID"), utils.CheckGuidID(customer.UserID, "customer.UserID"), utils.CheckDateTime(remind.startTime, "yyyyMMdd", "startTime"), utils.CheckDateTime(remind.endTime, "yyyyMMdd", "startTime")).ToString();
            return c;
        }

        /// <summary>
        /// 根据ID获取提醒
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public remindObj GetRemindById(string remindID,Customer customer)
        {
            Model.ServiceOrderRemind remind = bllremind.GetRemindById(utils.CheckGuidID(remindID, "remindID"), utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (remind == null)
            {
                //throw new Exception(Dicts.StateCode[4]);
                //return null;
                throw new Exception("没有找到资源！");
            }
            remindObj remindobj = Mapper.Map<Model.ServiceOrderRemind, remindObj>(remind);
            return remindobj;
        }

        /// <summary>
        /// 根据ID删除提醒
        /// </summary>
        /// <param name="remindID"></param>
        /// <param name="customer"></param>
        /// <returns></returns>
        public object DeleteRemindById(string remindID, Customer customer)
        {
            Guid guid = utils.CheckGuidID(remindID, "remindID");
            Model.ServiceOrderRemind remind = bllremind.GetRemindById(guid, utils.CheckGuidID(customer.UserID, "customer.UserID"));
            if (remind == null)
            {
                throw new Exception("该提醒不存在！");
            }
            bllremind.DeleteRemindById(remind);
            return new string[] { "删除成功！" };
        }
    }
}
