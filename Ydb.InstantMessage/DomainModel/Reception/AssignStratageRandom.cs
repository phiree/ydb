using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Ydb.InstantMessage.DomainModel.Reception
{
    /// <summary>
    /// 给客户分配相同区域的客服
    /// </summary>
    public class AssignStratageRandom : AssignStratage
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Ydb.InstantMessage.DomainModel.Reception.AssignStratageArea");
        static Random r = new Random();
        IReceptionSession receptionSession;
        public AssignStratageRandom(IReceptionSession receptionSession)
        {
            this.receptionSession = receptionSession;
        }

        /// <summary>
        /// 为一组客户分配客服
        /// </summary>
        /// <param name="customerList">待分配的客户列表</param>
        /// <param name="csListOnline">在线的客服列表</param>
        /// <returns>分配后的字典表,key 是客户,value 是客服</returns>
        //public override Dictionary<string, string> Assign(IList<string> customerList, IList<OnlineUserSession> csListOnline, string diandian)
        //{
        //    log.Debug("调用随机分配");
        //    Dictionary<string, string> assignList = new Dictionary<string, string>();
        //    //获取用户和 客服所在的区域..
            

        //    if (csListOnline == null || csListOnline.Count == 0)
        //    {
        //        //如果没有在线客服 怎么处理
        //        //throw new Exception("客服离线");

        //        //如果没有在线客服，分配给点点
        //        if (!receptionSession.IsUserOnline(diandian))
        //        {
        //            throw new Exception("点点不在线");
        //        }

        //        foreach (string customer in customerList)
        //        {
        //            assignList.Add(customer, diandian);
        //        }
        //    }
        //    else
        //    {
        //        IList<string> csList = csListOnline. Select(x => x.username).ToList();
        //        int totalCS = csListOnline.Count;
        //        foreach (string customer in customerList)
        //        {
        //            int i = r.Next(totalCS);
        //            var assignedCs = csList[i];
        //            assignList.Add(customer, assignedCs);
        //        }
        //    }

        //    return assignList;


        //}

        public override Dictionary<string, string> Assign(IList<MemberArea> customerList, IList<MemberArea> csListOnline, string diandian)
        {
            log.Debug("调用随机分配");
            Dictionary<string, string> assignList = new Dictionary<string, string>();
            foreach (var customer in customerList)
            {
                var csSameArea = csListOnline.Where(x => x.AreaId.Contains(customer.AreaId.Substring(0,4))).ToList();

                if ( csSameArea.Count == 0)
                {
                    //如果没有在线客服，分配给点点
                    if (!receptionSession.IsUserOnline(diandian))
                    {
                        throw new Exception("点点不在线");
                    }
                    assignList.Add(customer.MemberId, diandian);
                 
                }
                else
                {
                    
                    
                        int i = r.Next(csSameArea.Count);
                        var assignedCs = csSameArea[i];
                        assignList.Add(customer.MemberId,assignedCs.MemberId);
                     
                }

            }
            return assignList;


        }

    }

}
