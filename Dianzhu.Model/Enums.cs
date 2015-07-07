using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Enums
{
    public enum ImageType
    {
        Business_Licence,//商家营业执照
        Business_Show,//商家展示图片
        Business_ChargePersonIdCard,//负责人证件照片
        Business_Avatar,//店铺头像
    }
    public enum PayType
    {
        Offline,
        Online_AliPay,
        Online_WechatPay
    }
    /// <summary>
    /// 计费单位
    /// </summary>
    public enum ChargeUnit
    {
        Hour,//每小时
        Day, //每天
        Times //每次
    }

    public enum ServiceMode
    {
        ToHouse,//上门
        NotToHouse,//不上门
    }
    public enum IDCardType
    {
        身份证 = 0,
        学生证 = 1,
        军官证 = 2,
        护照 = 3,
        其他 = 4
    }

    public enum OrderStatus
    {
        Wt,//Wait 
        Ry,//Ready 
        An,//Actionţ 
        Py,//Pay 
        Ee,//Evaluate 
        Nu,//Can
        Dy,//Delay 
        Ed,//End
    }



}
