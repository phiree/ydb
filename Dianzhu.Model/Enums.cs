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
        None=99,

    }
    /// <summary>
    /// 支付方式
    /// </summary>
    public enum enum_PayType
    {
        Offline = 1,
        Online = 2,
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
        ReturnNotifyFromWePay=4,//微支付notify回调
        
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
    /// <summary>
    /// 订单状态
    /// </summary>
    public enum enum_OrderStatus
    {
        /// <summary>
        /// 搜索单
        /// </summary>
        Search = 17,

        #region 正常流程订单状态
        /// <summary>
        /// 草稿,未创建.
        /// </summary>
        Draft = 0,//草稿,未创建.
        /// <summary>
        /// 客服推出的待用户选择的订单
        /// </summary>
        DraftPushed = 33,
        /// <summary>
        /// 新订单，等待支付定金
        /// </summary>
        Created = 1,//已创建,待付款
        /// <summary>
        /// 等待后台确认订单是否到帐
        /// </summary>
        checkPayWithDeposit = 4,
        /// <summary>
        /// 已付款,等待上门服务
        /// </summary>
        Payed = 2,
        /// <summary>
        /// 商家已确认订单,协商金额
        /// </summary>
        Negotiate = 18,
        /// <summary>
        /// 商户已经提交新价格，等待用户确认
        /// </summary>
        isNegotiate = 34,
        /// <summary>
        /// 等待服务开始
        /// </summary>
        Assigned = 20,
        /// <summary>
        /// 服务已经开始
        /// </summary>
        Begin = 22,
        /// <summary>
        /// 商家确定服务完成（如果用户已经确认完成， 该状态跳过）
        /// </summary>
        isEnd = 24,
        /// <summary>
        /// 用户确定服务完成
        /// </summary>
        Ended = 26,
        /// <summary>
        /// 等待后台确认商议价格是否到帐
        /// </summary>
        checkPayWithNegotiate = 27,
        /// <summary>
        /// 用户支付尾款，订单完成
        /// </summary>
        Finished = 28,
        /// <summary>
        /// 用户已经评价
        /// </summary>
        Appraised = 30,
        /// <summary>
        /// 已过质保期
        /// </summary>
        EndWarranty = 36,
        #endregion

        #region 取消流程订单状态
        /// <summary>
        /// 已提交取消订单请求
        /// </summary>
        Canceled = 3,
        /// <summary>
        /// 服务提前时间未到，等待退还定金
        /// </summary>
        WaitingDepositWithCanceled = 31,
        /// <summary>
        /// 取消成功, 退款成功
        /// </summary>
        EndCancel = 38,
        #endregion

        #region 理赔流程订单状态
        /// <summary>
        /// 已提交退款请求
        /// </summary>
        Refund = 40,
        /// <summary>
        /// 等待商户审核退款请求
        /// </summary>
        WaitingRefund = 42,
        /// <summary>
        /// 商户审核后，确认退款，等待退还金额
        /// </summary>
        isRefund = 44,
        /// <summary>
        /// 商户审核后，商户驳回退款请求
        /// </summary>
        RejectRefund = 46,
        /// <summary>
        /// 商户审核后，要求支付赔偿金
        /// </summary>
        AskPayWithRefund = 48,
        /// <summary>
        /// 等待用户支付赔偿金
        /// </summary>
        WaitingPayWithRefund = 50,
        /// <summary>
        /// 等待后台确认退款赔偿金是否到帐
        /// </summary>
        checkPayWithRefund = 51,
        /// <summary>
        /// 理赔完成
        /// </summary>
        EndRefund = 52,
        #endregion

        #region 介入流程订单状态
        /// <summary>
        /// 官方介入并等待提交凭证
        /// </summary>
        InsertIntervention = 54,
        /// <summary>
        /// 等待官方处理
        /// </summary>
        HandleWithIntervention = 56,
        /// <summary>
        /// 已确认退款，等待退还金额
        /// </summary>
        NeedRefundWithIntervention = 58,
        /// <summary>
        /// 要求支付赔偿金
        /// </summary>
        NeedPayWithIntervention = 60,
        /// <summary>
        /// 等待后台确认介入要求的赔偿金是否到帐
        /// </summary>
        checkPayWithIntervention = 61,
        /// <summary>
        /// 官方介入完成
        /// </summary>
        EndIntervention = 62,
        #endregion

        #region 投诉流程
        /// <summary>
        /// 已提交投诉申请
        /// </summary>
        Complaints = 64,
        /// <summary>
        /// 等待官方审核投诉
        /// </summary>
        WaitingComplaints = 66,
        /// <summary>
        /// 投诉完成
        /// </summary>
        EndComplaints = 68,
        #endregion

        /// <summary>
        /// 固定时间内未支付赔偿金，强制终止
        /// </summary>
        ForceStop = 70,
        
        /// <summary>
        /// 未知订单类型
        /// </summary>
        Unknow = 100,
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
        customer=1,
        /// <summary>
        /// 商户
        /// </summary>
        business=2,
        /// <summary>
        /// 客服
        /// </summary>
        customerservice=4,
        /// <summary>
        /// 管理员
        /// </summary>
        admin=8,
        /// <summary>
        /// 员工
        /// </summary>
        staff=16,
            agent=32
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
        //todo:发布全新版本时需删除
        #region 待删除
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
        #endregion

        /// <summary>
        /// 模拟用户客户端
        /// </summary>
        YDBan_DemoClient = 40,

        /// <summary>
        /// 用户版
        /// </summary>
        YDBan_User=50,
        /// <summary>
        /// 商户版
        /// </summary>
        YDBan_Store=52,
        /// <summary>
        /// 客服工具
        /// </summary>
        YDBan_CustomerService=54,
        /// <summary>
        /// IM服务
        /// </summary>
        YDBan_IMServer=56,
        /// <summary>
        /// 点点
        /// </summary>
        YDBan_DianDian=58,

        /// <summary>
        /// 未知资源名称
        /// </summary>
        Unknow = 100,
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
        Wait_Buyer_Pay = 0,//交易创建，等待买家付款。
        Trade_Success = 1,//支付成功，且可对该交易做操作，如：多级分润、退款等。
        Trade_Closed = 2,//在指定时间内未支付时关闭的交易；在交易完成全额退款成功时关闭的交易。
        Trade_Finished = 3,//交易成功且结束，即不可再做任何操作。
        Fail=4,//交易失败
    }

    /// <summary>
    /// 退款的状态
    /// </summary>
    public enum enum_RefundStatus
    {
        Success=1,
        Fail=2,
    }
}
