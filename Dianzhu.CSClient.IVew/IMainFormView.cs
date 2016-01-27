using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Dianzhu.Model;
using System.Collections;

namespace Dianzhu.CSClient.IVew
{
 
    /// <summary>
    /// 当前界面的标志条目被修改(点击了另一个 订单按钮)
    /// </summary>
    /// <param name="serviceOrder">新激活的订单</param>
    //public delegate void IdentityItemActived(ServiceOrder serviceOrder);
    public delegate void IdentityItemActived(DZMembership dm);
    /// <summary>
    /// 消息发送之后
    /// </summary>
    public delegate void MessageSent();
    /// <summary>
    /// 发送字节数组的消息(例如,截屏图片)
    /// </summary>
    /// <param name="fileData"></param>
    /// <param name="domainType"></param>
    /// <param name="mediaType"></param>
    public delegate void MediaMessageSent(byte[] fileData, string domainType,string mediaType);
    /// <summary>
    /// 播放音频委托
    /// </summary>
    /// <param name="audioTag"></param>
    /// <param name="handler"></param>
    public delegate void AudioPlay(object audioTag,IntPtr handler);
    public delegate void PushExternalService();
    public delegate void PushInternalService(DZService service);
    public delegate void SearchService();
    public delegate void SelectService();
    public delegate void SendPayLink(ReceptionChat chat);
    public delegate void CreateOrder();
    public delegate void ViewClosed();
    public delegate void BeforeCustomerChanged();
    public delegate void OrderStateChanged();
    public delegate void CreateNewOrder();
    public delegate void NoticeSystem();
    public delegate void NoticeOrder();
    public delegate void NoticePromote();
    public delegate void NoticeCustomerService();
    /// <summary>
    /// 客服分配
    /// </summary>
    public delegate void ReAssign();

    /// <summary>
    /// 保存重新分配
    /// </summary>
    public delegate void SaveReAssign();

    /// <summary>
    /// 消息发送之后
    /// </summary>
    public delegate void MessageSentAndNew();

    /// <summary>
    /// 主界面接口定义
    /// </summary>
    public interface IMainFormView
    {
        
        IList<ReceptionChat> ChatLog { set; get; }
        void LoadOneChat( ReceptionChat chat);
       
        /// <summary>
        /// 搜索关键字.
        /// </summary>
        /// <summary>
        /// 设置按钮的样式.
        /// </summary>
        /// <param name="buttonText">按钮文本(等同于客户登录名)</param>
        /// <param name="buttonStyle">按钮样式</param>
        //void SetCustomerButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle);
        void SetCustomerButtonStyle(DZMembership dm, em_ButtonStyle buttonStyle);
        /// <summary>
        /// 增加一个按钮,并设置其样式
        /// </summary>
        /// <param name="order">按钮对应的订单</param>
        /// <param name="buttonStyle"></param>
        //void AddCustomerButtonWithStyle(ServiceOrder order, em_ButtonStyle buttonStyle);
        void AddCustomerButtonWithStyle(DZMembership dm, em_ButtonStyle buttonStyle);
        /// <summary>
        /// 按钮名称的前缀.
        /// </summary>
        string ButtonNamePrefix { get; set; }
        /// <summary>
        /// 发送文本消息
        /// </summary>
        event MessageSent SendMessageHandler;
        /// <summary>
        /// 发送多媒体消息
        /// </summary>
        event MediaMessageSent SendMediaHandler;
        event AudioPlay PlayAudio;
        event IdentityItemActived IdentityItemActived;
        /// <summary>
        /// 多媒体消息本地存储路径
        /// </summary>
        string LocalMediaSaveDir { get; set; }
        string SerachKeyword { get; set; }
        string MessageTextBox { get; set; }
        string CurrentCustomerService { set; }


        IList<DZService> SearchedService { get; set; }
        //外部服务
        string ServiceName { get; set; }
        string ServiceBusinessName { get; set; }
        string ServiceDescription { get; set; }
        string ServiceUnitPrice { get; set; }
        string ServiceUrl { get; set; }
        string ServiceTime { get; set; }
        string TargetAddress { get; set; }
        string OrderAmount { get; set; }
        string OrderNumber { get; set; }
        string OrderStatus { get; set; }
        bool CanEditOrder { get; set; }
        string Memo { get; set; }
        System.IO.Stream SelectedImageStream { get; }
        string SelectedImageName { get; }
        event PushExternalService PushExternalService;
        event PushInternalService PushInternalService;
        event SearchService SearchService;
        event SelectService SelectService;
        event SendPayLink SendPayLink;
        event CreateOrder CreateOrder;
        event ViewClosed ViewClosed;
        string CurrentServiceId { get; set; }
        /// <summary>
        /// 更改标志(顶部按钮)之前的事件.
        /// </summary>
        event BeforeCustomerChanged BeforeCustomerChanged;
        event OrderStateChanged OrderStateChanged;
        event CreateNewOrder CreateNewOrder;

        //测试,通知发送
        event NoticeCustomerService NoticeCustomerService;
        event NoticeOrder NoticeOrder;
        event NoticePromote NoticePromote;
        event NoticeSystem NoticeSystem;
        /// <summary>
        /// 显示系统通知.
        /// </summary>
        /// <param name="noticeContent"></param>
        void ShowNotice(string noticeContent);

        /// <summary>
        /// 显示登录冲突
        /// </summary>
        /// <param name="streamError"></param>
        void ShowStreamError(string streamError);

        event ReAssign ReAssign;

        /// <summary>
        /// 当前客服正在接待的客户列表
        /// </summary>
        IList<DZMembership> RecptingCustomList { set; }
        /// <summary>
        /// 客户重新分配列表
        /// </summary>
        IDictionary<DZMembership, string> RecptingCustomServiceList { get; set; }

        event SaveReAssign SaveReAssign;

        /// <summary>
        /// 通用消息显示
        /// </summary>
        /// <param name="msg"></param>
        void ShowMsg(string msg);

        /// <summary>
        /// 当前选择的服务
        /// </summary>
        DZService CurrentService { get; set; }

        /// <summary>
        /// 删除用户按钮
        /// </summary>
        /// <param name="btnName"></param>
        void RemoveCustomBtn(string cid);

        /// <summary>
        /// 删除用户按钮并清空聊天区域
        /// </summary>
        /// <param name="cid"></param>
        void RemoveCustomBtnAndClear(string cid);

        /// <summary>
        /// 发送消息并生成新订单
        /// </summary>
        event MessageSentAndNew MessageSentAndNew;
    }
}
