using Dianzhu.CSClient.ViewModel;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dianzhu.CSClient.Presenter.VMAdapter
{
     public interface IVMChatAdapter
    {
        VMChat ChatToVMChat(Ydb.InstantMessage.DomainModel.Chat.ReceptionChatDto chat);
    }
}
