using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Dianzhu.Model.Enums
{
    public enum enum_ImageType
    {
        Business_License,//商家营业执照
        Business_License_B,//营业执照
        Business_Show,//商家展示图片
        Business_ChargePersonIdCard,//负责人证件照片
        Business_Avatar,//店铺头像
        Staff_Avatar,//职员头像
    }
    public enum PayType
    {
        Offline = 1,
        Online = 2,
        None = 4,
    }
    /// <summary>
    /// 计费单位
    /// </summary>
    public enum enum_ChargeUnit
    {
        Hour,   //每小时
        Day,    //每天
        Times,  //每次
        Month   //每月
    }

    public enum enum_ServiceMode
    {
        ToHouse,//上门
        NotToHouse,//不上门
    }
    public enum enum_IDCardType
    {
        身份证 = 0,
        其他 = 1,


    }

    public enum enum_OrderStatus
    {
        /// <summary>
        /// 草稿,未创建.
        /// </summary>
        Draft=0,//草稿,未创建.

        Search=17,//搜索单

        //未完成订单状态
        Created=1,//已创建,待付款
        Payed=2,//已付款
        Canceled=3,//已取消待退款
        Assigned=4,//已安排工作人员
        isCancel=5,//客户已取消 等待撤销工作人员分配.

        //已完成订单状态5
        Finished=6,//已完成
        Aborded=7,//已中止
        Appraise=8,//评价

        Wt=9,//Wait 
        Ry=10,//Ready 
        An=11,//Actionţ 
        Py=12,//Pay 
        Ee=13,//Evaluate 
        Nu=14,//Can
        Dy=15,//Delay 
        Ed=16,//End
    }
    public enum enum_OrderSearchType
    {
        Nt,//未完成的服务
        De,//完成的服务
        ALL//全部
    }
    public enum enum_CashTicketSearchType
    {
        /// <summary>
        /// 尚未使用的
        /// </summary>
        Nt,
        /// <summary>
        /// 已经使用的
        /// </summary>
        Us,
        /// <summary>
        /// 已经过期的
        /// </summary>
        Ps,
        /// <summary>
        ///所有
        /// </summary>
        All
    }
    /// <summary>
    /// 聊天类型
    /// </summary>
    public enum enum_ChatType
    {
        /// <summary>
        /// 业务流程需要IM完成的工作,
        /// 不需要IM完成的 比如, 用户取消订单,
        /// 申请退款等,通过http接口完成.
        ///  
        /// 
        /// </summary>
        Text,
        Media,//包含多媒体链接的消息
        Notice,//客服通知
        PushedService,// 推送的服务
        ConfirmedService,//被确认的服务
        Order,//订单.包含支付链接
        BeginPay,//开始支付消息.
        ReAssign,//重新分配客服
        UserStatus,//用户状态
    }
    /// <summary>
    /// 订单范围类型..这个名字好拗口.
    /// </summary>
    public enum enum_ServiceScopeType
    {
        /// <summary>
        /// 系统内服务,系统内用户
        /// </summary>
        ISIM, 
        /// <summary>
        /// 系统内服务,系统外用户
        /// </summary>
        ISOM, 
        /// <summary>
        /// 系统外服务,系统内用户
        /// </summary>
        OSIM, 
        /// <summary>
        /// 系统外服务,系统外用户
        /// </summary>
        OSOM, 
    }

    /// <summary>
    /// 用户类型
    /// </summary>
    public enum enum_UserType
    {
        /// <summary>
        /// 普通用户
        /// </summary>
        customer,
        /// <summary>
        /// 商户
        /// </summary>
        business,
        /// <summary>
        /// 客服
        /// </summary>
        customerservice
    }

    /// <summary>
    /// 聊天记录类型，接待方是平台客服还是商家客服
    /// </summary>
    public enum enum_ChatTarget
    {
        /// <summary>
        /// 与平台客服聊天类型
        /// </summary>
        cer,
        /// <summary>
        /// 与商家客服聊天类型
        /// </summary>
        store,
        /// <summary>
        /// 查询所有的类型
        /// </summary>
        all
    }

    /// <summary>
    /// 订单种类：搜索、先付、后付
    /// </summary>
    public enum enum_ChatPayType
    {
        /// <summary>
        /// 搜索
        /// </summary>
        search,
        /// <summary>
        /// 先付
        /// </summary>
        payfirst,
        /// <summary>
        /// 后付
        /// </summary>
        paylater
    }
    /// <summary>
    /// 用户的在线状态
    /// </summary>
    public enum enum_UserStatus
    {
        /// <summary>
        /// 上线
        /// </summary>
        available,
        /// <summary>
        /// 离线
        /// </summary>
        unavailable
    }
}
