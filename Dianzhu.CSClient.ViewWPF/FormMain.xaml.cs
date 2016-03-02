using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using Dianzhu.CSClient.IView;

namespace Dianzhu.CSClient.ViewWPF
{
    /// <summary>
    /// FormMain.xaml 的交互逻辑
    /// </summary>
    public partial class FormMain : Window,IMainFormView
    {
        public FormMain()
        {
            InitializeComponent();
        }

        public string FormText
        {
            get { return this.Title; }
            set { this.Title = value; }
        }

        public string ButtonNamePrefix
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal CancelCompensation
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public bool CanEditOrder
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Dianzhu.Model.ReceptionChat> ChatLog
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CurrentCustomerService
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public Dianzhu.Model.DZService CurrentService
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string CurrentServiceId
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public decimal DepositAmount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string LocalMediaSaveDir
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string Memo
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string MessageTextBox
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string OrderAmount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string OrderNumber
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Dianzhu.Model.ServiceOrder> OrdersList
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public string OrderStatus
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public int OverTimeForCancel
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Dianzhu.Model.DZMembership> ReceptionCustomerList
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Dianzhu.Model.DZMembership> RecptingCustomList
        {
            set
            {
                throw new NotImplementedException();
            }
        }

        public IDictionary<Dianzhu.Model.DZMembership, string> RecptingCustomServiceList
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public IList<Dianzhu.Model.DZService> SearchedService
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string SelectedImageName
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public Stream SelectedImageStream
        {
            get
            {
                throw new NotImplementedException();
            }
        }

        public string SerachKeyword
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceBusinessName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceDepositAmount
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceDescription
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceName
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceTime
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceUnitPrice
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string ServiceUrl
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public string TargetAddress
        {
            get
            {
                throw new NotImplementedException();
            }

            set
            {
                throw new NotImplementedException();
            }
        }

        public event BeforeCustomerChanged BeforeCustomerChanged;
        public event CreateNewOrder CreateNewOrder;
        public event CreateOrder CreateOrder;
        public event IdentityItemActived IdentityItemActived;
        public event MessageSentAndNew MessageSentAndNew;
        public event NoticeCustomerService NoticeCustomerService;
        public event NoticeOrder NoticeOrder;
        public event NoticePromote NoticePromote;
        public event NoticeSystem NoticeSystem;
        public event OrderStateChanged OrderStateChanged;
        public event AudioPlay PlayAudio;
        public event PushExternalService PushExternalService;
        public event PushInternalService PushInternalService;
        public event ReAssign ReAssign;
        public event SaveReAssign SaveReAssign;
        public event SearchService SearchService;
        public event SelectService SelectService;
        public event MediaMessageSent SendMediaHandler;
        public event MessageSent SendMessageHandler;
        public event SendPayLink SendPayLink;
        public event ViewClosed ViewClosed;

        public void AddCustomerButtonWithStyle(Dianzhu.Model.DZMembership dm, em_ButtonStyle buttonStyle)
        {
            throw new NotImplementedException();
        }

        public void LoadOneChat(Dianzhu.Model.ReceptionChat chat)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomBtn(string cid)
        {
            throw new NotImplementedException();
        }

        public void RemoveCustomBtnAndClear(string cid)
        {
            throw new NotImplementedException();
        }

        public void SetCustomerButtonStyle(Dianzhu.Model.DZMembership dm, em_ButtonStyle buttonStyle)
        {
            throw new NotImplementedException();
        }

        public void ShowMsg(string msg)
        {
            throw new NotImplementedException();
        }

        public void ShowNotice(string noticeContent)
        {
            throw new NotImplementedException();
        }

        public void ShowStreamError(string streamError)
        {
            throw new NotImplementedException();
        }

        public void WindowNotification()
        {
            throw new NotImplementedException();
        }
    }
}
