using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.Config;
namespace Dianzhu.Api.Model
{
    public class RespDataSHM_snapshotObj
    {
        public RespDataSHM_maxOrderDic maxOrderDic { get; set; }
        public RespDataSHM_workTimeDic workTimeDic { get; set; }
        public RespDataSHM_orderObjDic orderObjDic { get; set; }
    }

    public class RespDataSHM_maxOrderObj
    {
        public string date { get; set; }
        public string maxOrder { get; set; }
        public string reOrder { get; set; }
    }

    public class RespDataSHM_maxOrderDic
    {
        public IDictionary<string, IList<RespDataSHM_maxOrderObj>> maxOrderDic { get; set; }
    }

    public class RespDataSHM_workTimeDic
    {
        public IDictionary<string, IList<RespDataDRM_workTimeObj>> workTimeDic { get; set; }
    }

    public class RespDataSHM_orderObjDic
    {
        public IDictionary<string,IList<RespDataORM_orderObj>> orderObjDic { get; set; }
    }

    public class ReqDataSHM001007
    {
        public string stratTime { get; set; }
        public string endTime { get; set; }
        public string type { get; set; }
    }

    public class RespDataSHM001007
    {
        public RespDataSHM_snapshotObj snapshotObj { get; set; }
    }
}
