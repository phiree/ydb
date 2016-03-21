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
        Business_ChargePersonIdCard_B,//负责人证件照片
        Business_Image,//商家后台上传图片
        Business_Avatar,//店铺头像
        Staff_Avatar,//职员头像
        User_Avatar,//用户头像
        Chat_Audio,
        Chat_Video,
        Chat_Image,
        Advertisement
    }
    /// <summary>
    /// 支付目标
    /// </summary>
    public enum enum_PayTarget
    {
        Deposit=1,//订金
        FinalPayment=2,//尾款
        Compensation=3,//赔偿金

    }
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum enum_PayType
    {
        Offline = 1,
        Online = 2,
        AliPay = 2,
        WePay = 3,
        None = 4,
    }
    /// <summary>
    /// 支付记录类型
    /// </summary>
    public enum enum_PaylogType
    {
        None=0,
        ApplyFromUser=1,//用户发起支付请求
        ResultReturnFromAli=2,//支付宝 return回调
        ResultNotifyFromAli=3,//支付宝 notify回调
    }
    /// <summary>
    /// 支付接口
    /// </summary>
    public enum enum_PayAPI
    {
        None=0,
        Alipay =1,
        Wechat=2
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

        DraftPushed=33,//已推送的草稿单

        //未完成订单状态
        Created=1,//已创建,待付款
        Payed=2,//已付款
        Canceled=3,//用户已发起取消请求
        CanceledDirectly,//订单已直接取消
       
        isCancel=5,//客户已取消 等待撤销工作人员分配.

        //已完成订单状态5
        
        Aborded=7,//已中止
 
        /// <summary>
        /// 商家已确认订单
        /// </summary>
        Negotiate=18,
        /// <summary>
        /// 等待服务开始
        /// </summary>
        Assigned = 20,
        /// <summary>
        /// 服务已经开始
        /// </summary>
        Begin =22,
        /// <summary>
        /// 商家确定服务完成（如果用户已经确认完成， 该状态跳过）
        /// </summary>
        IsEnd=24,
        /// <summary>
        /// 用户确定服务完成
        /// </summary>
        Ended=26,
        /// <summary>
        /// 用户支付尾款，订单完成
        /// </summary>
        Finished=28,
        /// <summary>
        /// 用户已经评价
        /// </summary>
        Appraised=30,

        WaitingDepositWithCanceled=31,//等待退还定金
        WaitingCancel=32,//用户申请取消订单, 超过时长,等待商家审核  .



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
        /// 系统内服务,系统内用户,默认值.
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

    /// <summary>
    /// 资源名
    /// </summary>
    public enum enum_XmppResource
    {
        #region 服务
        /// <summary>
        /// im消息通知
        /// </summary>
        YDBan_Win_IMServer = 0,
        /// <summary>
        /// 点点
        /// </summary>
        YDBan_Win_DianDian = 1,
        #endregion

        #region 客服工具
        /// <summary>
        /// IOS端客服工具
        /// </summary>
        YDBan_IOS_CustomerService = 11,
        /// <summary>
        /// 安卓端客服工具
        /// </summary>
        YDBan_Android_CustomerService = 12,
        /// <summary>
        /// Web端客服工具
        /// </summary>
        YDBan_Web_CustomerService = 13,
        /// <summary>
        /// PC端客服工具
        /// </summary>
        YDBan_Win_CustomerService = 14,
        #endregion

        #region 用户版
        /// <summary>
        /// IOS端用户
        /// </summary>
        YDBan_IOS_User = 21,
        /// <summary>
        /// 安卓端用户
        /// </summary>
        YDBan_Android_User = 22,
        /// <summary>
        /// Web端用户
        /// </summary>
        YDBan_Web_User = 23,
        /// <summary>
        /// PC端用户
        /// </summary>
        YDBan_Win_User = 24,
        #endregion

        #region 商户版
        /// <summary>
        /// IOS端商户
        /// </summary>
        YDBan_IOS_Merchant = 31,
        /// <summary>
        /// 安卓端商户
        /// </summary>
        YDBan_Android_Merchant = 32,
        /// <summary>
        /// Web端商户
        /// </summary>
        YDBan_Web_Merchant = 33,
        /// <summary>
        /// PC端商户
        /// </summary>
        YDBan_Win_Merchant = 34,
        #endregion

        /// <summary>
        /// 模拟用户客户端
        /// </summary>
        YDBan_Win_DemoClient=40,
    }

    /// <summary>
    /// 登录用户类型
    /// </summary>
    public enum enum_LoginType
    {
        /// <summary>
        /// 原生登录用户
        /// </summary>
        original=0,

        //第三方登录用户
        WeChat=1,
        SinaWeiBo=2,
        TencentQQ=3,
    }

    /// <summary>
    /// 支付状态
    /// </summary>
    public enum enum_PaymentStatus
    {
        WaitForPay=0,//等待支付
        Success=1,//支付成功
        Failed=2,//支付失败
    }
}
