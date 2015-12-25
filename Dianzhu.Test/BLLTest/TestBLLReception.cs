using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using Dianzhu.BLL;
using Dianzhu.DAL;
using Dianzhu.Model;
using Dianzhu.Model.Enums;
using Rhino.Mocks;
using FizzWare.NBuilder;
namespace Dianzhu.Test.BLLTest
{
    [TestFixture]
   public class TestBLLReception
    {
        /// <summary>
        /// 获取聊天记录,按照时间倒序
        /// </summary>
        [Test]
        public void GetHistoryReceptionChat()
        {
            var dateBegin=new DateTime(2000,1,1);
            var dateEnd=new DateTime(2000,1,2);
            var receptionList = Builder<ReceptionBase>.CreateListOfSize(10).TheFirst(2)
                .With(x => x.TimeBegin = new DateTime(2000, 1, 1))
                .TheNext(2)
                .With(x=>x.TimeBegin=new DateTime(2003,1,2))
                .TheNext(5)
                .With(x=>x.TimeBegin=new DateTime(2005,1,3))
                .Build();
            Random r = new Random();
            foreach (ReceptionBase rb in receptionList)
            {
                var vchatList = Builder<ReceptionChat>.CreateListOfSize(3).TheFirst(1)
                    .With(x => x.SavedTime = rb.TimeBegin.AddMinutes(r.NextDouble()))
                   .TheNext(1)
                    .With(x => x.SavedTime = rb.TimeBegin.AddMinutes(r.NextDouble()))
                    .TheNext(1)
                    .With(x => x.SavedTime = rb.TimeBegin.AddMinutes(r.NextDouble()))
                       .Build();
                rb.ChatHistory.Add(vchatList[0]);
                rb.ChatHistory.Add(vchatList[1]);
                rb.ChatHistory.Add(vchatList[2]);
            }


            int rowCount;
            var dal = MockRepository.GenerateStub<DAL.DALReception>(string.Empty);
            dal.Stub(x => x.GetReceptionList(null, null, dateBegin, dateEnd, 0,10,out rowCount )).Return(receptionList);
            
            var bll = MockRepository.GenerateStub<BLLReception>(dal);
           IList<ReceptionChat> chatList= bll.GetReceptionChatList(null, null,Guid.Empty, dateBegin,dateEnd,0,10,enum_ChatTarget.all, out rowCount);
            if(chatList != null)
            {
                foreach (ReceptionChat c in chatList)
                {
                    Console.WriteLine(c.SavedTime);
                }
            }
            
        }

        [Test]
        public void AssignByCSId()
        {
            var dal = new DALReceptionStatus();
            //Guid guid = new Guid("dc73ba0f-91a4-4e14-b17a-a567009dfd6a");
            //var dzList = dal.GetCSMinCount(guid);
            //DZMembership d;
            //for(int i=0;i<dzList.Count;i++)
            //{
            //    d = new DZMembership();
            //    d = dzList[i];
            //    Console.WriteLine(d.DisplayName);
            //}
        }
    }
}
