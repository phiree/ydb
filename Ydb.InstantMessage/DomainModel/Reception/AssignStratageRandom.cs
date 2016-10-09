using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
 
namespace Ydb.InstantMessage.DomainModel.Reception
{
    public class AssignStratageRandom : AssignStratage
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.BLL.AssignStratageRandom");
        static Random r = new Random();
        /// <summary>
        /// 为一组客户分配客服
        /// </summary>
        /// <param name="customerList">待分配的客户列表</param>
        /// <param name="csList">在线的客服列表</param>
        /// <returns>分配后的字典表,key 是客户,value 是客服</returns>
        public override Dictionary<string, string> Assign(IList<string> customerList, IList<string> csList, string diandian)
        {
            log.Debug("调用随机分配");
            Dictionary<string, string> assignList = new Dictionary<string, string>();
            if (csList.Count == 0)
            {
                //如果没有在线客服 怎么处理
                //throw new Exception("客服离线");

                //如果没有在线客服，分配给点点
                foreach (string customer in customerList)
                {
                    assignList.Add(customer, diandian);
                }
            }
            else
            {
                int totalCS = csList.Count;
                foreach (string customer in customerList)
                {
                    int i = r.Next(totalCS);
                    var assignedCs = csList[i];
                    assignList.Add(customer, assignedCs);
                }
            }

            return assignList;


        }
    }

}
