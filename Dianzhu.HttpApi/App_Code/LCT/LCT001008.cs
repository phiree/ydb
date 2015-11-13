using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Dianzhu.BLL;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

/// <summary>
/// 上传经纬度
/// </summary>
public class ResponseLCT001008 : BaseResponse
{
    public ResponseLCT001008(BaseRequest request) : base(request) { }
    protected override void BuildRespData()
    {
        ReqDataLCT001008 requestData = this.request.ReqData.ToObject<ReqDataLCT001008>();

        DZMembershipProvider p = new DZMembershipProvider();
        string raw_id = requestData.userID;

        try
        {
            DZMembership member;
            bool validated = new Account(p).ValidateUser(new Guid(raw_id), requestData.pWord, this, out member);
            if (!validated)
            {
                return;
            }

            try
            {
                RespDataLCT001008 respData = new RespDataLCT001008();
                Area area = new Area();
                area.Name = "海口";
                area.Code = "460100";

                RespDataLCT001008_locationObj locationObj = new RespDataLCT001008_locationObj().Adap(area);
                string spell = GetChineseSpell(locationObj.name);
                locationObj.key = spell.Substring(0,1);

                respData.locationObj = locationObj;

                this.RespData = respData;
                this.state_CODE = Dicts.StateCode[0];
            }
            catch (Exception ex)
            {
                this.state_CODE = Dicts.StateCode[2];
                this.err_Msg = ex.Message;
                return;
            }
        }
        catch (Exception e)
        {
            this.state_CODE = Dicts.StateCode[1];
            this.err_Msg = e.Message;
            return;
        }


    }

    public class ReqDataLCT001008
    {
        public string userID { get; set; }
        public string pWord { get; set; }
        public string longitude { get; set; }
        public string latitude { get; set; }
    }

    public class RespDataLCT001008
    {
        public RespDataLCT001008_locationObj locationObj { get; set; }

    }
    public class RespDataLCT001008_locationObj
    {
        public string name { get; set; }
        public string key { get; set; }
        public string code { get; set; }
        public RespDataLCT001008_locationObj Adap(Area area)
        {
            this.name = area.Name;
            this.key=string.Empty;
            this.code = area.Code;

            return this;
        }
    }

    /// <summary>
    /// 只转换每个汉字首字母（大写）
    /// </summary>
    /// <param name="strText"></param>
    /// <returns></returns>
    public static string GetChineseSpell(string strText)
    {
        int len = strText.Length;
        string myStr = "";
        for (int i = 0; i < len; i++)
        {
            myStr += getSpell(strText.Substring(i, 1));
        }
        return myStr;
    }

    /// <summary>
    /// 获得第一个汉字的首字母（大写）；
    /// </summary>
    /// <param name="cnChar"></param>
    /// <returns></returns>
    public static string getSpell(string cnChar)
    {
        byte[] arrCN = System.Text.Encoding.Default.GetBytes(cnChar);
        if (arrCN.Length > 1)
        {
            int area = (short)arrCN[0];
            int pos = (short)arrCN[1];
            int code = (area << 8) + pos;
            int[] areacode = { 45217, 45253, 45761, 46318, 46826, 47010, 47297, 47614, 48119, 48119, 49062, 49324, 49896, 50371, 50614, 50622, 50906, 51387, 51446, 52218, 52698, 52698, 52698, 52980, 53689, 54481 };
            for (int i = 0; i < 26; i++)
            {
                int max = 55290;
                if (i != 25) max = areacode[i + 1];
                if (areacode[i] <= code && code < max)
                {
                    return System.Text.Encoding.Default.GetString(new byte[] { (byte)(65 + i) });
                }
            }
            return "*";
        }
        else return cnChar;
    }

}