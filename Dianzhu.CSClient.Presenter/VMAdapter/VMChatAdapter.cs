using Dianzhu.CSClient.ViewModel;
using Dianzhu.IDAL;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public class VMChatAdapter:IVMChatAdapter
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.VMChatAdapter");

        IDALMembership dalMembership;
        IDALDZService dalDZService;

        public VMChatAdapter(IDALMembership dalMembership,IDALDZService dalDZService)
        {
            this.dalMembership = dalMembership;
            this.dalDZService = dalDZService;
        }

        public VMChat ChatToVMChat(ReceptionChat chat,string customerAvatar)
        {
            DZMembership from = dalMembership.FindById(Guid.Parse(chat.FromId));
            NHibernateUnitOfWork.UnitOfWork.Current.TransactionalFlush();

            if (from == null)
            {
                throw new Exception("发送方用户不存在");
            }

            string chatId = chat.Id.ToString();
            string fromId = chat.FromId;
            string fromName = from.DisplayName;
            DateTime savedTime = chat.SavedTime;
            double savedTimestamp = chat.SavedTimestamp;
            string csAvatar = "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png";
            string cAvatar = customerAvatar ?? string.Empty;
            string chatBackground = "#b3d465";
            bool isFromCs = chat.IsfromCustomerService;

            VMChatFactory vmChatFactory = new VMChatFactory(chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, cAvatar, chatBackground, isFromCs);


            switch (chat.GetType().Name)
            {
                case "ReceptionChat":
                    return vmChatFactory.CreateVMChatText(chat.MessageBody);
                case "ReceptionChatMedia":
                    ReceptionChatMedia chatMedia = chat as ReceptionChatMedia;
                    string mediaType = chatMedia.MediaType;
                    string mediaUrl = chatMedia.MedialUrl;

                    return vmChatFactory.CreateVMChatMedia(mediaType, mediaUrl);
                case "ReceptionChatPushService":
                    ReceptionChatPushService chatPushService = chat as ReceptionChatPushService;
                    DZService service = dalDZService.FindById(Guid.Parse(chatPushService.ServiceInfos[0].ServiceId));
                    if (service == null)
                    {
                        throw new Exception("服务不存在,id:"+ chatPushService.ServiceInfos[0].ServiceId);
                    }

                    string servieName = service.Name ?? string.Empty;
                    bool isVerify = service.IsCertificated;
                    string imageUrl = service.Business.BusinessAvatar.ImageName;
                    int creditPoint = 5;//没有数据，暂时设置为5
                    decimal unitPrice = service.UnitPrice;
                    decimal depositAmount = service.DepositAmount;
                    string serviceMemo = service.Description ?? string.Empty;

                    return vmChatFactory.CreateVMChatPushServie(servieName, isVerify, imageUrl, creditPoint, unitPrice, depositAmount, serviceMemo);
                default:
                    throw new Exception("Unknow Type:" + chat.GetType().Name);
            }
        }
    }
}
