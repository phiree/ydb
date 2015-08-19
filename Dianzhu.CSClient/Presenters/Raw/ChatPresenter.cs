using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Dianzhu.CSClient.Presenters.Raw
{
    public  class ChatPresenter
    {
        Views.Raw.IChatView iview;
        public ChatPresenter(Views.Raw.IChatView iview)
        {
            this.iview = iview;
        }
        public void LoadChatHistory()
        {
            Models.Raw.ChatModel m = new Models.Raw.ChatModel();
            iview.MessageList = m.GetChatHistory();
        }
    }
}
