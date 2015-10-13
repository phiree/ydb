using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
 
using Dianzhu.Model;
namespace Dianzhu.CSClient.IVew
{
    public delegate void IdentityItemActived(ServiceOrder serviceOrder);
    public delegate void MessageSent();
    public delegate void MediaMessageSent(string domainType,string mediaType);
    
    public delegate void AudioPlay(object audioTag,IntPtr handler);
    public delegate void PushExternalService();
    public delegate void PushInternalService(DZService service);
    public delegate void SearchService();
    public delegate void SendPayLink(ReceptionChat chat);
    public delegate void CreateOrder();
    public delegate void ViewClosed();
    public delegate void BeforeCustomerChanged();
    public delegate void OrderStateChanged();

    public interface IMainFormView
    {
        #region Chat
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
        void SetCustomerButtonStyle(ServiceOrder order, em_ButtonStyle buttonStyle);
        /// <summary>
        /// 增加一个客户按钮,并设置样式
        /// </summary>
        /// <param name="buttonText"></param>
        /// <param name="buttonStyle"></param>
        void AddCustomerButtonWithStyle(ServiceOrder order, em_ButtonStyle buttonStyle);
        string ButtonNamePrefix { get; set; }
        event MessageSent SendMessageHandler;
        event MediaMessageSent SendMediaHandler;
        event AudioPlay PlayAudio;
        event IdentityItemActived IdentityItemActived;
        #endregion
        string LocalMediaSaveDir { get; set; }
        string SerachKeyword { get; set; }
        string MessageTextBox { get; set; }
        string CurrentCustomerName { get; set; }


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
        string Memo { get; set; }
        System.IO.Stream SelectedImageStream { get; }
        string SelectedImageName { get; }
        event PushExternalService PushExternalService;
        event PushInternalService PushInternalService;
        event SearchService SearchService;
        event SendPayLink SendPayLink;
        event CreateOrder CreateOrder;
        event ViewClosed ViewClosed;
        
        event BeforeCustomerChanged BeforeCustomerChanged;
        event OrderStateChanged OrderStateChanged;
    }
}
