using System;
using System.Collections.Generic;
using System.Linq;

using log4net;

using System.Deployment;
using System.Deployment.Application;
using System.Threading;
using System.ComponentModel;
using System.Windows;
using Dianzhu.BLL;
using Dianzhu.CSClient.IView;
using ViewWPF = Dianzhu.CSClient.ViewWPF;

using cw = Castle.Windsor;
using cmr = Castle.MicroKernel.Registration;
using Dianzhu.DAL;
using Dianzhu.IDAL;
using Dianzhu.Model;
using NHibernate;
using DDDCommon.Domain;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Ydb.InstantMessage.Application;
using Dianzhu.Model.Enums;
using Dianzhu.CSClient.Presenter;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.Application;
using Dianzhu.CSClient.LocalStorage;
using System.Windows.Threading;
using Ydb.InstantMessage.Application.Dto;

namespace Dianzhu.CSClient
{
    static class Program
    {
        static ILog log = LogManager.GetLogger("Dianzhu.CSClient");
        
        static PIdentityList pIdentityList;
        static PMain mainPresenter;
        static Dictionary<string,IViewTabContent> viewTabContentList = new Dictionary<string, IViewTabContent>();
        static IDZMembershipService memberService;
        static LocalChatManager localChatManager;

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            PHSuit.Logging.Config("Dianzhu.CSClient");

            //systemconfig
            AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            //log

            log.Debug("开始启动助理工具");
            bool isValidConfig = CheckConfig();
            if (!isValidConfig)
            {
                MessageBox.Show("配置错误,程序即将退出");
                Application.ExitThread();
                return;
            }

            Bootstrap.Boot();

            memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            localChatManager = Bootstrap.Container.Resolve<LocalChatManager>();

            string version = GetVersion();
            //  loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter = Bootstrap.Container.Resolve<Presenter.LoginPresenter>();
            loginPresenter.Args = args;
            bool? result = loginPresenter.ShowDialog();


            bool useWpf = true;
            //登录成功
            if (result.Value)// == DialogResult.OK)
            {
                log.Debug("登录成功");

                pIdentityList = Bootstrap.Container.Resolve<Presenter.PIdentityList>();
                IInstantMessage iIM = Bootstrap.Container.Resolve<IInstantMessage>();
                iIM.IMReceivedMessage += IIM_IMReceivedMessage;

                mainPresenter = Bootstrap.Container.Resolve<PMain>();
                Thread t = new Thread(SysAssign);
                t.Start();

                System.Windows.Application app=new System.Windows.Application();
                app.Run((Window) mainPresenter.Form);
              //  mainPresenter.ShowDialog();
            }


            //Application.Run(new Views.Raw.ChatView());

            //var mainForm = new WPF.FormMain();
            //mainForm.ShowDialog();

        }

        private static void SysAssign()
        {
            try
            {
                IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();
                log.Debug("-------开始 接收离线用户------");
                IList<ReceptionStatusDto> assignList = receptionService.AssignCSLogin(GlobalViables.CurrentCustomerService.Id.ToString(), 3);
                
                if (assignList.Count > 0)
                {
                    log.Debug("需要接待的离线用户数量:" + assignList.Count);
                    for (int i = 0; i < assignList.Count; i++)
                    {
                        Guid orderId;
                        if (!Guid.TryParse(assignList[i].OrderId, out orderId))
                        {
                            continue;
                        }

                        IdentityTypeOfOrder type = IdentityManager.UpdateCustomerList(assignList[i].CustomerId, assignList[i].OrderId);
                        //IdentityManager.UpdateIdentityList(order, out type);

                        MemberDto customer = memberService.GetUserById(assignList[i].CustomerId);
                        ClientState.customerList.Add(customer);

                        if (!localChatManager.LocalCustomerAvatarUrls.ContainsKey(assignList[i].CustomerId))
                        {
                            string avatar = string.Empty;
                            if (customer.AvatarUrl != null)
                            {
                                avatar = customer.AvatarUrl;
                            }
                            localChatManager.LocalCustomerAvatarUrls[assignList[i].CustomerId] = avatar;
                        }
                        VMIdentity vmIdentity = new VMIdentity(orderId.ToString(), customer.Id.ToString(), customer.DisplayName, localChatManager.LocalCustomerAvatarUrls[assignList[i].CustomerId]);
                        pIdentityList.AddIdentity(vmIdentity);

                        AddIdentityTab(vmIdentity.CustomerId, vmIdentity.CustomerName);
                    }
                }
            }
            catch (Exception e)
            {
                log.Error(e.ToString());
            }
        }

        private static void IIM_IMReceivedMessage(Ydb.InstantMessage.DomainModel.Chat.ReceptionChatDto chat)
        {
            string errMsg = string.Empty;
            //判断信息类型
            if (chat.ChatType == enum_ChatType.Chat.ToString())
            {
                if (!string.IsNullOrEmpty(chat.SessionId)) 
                {
                    NHibernateUnitOfWork.UnitOfWork.Start();

                    //todo:playvoice
                    //iView.PlayVoice();

                    //1 更新当前聊天列表
                    //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                    IdentityTypeOfOrder type;
                    //IdentityManager.UpdateIdentityList(order, out type);

                    type = IdentityManager.UpdateCustomerList(chat.FromId, chat.SessionId);

                    ReceivedMessage(chat, type);

                    //workerChatImage = new BackgroundWorker();
                    //workerChatImage.DoWork += Worker_DoWork;
                    //workerChatImage.RunWorkerCompleted += Worker_RunWorkerCompleted;
                    //workerChatImage.RunWorkerAsync(chat);

                    //// 用户头像的本地化处理
                    //MemberDto from = memberService.GetUserById(chat.FromId);
                    //if (from.AvatarUrl != null)
                    //{
                    //    workerCustomerAvatar = new BackgroundWorker();
                    //    workerCustomerAvatar.DoWork += WorkerCustomerAvatar_DoWork;
                    //    workerCustomerAvatar.RunWorkerCompleted += WorkerCustomerAvatar_RunWorkerCompleted;
                    //    workerCustomerAvatar.RunWorkerAsync(from);
                    //}

                    NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();
                    NHibernateUnitOfWork.UnitOfWork.DisposeUnitOfWork(null);
                }
            }
        }

        /// <summary>
        /// 接收聊天消息
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="isCurrentIdentity">是否是当前标识</param>
        /// <param name="isCurrentCustomer"></param>
        public static void ReceivedMessage(Ydb.InstantMessage.DomainModel.Chat.ReceptionChatDto chat, IdentityTypeOfOrder type)
        {
            IVMChatAdapter vmChatAdapter = Bootstrap.Container.Resolve<IVMChatAdapter>();           
            
            VMChat vmChat = vmChatAdapter.ChatToVMChat(chat);
            
            switch (type)
            {
                case IdentityTypeOfOrder.CurrentCustomer:
                    viewTabContentList[vmChat.FromId].ViewChatList.AddOneChat(vmChat);
                    break;
                case IdentityTypeOfOrder.InList:
                    viewTabContentList[vmChat.FromId].ViewChatList.AddOneChat(vmChat);
                    pIdentityList.SetIdentityUnread(chat.FromId, 1);
                    break;
                case IdentityTypeOfOrder.NewIdentity:
                    MemberDto customer = memberService.GetUserById(chat.FromId);
                    VMIdentity vmIdentity = new VMIdentity(chat.SessionId, chat.FromId, customer.DisplayName, localChatManager.LocalCustomerAvatarUrls[vmChat.FromId]);
                    pIdentityList.AddIdentity(vmIdentity);
                    pIdentityList.SetIdentityUnread(chat.FromId, 1);

                    AddIdentityTab(vmIdentity.CustomerId, vmIdentity.CustomerName);

                    break;
                default:
                    throw new Exception("无法判断消息属性");

            }
        }

        /// <summary>
        /// 添加用户tab界面
        /// </summary>
        /// <param name="identityId"></param>
        private static void AddIdentityTab(string identityId, string customerName)
        {
            Action act = () =>
            {
                IViewTabContent viewTabContent = Bootstrap.Container.Resolve<IViewTabContent>(new { identity = identityId });
                //viewTabContent.IdleTimerOut += ViewTabContent_IdleTimerOut;
                viewTabContentList.Add(identityId, viewTabContent);

                PSearch pSearch = Bootstrap.Container.Resolve<PSearch>(new
                {
                    viewSearch = viewTabContent.ViewSearch,
                    viewSearchResult = viewTabContent.ViewSearchResult,
                    viewChatList = viewTabContent.ViewChatList,
                    identity = identityId
                });
                PChatList pChatList = Bootstrap.Container.Resolve<PChatList>(new
                {
                    viewChatList = viewTabContent.ViewChatList,
                    identity = identityId,
                    customerName = customerName
                });
                PChatSend pChatSend = Bootstrap.Container.Resolve<PChatSend>(new
                {
                    viewChatSend = viewTabContent.ViewChatSend,
                    viewChatList = viewTabContent.ViewChatList,
                    identity = identityId
                });
                POrderHistory pOrderHistory = Bootstrap.Container.Resolve<POrderHistory>(new
                {
                    viewOrderHistory = viewTabContent.ViewOrderHistory,
                    identity = identityId
                });
                PToolsControl pTabControl = Bootstrap.Container.Resolve<PToolsControl>(new
                {
                    viewToolsControl = viewTabContent.ViewToolsControl,
                    viewSearch = viewTabContent.ViewSearch,
                    identity = identityId
                });

                string identityFriendly = PHSuit.StringHelper.SafeNameForWpfControl(identityId, GlobalViables.PRE_TAB_CUSTOMER);
                mainPresenter.AddIdentityTab(identityFriendly, viewTabContent);
            };
            if (!System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                System.Windows.Application.Current.Dispatcher.Invoke(act);
            }
            else
            {
                act();
            }
        }

        static bool CheckConfig()
        {
            log.Debug("--开始 检查配置是否冲突");
            //need: openfire服务器 数据库,api服务器,三者目标ip应该相等.
            bool isValidConfig = false;
            string connectionString = PHSuit.Security.Decrypt(System.Configuration.ConfigurationManager
                .ConnectionStrings["DianzhuConnectionString"].ConnectionString, false);
            System.Text.RegularExpressions.Match m = System.Text.RegularExpressions.Regex.Match(connectionString, @"(?<=data\s+source\=).+?(?=;uid)");
            string ofserver = Dianzhu.Config.Config.GetAppSetting("ImServer");
            System.Text.RegularExpressions.Match m2 = System.Text.RegularExpressions.Regex.Match(Dianzhu.Config.Config.GetAppSetting("APIBaseURL"), "(?<=https?://).+?(?=:" + Dianzhu.Config.Config.GetAppSetting("GetHttpAPIPort") + ")");

            if (ofserver == m.Value && m.Value == m2.Value)
            {
                isValidConfig = true;
            }
            else
            {
                log.Error(m.Value + "," + m2.Value + "," + ofserver);
            }
            log.Debug("--结束 检查配置是否冲突");
            return isValidConfig;




        }
        static void cDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            { 
                log.Error("异常崩溃:" + e.ExceptionObject.ToString());

                MessageBox.Show(e.ExceptionObject.ToString());
            }
            catch (Exception ex)
            {

            }
        }
        static string GetVersion()
        {
            Version myVersion = new Version();

            if (ApplicationDeployment.IsNetworkDeployed)
                myVersion = ApplicationDeployment.CurrentDeployment.CurrentVersion;
            return myVersion.ToString();
        }
    }
}
