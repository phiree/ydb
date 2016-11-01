using Dianzhu.CSClient.LocalStorage;
using Dianzhu.CSClient.ViewModel;
using Dianzhu.IDAL;
using Dianzhu.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Ydb.InstantMessage.DomainModel.Chat;
using Ydb.Membership.Application;
using Ydb.Membership.Application.Dto;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public class VMChatAdapter:IVMChatAdapter
    {
        log4net.ILog log = log4net.LogManager.GetLogger("Dianzhu.CSClient.Presenter.VMChatAdapter");

        IDZMembershipService memberService;
        IDALDZService dalDZService;
        LocalChatManager localChatManager;

        public VMChatAdapter(IDZMembershipService memberService, IDALDZService dalDZService, LocalChatManager localChatManager)
        {
            this.memberService = memberService;
            this.dalDZService = dalDZService;
            this.localChatManager = localChatManager;
        }

        public VMChat ChatToVMChat(ReceptionChatDto chat)
        {
            MemberDto from = memberService.GetUserById(chat.FromId);

            if (from == null)
            {
                throw new Exception("发送方用户不存在");
            }

            string chatId = chat.Id.ToString();
            string fromId = chat.FromId;
            string fromName = from.NickName;
            DateTime savedTime = chat.SavedTime;
            double savedTimestamp = chat.SavedTimestamp;
            string csAvatar = "pack://application:,,,/Dianzhu.CSClient.ViewWPF;component/Resources/DefaultCS.png";
            string cAvatar = string.Empty;
            string chatBackground = "#b3d465";
            bool isFromCs = chat.IsfromCustomerService;

            if (!chat.IsfromCustomerService )
            {
                if (!localChatManager.LocalCustomerAvatarUrls.Keys.Contains(chat.FromId))
                {
                    localChatManager.LocalCustomerAvatarUrls[chat.FromId] = from.AvatarUrl;
                }

                cAvatar = localChatManager.LocalCustomerAvatarUrls[chat.FromId];
            }

            VMChatFactory vmChatFactory = new VMChatFactory(chatId, fromId, fromName, fromId, savedTime, savedTimestamp, csAvatar, cAvatar, chatBackground, isFromCs);


            switch (chat.GetType().Name)
            {
                case "ReceptionChatDto":
                    return vmChatFactory.CreateVMChatText(chat.MessageBody);
                case "ReceptionChatMediaDto":
                    ReceptionChatMediaDto chatMedia = chat as ReceptionChatMediaDto;
                    string mediaType = chatMedia.MediaType;
                    string mediaUrl = chatMedia.MedialUrl;

                    return vmChatFactory.CreateVMChatMedia(mediaType, mediaUrl);
                case "ReceptionChatPushServiceDto":
                    ReceptionChatPushServiceDto chatPushService = chat as ReceptionChatPushServiceDto;                    
                    Guid serviceId;
                    if (!Guid.TryParse(chatPushService.ServiceInfos[0].ServiceId, out serviceId))
                    {
                        throw new Exception("ServiceInfos有误,chatPushServiceId:" + chatPushService.Id);
                    }
                    DZService service = dalDZService.FindById(serviceId);
                    if (service == null)
                    {
                        throw new Exception("服务不存在,id:" + chatPushService.ServiceInfos[0].ServiceId);
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
