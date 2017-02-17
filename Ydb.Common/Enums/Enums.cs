using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;

namespace Ydb.Common
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
        Deposit = 1,//订金
        FinalPayment = 2,//尾款
        Compensation = 3,//赔偿金
        None = 99,

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
        None = 0,
        ApplyFromUser = 1,//用户发起支付请求
        ResultReturnFromAli = 2,//支付宝 return回调
        ResultNotifyFromAli = 3,//支付宝 notify回调
        ReturnNotifyFromWePay = 4,//微支付notify回调

    }
    /// <summary>
    /// 支付接口
    /// </summary>
    public enum enum_PayAPI
    {
        None = 0,
        AlipayWeb = 1,
        
        Wechat = 2,
        AliBatch = 3,
            AlipayApp = 4,
            AlipayBatch=5
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
        ALL,//全部
        Nt,//未完成的服务
        De//完成的服务
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

        Chat,//聊天信息,需要持久化
        Notice,//推送信息,不需要持久化
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
        customer = 1,
        /// <summary>
        /// 商户
        /// </summary>
        business = 2,
        /// <summary>
        /// 客服
        /// </summary>
        customerservice = 4,
        /// <summary>
        /// 管理员
        /// </summary>
        admin = 8,
        /// <summary>
        /// 员工
        /// </summary>
        staff = 16,
        /// <summary>
        /// 代理商
        /// </summary>
        agent = 32,
        /// <summary>
        /// 点点
        /// </summary>
        diandian = 64,
        /// <summary>
        /// 通知服务器
        /// </summary>
        notify = 128,
        /// <summary>
        /// openfire服务器
        /// </summary>
        openfire = 256
    }

    /// <summary>
    /// 聊天记录类型，接待方是平台客服还是商家客服
    /// </summary>
    public enum enum_ChatTarget
    {
        /// <summary>
        /// 与平台客服聊天类型
        /// </summary>
        cer = 1,
        /// <summary>
        /// 与商家客服聊天类型
        /// </summary>
        store = 2,
        /// <summary>
        /// 查询所有的类型
        /// </summary>
        all = 4,
        /// <summary>
        /// 用户
        /// </summary>
        user = 8,
        /// <summary>
        /// 系统
        /// </summary>
        system = 16,
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
        /// <summary>
        /// 用户版
        /// </summary>
        YDBan_User = 50,
        /// <summary>
        /// 商户版
        /// </summary>
        YDBan_Store = 52,
        /// <summary>
        /// 客服工具
        /// </summary>
        YDBan_CustomerService = 54,
        /// <summary>
        /// IM服务
        /// </summary>
        YDBan_IMServer = 56,
        /// <summary>
        /// 点点
        /// </summary>
        YDBan_DianDian = 58,
        /// <summary>
        /// 员工
        /// </summary>
        YDBan_Staff = 60,

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
        original = 0,

        //第三方登录用户
        WeChat = 1,
        SinaWeiBo = 2,
        TencentQQ = 3,
    }

    /// <summary>
    /// 注册平台
    /// </summary>
    public enum enum_PlatFormType
    {
        /// <summary>
        /// 系统
        /// </summary>
        system = 0,

        //第三方登录用户
        WeChat = 1,
        SinaWeiBo = 2,
        TencentQQ = 3,
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
        Fail = 4,//交易失败
        None = 5,
    }

    /// <summary>
    /// 退款的状态
    /// </summary>
    public enum enum_RefundStatus
    {
        Success = 1,
        Fail = 2,
    }

    /// <summary>
    /// 访问接口的客户端类型，主要区分js客户端和非js客户端
    /// </summary>
    public enum ApplicationTypes
    {
        JavaScript = 0,
        NativeConfidential = 1
    };

    /// <summary>
    /// 投诉目标类型
    /// </summary>
    public enum enum_ComplaintTarget
    {
        /// <summary>
        /// 客服
        /// </summary>
        customerService,
        /// <summary>
        /// 店铺
        /// </summary>
        //store
        /// <summary>
        /// 与平台客服聊天类型
        /// </summary>
        //cer,
        /// <summary>
        /// 与商家客服聊天类型
        /// </summary>
        store,
        /// <summary>
        /// 用户
        /// </summary>
        user,
    }

    /// <summary>
    /// 投诉状态，就是订单中的投诉状态
    /// </summary>
    public enum enum_ComplaintStatus
    {
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
    }


    /// <summary>
    /// 平台标识类型
    /// </summary>
    public enum enum_appName
    {
        /// <summary>
        /// IOS用户版 IOS_User
        /// security_key:NoJBn3npJIvre2fC2SQL5aQGNB/3l73XXSqNZYdY6HU
        ///UI3f4185e97b3E4a4496594eA3b904d60d,
        /// </summary>
        IOS_Customer,
        /// <summary>
        /// IOS商户版 IOS_Merchant
        /// security_key:h7lVzFNKU5Nlp7iCSVIyfs2bEgCzA2aFnQsJwia8utE
        ///MI354d5aaa55Ff42fba7716C4e70e015f2,
        /// </summary>
        IOS_Merchant,
        /// <summary>
        /// IOS客服版 IOS_CustomerService
        /// security_key:Ce6QgbBcwFxbB9yCAI5BEJ95L7RJi8AeQ9REYxvp79Q
        ///CI5baFa6180f5d4b9D85026073884c3566,
        /// </summary>
        IOS_CustomerService,
        /// <summary>
        /// Android用户版 Android_User
        /// security_key:WDcajjuVXA6TToFfm1MWhFFgn6bsXTt8VNsGLjcqGMg
        ///UA811Cd5343a1a41e4beB35227868541f8,
        /// </summary>
        Android_Customer,
        /// <summary>
        /// Android商户版 Android_Merchant
        /// security_key:3xhBie885/2f6dWg4O5rh7bUpcsgldeQxnwsx6f9638
        ///MAA6096436548346B0b70ffb58A9b0426d,
        /// </summary>
        Android_Merchant,
        /// <summary>
        /// Android客户版 Android_CustomerService
        /// security_key:suSjG+pPCu0gwXOqamNdp0zE3sY29vcHJHe1S429hNU
        ///CA660838f88147463CAF3a52bae6c30cbd,
        /// </summary>
        Android_CustomerService
        /// <summary>
        /// js客户端
        /// security_key:FJXTdZVLhmFLHO5M3Xweo5kHRmLH3qFdRzLyGFZLeBc
        ///JS1adBF8cbaf594d1ab2f1A68755e70440,
        /// </summary>
    }

    /// <summary>
    /// 理赔动作类型
    /// </summary>
    public enum enum_RefundAction
    {
        /// <summary>
        /// 提交理赔请求
        /// </summary>
        submit = 0,
        /// <summary>
        /// 店铺同意理赔要求
        /// </summary>
        refund = 4,
        /// <summary>
        /// 店铺拒绝理赔
        /// </summary>
        reject = 1,
        /// <summary>
        /// 店铺要求支付赔偿金
        /// </summary>
        askPay = 2,
        /// <summary>
        /// 用户同意商户处理
        /// </summary>
        agree = 8,
        /// <summary>
        /// 用户放弃理赔
        /// </summary>
        cancel = 16,
        /// <summary>
        /// 用户要求官方介入
        /// </summary>
        intervention = 32,
        None=33
    }

    /// <summary>
    /// 筛选类型
    /// </summary>
    public enum enum_FilterType
    {
        /// <summary>
        /// 按距离筛选
        /// </summary>
        ByDistance,
        /// <summary>
        /// 按价格筛选
        /// </summary>
        ByPrice,
        /// <summary>
        /// 按评价筛选
        /// </summary>
        ByApprise
    }
}
