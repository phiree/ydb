using System;
using System.Collections.Generic;
using System.Linq;

using log4net;

using System.Deployment;
using System.Deployment.Application;
using System.Threading;
using System.ComponentModel;
using System.Windows;

using Dianzhu.CSClient.IView;
using ViewWPF = Dianzhu.CSClient.ViewWPF;

using cw = Castle.Windsor;
using cmr = Castle.MicroKernel.Registration;



using NHibernate;
using FluentNHibernate.Cfg;
using FluentNHibernate.Cfg.Db;
using NHibernate.Tool.hbm2ddl;
using Application = System.Windows.Forms.Application;
using MessageBox = System.Windows.Forms.MessageBox;
using Ydb.InstantMessage.Application;

using Dianzhu.CSClient.Presenter;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.CSClient.Presenter.VMAdapter;
using Ydb.Membership.Application.Dto;
using Ydb.Membership.Application;
using Dianzhu.CSClient.LocalStorage;
using System.Windows.Threading;
using Ydb.InstantMessage.Application.Dto;
using Ydb.InstantMessage.DomainModel.Chat;
using System.IO;
using System.Reflection;
using Ydb.Common;
using Ydb.InstantMessage.DomainModel.Reception;

namespace Dianzhu.CSClient
{
    static class Program
    {
        static ILog log = LogManager.GetLogger("Ydb.CSClient");
        
        static PIdentityList pIdentityList;
        static PMain mainPresenter;
        static Dictionary<string,IViewTabContent> viewTabContentList = new Dictionary<string, IViewTabContent>();
        static Ydb.Common.Infrastructure.IEncryptService encryptService;
        static IDZMembershipService memberService;
        static LocalChatManager localChatManager;

        static void InitData()
        {
            //init servicetype

            BackgroundWorker bgk = new BackgroundWorker();
            bgk.DoWork += Bgk_DoWork;
            bgk.RunWorkerCompleted += Bgk_RunWorkerCompleted;
            bgk.RunWorkerAsync();
        }

        private static void Bgk_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            if (mainPresenter == null) return;
            foreach (IViewTabContent tabContent in mainPresenter.Form.ViewTabContents)
            {
                tabContent.ViewSearch.InitType(GlobalViables.AllServiceType);
            }
        }

        private static void Bgk_DoWork(object sender, DoWorkEventArgs e)
        {
           
          var typeService=  Bootstrap.Container.Resolve<Ydb.BusinessResource.Application.IServiceTypeService>();
            var typeList = typeService.GetTopList();

            GlobalViables.AllServiceType = typeList;
        }

        /// <summary>
        /// 应用程序的主入口点。
        /// </summary>
        [STAThread]
        static void Main(string[] args)
        {
            //PHSuit.Logging.Config("Dianzhu.CSClient");
            //Ydb.Common.LoggingConfiguration.Config("mongodb://112.74.198.215/");
            //systemconfig

            //log
            Bootstrap.Boot();

            AppDomain cDomain = AppDomain.CurrentDomain;
            cDomain.UnhandledException += new UnhandledExceptionEventHandler(cDomain_UnhandledException);
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
           InitData();
            

            log.Debug("开始启动助理工具");
           

            
            encryptService = Bootstrap.Container.Resolve<Ydb.Common.Infrastructure.IEncryptService>();
           
            memberService = Bootstrap.Container.Resolve<IDZMembershipService>();
            localChatManager = Bootstrap.Container.Resolve<LocalChatManager>();

            string version = GetVersionText();
           //  loginForm.FormText += "v" + version;
            Presenter.LoginPresenter loginPresenter = Bootstrap.Container.Resolve<Presenter.LoginPresenter>();
            loginPresenter.LoginForm.Version = version;
            loginPresenter.Args = args;
            bool? result = loginPresenter.ShowDialog();

            
            //登录成功
            if (result.Value)// == DialogResult.OK)
            {
                log.Debug("登录成功");

                pIdentityList = Bootstrap.Container.Resolve<Presenter.PIdentityList>();
                IInstantMessage iIM = Bootstrap.Container.Resolve<IInstantMessage>();
                iIM.IMReceivedMessage += IIM_IMReceivedMessage;

                mainPresenter = Bootstrap.Container.Resolve<PMain>();
                mainPresenter.Form.Version = version;
                mainPresenter.Form.AddCustomerTest += Form_AddCustomerTest;
                
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

        private static void Form_AddCustomerTest()
        {
            string orderId = Guid.NewGuid().ToString();
            string customerId = Guid.NewGuid().ToString();
            
            VMIdentity vmIdentityTest = new VMIdentity(orderId, customerId, "TestCustomer", string.Empty);

            IdentityTypeOfOrder type;
            type = IdentityManager.UpdateCustomerList(customerId,orderId);

            pIdentityList.AddIdentity(vmIdentityTest);

            AddIdentityTab(vmIdentityTest.CustomerId, vmIdentityTest.CustomerName);
        }

        /// <summary>
        /// 自动拉取分配给点点的用户数据
        /// </summary>
        private static void SysAssign()
        {
            try
            {
               
                IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();
                log.Debug("-------开始 接收离线用户------");
              
                IList<ReceptionStatusDto> assignList = receptionService.AssignCSLogin(GlobalViables.CurrentCustomerService.Id.ToString(),GlobalViables.CurrentCustomerService.AreaId, 3);
                
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

        static BackgroundWorker workerChatImage;
        static BackgroundWorker workerCustomerAvatar;
        private static void IIM_IMReceivedMessage(Ydb.InstantMessage.DomainModel.Chat.ReceptionChatDto chat)
        {
            string errMsg = string.Empty;
            //判断信息类型
            if (chat.ChatType == enum_ChatType.Chat.ToString())
            {
                if (!string.IsNullOrEmpty(chat.SessionId)) 
                {
                     
                    IViewMainForm viewMainForm = Bootstrap.Container.Resolve<IViewMainForm>();
                    viewMainForm.PlayVoice();
                    viewMainForm.FlashTaskBar();

                    //1 更新当前聊天列表
                    //2 判断消息 和 聊天列表,当前聊天项的关系(是当前聊天项 但是需要修改订单 非激活的列表, 新聊天.
                    IdentityTypeOfOrder type;
                    type = IdentityManager.UpdateCustomerList(chat.FromId, chat.SessionId);

                    ReceivedMessage(chat, type);

                    //聊天中的图片下载到本地
                    if (chat is ReceptionChatMediaDto)
                    {
                        workerChatImage = new BackgroundWorker();
                        workerChatImage.DoWork += Worker_DoWork;
                        workerChatImage.RunWorkerCompleted += Worker_RunWorkerCompleted;
                        workerChatImage.RunWorkerAsync(chat);
                    }


                    // 用户头像的本地化处理
                    MemberDto from = memberService.GetUserById(chat.FromId);
                    if (from.AvatarUrl != null)
                    {
                        workerCustomerAvatar = new BackgroundWorker();
                        workerCustomerAvatar.DoWork += WorkerCustomerAvatar_DoWork;
                        workerCustomerAvatar.RunWorkerCompleted += WorkerCustomerAvatar_RunWorkerCompleted;
                        workerCustomerAvatar.RunWorkerAsync(from);
                    }
 
                }
            }
        }

        private static void WorkerCustomerAvatar_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            log.Debug("用户头像本地加载完成");
        }

        private static void WorkerCustomerAvatar_DoWork(object sender, DoWorkEventArgs e)
        {
            MemberDto customer = e.Argument as MemberDto;

            string mediaUrl = customer.AvatarUrl;
            string mediaUrl_32X32 = customer.AvatarUrl + "_32X32";
            string fileName = string.Empty;
            string fileName_32X32 = string.Empty;

            if (!mediaUrl.Contains(GlobalViables.MediaGetUrl))
            {
                fileName = mediaUrl;
                fileName_32X32 = mediaUrl_32X32;
                mediaUrl_32X32 = GlobalViables.MediaGetUrl + mediaUrl_32X32;
            }
            else
            {
                fileName = mediaUrl.Replace(GlobalViables.MediaGetUrl, "");
                fileName_32X32 = mediaUrl_32X32.Replace(GlobalViables.MediaGetUrl, "");
            }

            if (!File.Exists(PHSuit.LocalFileManagement.LocalFilePath + fileName_32X32))
            {
                if (PHSuit.LocalFileManagement.DownLoad(string.Empty, mediaUrl_32X32, fileName_32X32))
                {
                    customer.AvatarUrl = fileName;
                    log.Debug("用户头像本地存储完成");
                }
            }
        }

        private static void Worker_RunWorkerCompleted(object sender, RunWorkerCompletedEventArgs e)
        {
            ReceptionChatDto chat = e.Result as ReceptionChatDto;
            if (chat != null)
            {
                if (!string.IsNullOrEmpty(chat.SessionId))
                {
                    //iView.IdleTimerStop(chat.FromId);
                }

                if (chat is ReceptionChatMediaDto)
                {
                    viewTabContentList[chat.FromId].ViewChatList.RemoveChatImageNormalMask(chat.Id);
                }
            }
        }

        private static void Worker_DoWork(object sender, DoWorkEventArgs e)
        {
            ReceptionChatDto chat = e.Argument as ReceptionChatDto;
            
            viewTabContentList[chat.FromId].ViewChatList.ShowChatImageNormalMask(chat.Id);

            string mediaUrl = ((ReceptionChatMediaDto)chat).MedialUrl;
            string fileName = string.Empty;
            if (!mediaUrl.Contains(GlobalViables.MediaGetUrl))
            {
                fileName = mediaUrl;
                mediaUrl = GlobalViables.MediaGetUrl + mediaUrl;
            }
            else
            {
                fileName = ((ReceptionChatMediaDto)chat).MedialUrl.Replace(GlobalViables.MediaGetUrl, "");
            }

            bool downSuccess = PHSuit.LocalFileManagement.DownLoad(string.Empty, mediaUrl, fileName);

            e.Result = chat;
        }

        /// <summary>
        /// 接收聊天消息
        /// </summary>
        /// <param name="chat"></param>
        /// <param name="type">消息所属类型</param>
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

            viewTabContentList[vmChat.FromId].ViewTabContentTimer.StopTimer();
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
                viewTabContentList.Add(identityId, viewTabContent);
                viewTabContent.ViewTabContentTimer.Identity = identityId;
                viewTabContent.ViewTabContentTimer.TimeOver += ViewTabContentTimer_TimeOver;

                PSearch pSearch = Bootstrap.Container.Resolve<PSearch>(new
                {
                    viewSearch = viewTabContent.ViewSearch,
                    viewSearchResult = viewTabContent.ViewSearchResult,
                    viewChatList = viewTabContent.ViewChatList,
                    viewTabContentTimer = viewTabContent.ViewTabContentTimer,
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
                    viewTabContentTimer = viewTabContent.ViewTabContentTimer,
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
                mainPresenter.AddIdentityTabContent(identityFriendly, viewTabContent);
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

        private static void ViewTabContentTimer_TimeOver(string identity)
        {
            log.Debug("计时结束，customerId：" + identity);

            if (IdentityManager.DeleteCustomer(identity))
            {
                string identityTabFriendly = PHSuit.StringHelper.SafeNameForWpfControl(identity, GlobalViables.PRE_TAB_CUSTOMER);
                mainPresenter.RemoveIdentityTabContent(identityTabFriendly);

                viewTabContentList.Remove(identity);
                pIdentityList.RemoveIdentity(identity);

                IReceptionService receptionService = Bootstrap.Container.Resolve<IReceptionService>();
                receptionService.DeleteReception(identity);
            }                
        }

        
        static void cDomain_UnhandledException(object sender, UnhandledExceptionEventArgs e)
        {
            try
            { 
                log.Error("异常崩溃:" + e.ExceptionObject.ToString());

                MessageBox.Show(e.ExceptionObject.ToString());
            }
            catch 
            {

            }
        }
        static string GetVersionText()
        {
            return DateTime.Now.ToString("(yyyy-MM-dd hh:mm:ss)") + GetVersion();
        }
        static string GetVersion()
        {
            string versionText;

            Version myVersion = new Version();

            if (ApplicationDeployment.IsNetworkDeployed)
            {
                versionText = ApplicationDeployment.CurrentDeployment.CurrentVersion.ToString();

            }
            else
            {
                versionText = Assembly.GetExecutingAssembly().GetName().Version.ToString();
            }

            return versionText;



        }
    }
}
