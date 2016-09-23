using Dianzhu.CSClient.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
    public class VMChatFactory
    {
        string chatId; string fromId; string fromName; DateTime savedTime;double savedTimestamp; string csAvatar; string customerAvatar; string chatBackground; bool isFromCs;

        public VMChatFactory(string chatId, string fromId, string fromName, DateTime savedTime,double savedTimestamp, string csAvatar, string customerAvatar, string chatBackground, bool isFromCs)
        {
            this.chatId = chatId;
            this.fromId = fromId;
            this.fromName = fromName;
            this.savedTime = savedTime;
            this.savedTimestamp = savedTimestamp;
            this.csAvatar = csAvatar;
            this.customerAvatar = customerAvatar;
            this.chatBackground = chatBackground;
            this.isFromCs = isFromCs;
        }

        public VMChat CreateVMChatText(string messageBody)
        {
            return new VMChatText(messageBody, 
                chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, customerAvatar, chatBackground, isFromCs);
        }

        public VMChat CreateVMChatMedia(string mediaType,string mediaUrl)
        {
            return new VMChatMedia(mediaType, mediaUrl,
                chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, customerAvatar, chatBackground, isFromCs);
        }

        public VMChat CreateVMChatPushServie(string servieName, bool isVerify, string imageUrl, int creditPoint, decimal unitPrice, decimal depositAmount, string serviceMemo)
        {
            return new VMChatPushServie(servieName, isVerify, imageUrl, creditPoint,unitPrice, depositAmount, serviceMemo, 
                chatId, fromId, fromName, savedTime, savedTimestamp, csAvatar, customerAvatar, chatBackground, isFromCs);
        }
    }
}
