using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Dianzhu.Model;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using Dianzhu.CSClient.IInstantMessage;
using Dianzhu.BLL;
using Dianzhu.Model.Enums;
using Dianzhu.DAL;
namespace Dianzhu.CSClient.Presenter
{
     /// <summary>
     ///  和组件无关的逻辑
     ///  1)客服登录之后,从点点拉取用户
     /// </summary>
    public  class PMain
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.PMain");
        BLLReceptionStatus bllReceptionStatus;
        BLLReceptionChat bllReceptionChat;
        BLLReceptionChatDD bllReceptionChatDD;
        BLLReceptionStatusArchieve bllReceptionStatusArchieve;
        BLLIMUserStatus bllIMUserStatus;
        InstantMessage iIM;
        IViewIdentityList iViewIdentityList;
        public PMain(BLLReceptionStatus bllReceptionStatus,
            BLLReceptionStatusArchieve bllReceptionStatusArchieve,
             BLLReceptionChatDD bllReceptionChatDD,
             BLLReceptionChat bllReceptionChat,
             BLLIMUserStatus bllIMUserStatus,
            InstantMessage iIM,
            IViewIdentityList iViewIdentityList)
        {
            this.bllReceptionStatus = bllReceptionStatus;
            this.bllReceptionStatusArchieve = bllReceptionStatusArchieve;
            this.iIM = iIM;
            this.iViewIdentityList = iViewIdentityList;
            this.bllReceptionChatDD = bllReceptionChatDD;
            this.bllReceptionChat = bllReceptionChat;
            this.bllIMUserStatus = bllIMUserStatus;
            SysAssign(3);
        }
        /// <summary>
        /// 系统按数量分配用户给在线客服
        /// </summary>
        /// <param name="num"></param>
        private void SysAssign(int num)
        {
            log.Debug("-------开始 接收离线消息------");
            IList<ReceptionStatus> rsList = bllReceptionStatus.GetRSListByDiandian(GlobalViables.Diandian, num);
            if (rsList.Count > 0)
            {
                log.Debug("需要接待的离线用户数量:"+rsList.Count);
                foreach (ReceptionStatus rs in rsList)
                {
                    #region 接待记录存档
                    SaveRSA(rs.Customer, rs.CustomerService, rs.Order);
                    #endregion
                    rs.CustomerService = GlobalViables.CurrentCustomerService;
                    log.Debug("保存新分配的接待记录");
                    bllReceptionStatus.SaveByRS(rs);

                    CopyDDToChat(rsList.Select(x => x.Customer).ToList());

                    ReceptionChatReAssign rChatReAss = new ReceptionChatReAssign();
                    rChatReAss.From = GlobalViables.Diandian;
                    rChatReAss.To = rs.Customer;
                    rChatReAss.MessageBody = "客服" + rs.CustomerService.DisplayName + "已上线";
                    rChatReAss.ReAssignedCustomerService = rs.CustomerService;
                    rChatReAss.SavedTime = rChatReAss.SendTime = DateTime.Now;
                    rChatReAss.ServiceOrder = rs.Order;
                    rChatReAss.ChatType = Model.Enums.enum_ChatType.ReAssign;

                    //SendMessage(rChatReAss);//保存更换记录，发送消息并且在界面显示
                   // SaveMessage(rChatReAss, true);
                    iIM.SendMessage(rChatReAss);

                    //ClientState.OrderList.Add(rs.Order);
                    ClientState.customerList.Add(rs.Customer);
                    //view.AddCustomerButtonWithStyle(rs.Order, em_ButtonStyle.Unread);
                    iViewIdentityList.AddIdentity(rs.Order);
                }
            }
            else
            {
                try
                {
                    IList<ServiceOrder> orderCList = bllReceptionChatDD.GetCustomListDistinctFrom(num);
                    if (orderCList.Count > 0)
                    {
                        IList<DZMembership> logoffCList = new List<DZMembership>();
                        foreach (ServiceOrder order in orderCList)
                        {
                            if (!logoffCList.Contains(order.Customer))
                            {
                                logoffCList.Add(order.Customer);
                            }

                            //按订单显示按钮
                            //ClientState.OrderList.Add(order);
                            ClientState.customerList.Add(order.Customer);
                            //view.AddCustomerButtonWithStyle(order, em_ButtonStyle.Unread);
                            iViewIdentityList.AddIdentity(order);
                        }
                        CopyDDToChat(logoffCList);
                    }
                }
                catch (Exception)
                {
                    
                }
            }
            log.Debug("-------结束 接收离线消息------");
        }
        /// <summary>
        /// 接待记录存档
        /// </summary>
        /// <param name="customer"></param>
        /// <param name="cs"></param>
        /// <param name="order"></param>
        private void SaveRSA(DZMembership customer, DZMembership cs, ServiceOrder order)
        {
            log.Debug("-------开始 接待记录存档------");
            ReceptionStatusArchieve rsa = new ReceptionStatusArchieve
            {
                Customer = customer,
                CustomerService = cs,
                Order = order,
            };
            bllReceptionStatusArchieve.SaveOrUpdate(rsa);
            log.Debug("-------结束 接待记录存档------");
        }

        /// <summary>
        /// 把点点的记录复制到聊天记录
        /// </summary>
        /// <param name="cList"></param>
        private void CopyDDToChat(IList<DZMembership> cList)
        {
            //查询点点聊天记录表中该用户的聊天记录
            IList<ReceptionChatDD> chatDDList = bllReceptionChatDD.GetChatDDListByOrder(cList);

            ReceptionChat copychat;
            DZMembership cs = GlobalViables.CurrentCustomerService;
            IMUserStatus userStatus = bllIMUserStatus.GetIMUSByUserId(GlobalViables.CurrentCustomerService.Id);
            foreach (ReceptionChatDD chatDD in chatDDList)
            {
                copychat = ReceptionChat.Create(chatDD.ChatType);
              //  copychat.Id = chatDD.Id;
                copychat.MessageBody = chatDD.MessageBody;
                copychat.ReceiveTime = chatDD.ReceiveTime;
                copychat.SendTime = chatDD.SendTime;
                copychat.To = cs;
                copychat.From = chatDD.From;
                
                copychat.SavedTime = chatDD.SavedTime;
                copychat.ChatType = chatDD.ChatType;
                copychat.FromResource = chatDD.FromResource;
                copychat.ToResource = (enum_XmppResource)Enum.Parse(typeof(enum_XmppResource), userStatus.ClientName);
                copychat.ServiceOrder = chatDD.ServiceOrder;
                copychat.Version = chatDD.Version;
                if (chatDD.ChatType == enum_ChatType.Media)
                {
                    ((ReceptionChatMedia)copychat).MedialUrl = chatDD.MedialUrl;
                    ((ReceptionChatMedia)copychat).MediaType = chatDD.MediaType;
                }
                chatDD.IsCopy = true;
                bllReceptionChat.Save(copychat);

               
                bllReceptionChatDD.Save(chatDD);
            }
        }




    }

}
