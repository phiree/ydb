using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Dianzhu.Model;

namespace Dianzhu.CSClient.Presenter
{
    /// <summary>
    /// 接收消息的模板方法. 似乎不太需要 因为只有 chatbuilder不一样.
    /// </summary>
    public class SendMessageTemplate
    {
        public SendMessageTemplate()
        { }
        public virtual void Send()
        {
             
            SaveChat();
            SendMessage();
            LoadView();

        }
       
        public virtual void SaveChat() { }
        public virtual void SendMessage() { }
        public virtual void LoadView() { }
    }
    
     
}
